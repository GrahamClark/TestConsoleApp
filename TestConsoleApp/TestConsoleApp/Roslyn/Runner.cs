using System;
using Roslyn.Scripting;
using Roslyn.Scripting.CSharp;

namespace TestConsoleApp.Roslyn
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            SimpleEngine();
            HostedEngine();
            Report();
        }

        private static void Report()
        {
            var report = new Report();
            var engine = new ScriptEngine(references: new[] { "System.Core", report.GetType().Assembly.Location });
            var session = Session.Create(report);

            engine.Execute(@"using System.Linq;", session);
            engine.Execute(@"GetValues = () => { for(int i = 1; i <= 3; i++) AddValue(i); };", session);
            engine.Execute(@"CalculateResult = () => Values.Sum();", session);

            report.PrintResult();
        }

        private static void HostedEngine()
        {
            var host = new HostObject();
            var engine = new ScriptEngine(new[] { host.GetType().Assembly.Location });
            var session = Session.Create(host);
            engine.Execute(@"Value = 42;", session);
            engine.Execute(@"System.Console.WriteLine(Value);", session);
            Console.WriteLine(host.Value);
        }

        private static void SimpleEngine()
        {
            var engine = new ScriptEngine(importedNamespaces: new[] { "System" });
            var session = Session.Create();
            engine.Execute(@"Console.WriteLine(""Hello Roslyn"");", session);
            engine.Execute(@"var i = 10;", session);
            engine.Execute(@"System.Console.WriteLine(i+1);", session);
        }
    }
}
