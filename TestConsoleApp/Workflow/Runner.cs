using System;
using System.Activities;
using System.Collections.Generic;

namespace TestConsoleApp.Workflow
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            var workflow = new SequenceActivity();

            // setting property values like this only works for primitive types
            //workflow.firstName = "Graham";

            IDictionary<string, object> inputs = new Dictionary<string, object>();
            inputs["firstName"] = "Graham";

            //IDictionary<string, object> outputs = WorkflowInvoker.Invoke(workflow, inputs);
            //Console.WriteLine(outputs["greeting"]);

            var app = new WorkflowApplication(workflow, inputs);
            app.Completed = e =>
                {
                    Console.WriteLine("Completed with CompletionState {0}", e.CompletionState);
                    Console.WriteLine(e.Outputs["greeting"]);
                };
            app.Run();
        }
    }
}
