using System.Management.Automation;

namespace Thoughtology.Expresso.Tests.Foundation
{
    [Cmdlet(VerbsCommunications.Write, "Input")]
    public class WriteInputCommand : PSCmdlet
    {
        [Parameter(
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public object InputObject { get; set; }

        protected override void ProcessRecord()
        {
            WriteObject(InputObject);
        }
    }
}
