using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace Thoughtology.Expresso.Commands.Runtime
{
    public static class PSCmdletExtension
    {
        public static IEnumerable InvokeInRunspace(this PSCmdlet cmdlet)
        {
            var runspace = CreateRunspaceForCmdlet(cmdlet);
            return InvokeRunspace(runspace);
        }

        public static IEnumerable<T> InvokeInRunspace<T>(this PSCmdlet cmdlet)
        {
            var runspace = CreateRunspaceForCmdlet(cmdlet);
            return InvokeRunspace<T>(runspace);
        }

        private static PowerShell CreateRunspaceForCmdlet(PSCmdlet cmdlet)
        {
            var cmdletType = cmdlet.GetType();

            var parameterProperties = cmdletType
                .GetProperties()
                .Where(p => p.GetCustomAttributes(typeof(ParameterAttribute), inherit: true).Any());

            var cmdletAttribute = cmdletType
                .GetCustomAttributes(typeof(CmdletAttribute), inherit: false)
                .Cast<CmdletAttribute>()
                .First();
            var cmdletName = String.Concat(cmdletAttribute.VerbName, "-", cmdletAttribute.NounName);

            var session = InitialSessionState.CreateDefault();
            session.ImportPSModule(new[] { cmdletType.Assembly.Location });
            var runspace = RunspaceFactory.CreateRunspace(session);
            runspace.Open();
            var host = PowerShell.Create();
            host.Runspace = runspace;
            host.AddCommand(cmdletName);

            foreach (var parameterProperty in parameterProperties)
            {
                host.AddParameter(parameterProperty.Name, parameterProperty.GetValue(cmdlet, index: null));
            }

            return host;
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
