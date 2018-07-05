using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCoreWebsite.Models;
using WorkflowCoreWebsite.Workflows.Steps;

namespace WorkflowCoreWebsite.Workflows
{
    public class ContactFormWorkflow : IWorkflow<ContactForm>
    {
        public string Id => nameof(ContactFormWorkflow);

        public int Version => 1;

        public void Build(IWorkflowBuilder<ContactForm> builder)
        {
            // Email admins every 30 seconds until receipt is acknowledged.
            builder
                .Start()
                .Parallel()
                    .Do(then => then
                        .StartWith(context => ExecutionResult.Next())
                        .WaitFor("ContactFormAcknowledged", (data, step) => data.Key.ToString())
                        .Output(step => step.Acknowledged, data => true)
                    )
                    .Do(then => then
                        .StartWith(context => ExecutionResult.Next())
                        .Recur(data => TimeSpan.FromSeconds(30), data => data.Acknowledged)
                        .Do(innerThen => innerThen
                            .StartWith(context => ExecutionResult.Next())
                            .Then<EmailAdmins>()
                                .Input(step => step.Key, data => data.Key)
                                .Input(step => step.From, data => data.From)
                                .Input(step => step.Subject, data => data.Subject)
                                .Input(step => step.Message, data => data.Message)
                        )
                    )
                .Join()
            ;
        }
    }
}
