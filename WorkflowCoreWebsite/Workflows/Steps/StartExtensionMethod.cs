using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Primitives;

namespace WorkflowCoreWebsite.Workflows.Steps
{
    public static class StartExtensionMethod
    {
        public static IStepBuilder<TData, InlineStepBody> Start<TData>(this IWorkflowBuilder<TData> builder)
        {
            return builder.StartWith(context => ExecutionResult.Next());
        }
    }
}
