using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;

namespace Thoughtology.Expresso.Commands.Runtime
{
    /// <summary>
    /// Provides extension methods for the <see cref="PSCmdlet"/> class.
    /// </summary>
    public static class PSCmdletExtension
    {
        /// <summary>
        /// Invokes a <see cref="PSCmdlet"/> in a dynamically generated runspace.
        /// </summary>
        /// <param name="cmdlet">The cmdlet to invoke.</param>
        /// <remarks>
        /// This method allows to invoke a <see cref="PSCmdlet"/> in the same way as a <see cref="Cmdlet"/>.
        /// A runspace will be created automatically at runtime. The invocation command is constructed from the <see cref="CmdletAttribute"/>
        /// of the specified cmdlet and the parameters are inferred from the properties decorated with the <see cref="ParameterAttribute"/>.
        /// </remarks>
        /// <returns>
        /// The sequence of objects returned to the pipeline by the cmdlet.
        /// </returns>
        public static IEnumerable InvokeInRunspace(this PSCmdlet cmdlet)
        {
            VerifyArgument(cmdlet);
            var runspace = CreateRunspaceForCmdlet(cmdlet);
            return InvokeRunspace(runspace);
        }

        /// <summary>
        /// Invokes a <see cref="PSCmdlet"/> in a dynamically generated runspace.
        /// </summary>
        /// <param name="cmdlet">The cmdlet to invoke.</param>
        /// <remarks>
        /// This method allows to invoke a <see cref="PSCmdlet"/> in the same way as a <see cref="Cmdlet"/>.
        /// A runspace will be created automatically at runtime. The invocation command is constructed from the <see cref="CmdletAttribute"/>
        /// of the specified cmdlet and the parameters are inferred from the properties decorated with the <see cref="ParameterAttribute"/>.
        /// </remarks>
        /// <typeparam name="T">The type of objects returned by the cmdlet.</typeparam>
        /// <returns>
        /// The sequence of objects returned to the pipeline by the cmdlet.
        /// </returns>
        public static IEnumerable<T> InvokeInRunspace<T>(this PSCmdlet cmdlet)
        {
            VerifyArgument(cmdlet);
            var runspace = CreateRunspaceForCmdlet(cmdlet);
            return InvokeRunspace<T>(runspace);
        }

        private static void VerifyArgument(PSCmdlet cmdlet)
        {
            if (cmdlet == null)
            {
                throw new ArgumentNullException("cmdlet");
            }
        }

        private static PowerShell CreateRunspaceForCmdlet(PSCmdlet cmdlet)
        {
            var session = ImportCmdletInSession(cmdlet);
            var runspace = CreateRunspaceWithSession(session);
            runspace = AddCmdletToRunspace(cmdlet, runspace);
            return runspace;
        }

        private static InitialSessionState ImportCmdletInSession(PSCmdlet cmdlet)
        {
            var session = InitialSessionState.CreateDefault();
            var cmdletAssemblyLocation = cmdlet.GetType().Assembly.Location;
            session.ImportPSModule(new[] { cmdletAssemblyLocation });
            return session;
        }

        private static PowerShell CreateRunspaceWithSession(InitialSessionState session)
        {
            var runspace = RunspaceFactory.CreateRunspace(session);
            runspace.Open();
            var host = PowerShell.Create();
            host.Runspace = runspace;
            return host;
        }

        private static PowerShell AddCmdletToRunspace(PSCmdlet cmdlet, PowerShell runspace)
        {
            var cmdletType = cmdlet.GetType();
            string cmdletName = GetCmdletName(cmdletType);
            var parameters = GetCmdletParameters(cmdletType);
            AddCmdletInvocationToRunspace(cmdlet, runspace, cmdletName, parameters);
            return runspace;
        }

        private static IEnumerable<PropertyInfo> GetCmdletParameters(Type cmdletType)
        {
            return cmdletType.GetProperties().Where(p => IsParameter(p));
        }

        private static bool IsParameter(PropertyInfo property)
        {
            return property.GetCustomAttributes(typeof(ParameterAttribute), true).Any();
        }

        private static string GetCmdletName(Type cmdletType)
        {
            var cmdletAttribute = GetCmdletAttribute(cmdletType);
            return BuildCmdletNameFromAttribute(cmdletAttribute);
        }

        private static CmdletAttribute GetCmdletAttribute(Type cmdletType)
        {
            return cmdletType
                .GetCustomAttributes(typeof(CmdletAttribute), inherit: false)
                .Cast<CmdletAttribute>()
                .First();
        }

        private static string BuildCmdletNameFromAttribute(CmdletAttribute cmdletAttribute)
        {
            return String.Concat(cmdletAttribute.VerbName, "-", cmdletAttribute.NounName);
        }

        private static void AddCmdletInvocationToRunspace(
            PSCmdlet cmdlet,
            PowerShell runspace,
            string cmdletName,
            IEnumerable<PropertyInfo> parameters)
        {
            runspace.AddCommand(cmdletName);

            foreach (var parameterProperty in parameters)
            {
                runspace.AddParameter(parameterProperty.Name, parameterProperty.GetValue(cmdlet, null));
            }
        }

        private static IEnumerable InvokeRunspace(PowerShell runspace)
        {
            var results = runspace.Invoke();
            return results.Select(i => i.BaseObject);
        }

        private static IEnumerable<T> InvokeRunspace<T>(PowerShell runspace)
        {
            var results = runspace.Invoke();
            return results.Select(i => (T)i.BaseObject);
        }
    }
}
