using System.Management.Automation;

namespace Thoughtology.Expresso.Tests.Foundation
{
    [Cmdlet(VerbsCommunications.Write, "PassThrough")]
    public class WritePassThroughCommand : PSCmdlet
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
