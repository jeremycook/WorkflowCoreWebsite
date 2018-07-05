using System;
using System.Net.Mail;
using System.Web;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowCoreWebsite.Workflows.Steps
{
    public class EmailAdmins : StepBody
    {
        private readonly EmailAdminsOptions options;

        public EmailAdmins()
        {
            // TODO: Get options from configuration/services.
            options = new EmailAdminsOptions
            {
                AdminEmails = "admins@example.com",
                DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                PickupDirectoryLocation = @"C:\Development\SmtpPickupDirectory",
                AckUrlFormat = "https://localhost:44389/Home/Ack/{0}",
            };
        }

        public string From { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public Guid Key { get; internal set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            using (var client = new SmtpClient()
            {
                DeliveryMethod = options.DeliveryMethod,
                PickupDirectoryLocation = options.PickupDirectoryLocation,
            })
            {
                var fullMessage = HttpUtility.HtmlEncode(Message) + $@"<br><br>

<a href=""{string.Format(options.AckUrlFormat, Key)}"">Acknowledge receipt</a>
";
                client.Send(new MailMessage(From, options.AdminEmails, Subject, fullMessage) { IsBodyHtml = true });
            }

            return ExecutionResult.Next();
        }
    }
}