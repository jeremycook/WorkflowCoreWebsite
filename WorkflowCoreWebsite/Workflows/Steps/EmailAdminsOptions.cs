using System.Net.Mail;

namespace WorkflowCoreWebsite.Workflows.Steps
{
    internal class EmailAdminsOptions
    {
        public string AdminEmails { get; set; }
        public SmtpDeliveryMethod DeliveryMethod { get; set; }
        public string PickupDirectoryLocation { get; set; }
        public string AckUrlFormat { get; set; }
    }
}