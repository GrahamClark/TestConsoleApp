using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

namespace TestConsoleApp.PythonScripting
{
    /// <summary>
    /// From http://blogs.microsoft.co.il/blogs/berniea/archive/2008/12/04/extending-your-c-application-with-ironpython.aspx
    /// </summary>
    public class Runner : IRunner
    {
        ScriptEngine engine = null;
        ScriptRuntime runtime = null;
        ScriptScope scope = null;

        public void RunProgram()
        {
            engine = Python.CreateEngine();
            runtime = engine.Runtime;
            scope = runtime.CreateScope();

            RunExpression();

            RunInScope();

            RunFromFile();
        }

        private void RunFromFile()
        {
            var salesBasket = new SalesBasket()
            {
                Lines = new List<Line>()
                {
                    new Line { ProductName = "Prod1", ProductPrice = 100, Quantity = 2, Amount = 100 * 2},
                    new Line { ProductName = "Prod2", ProductPrice = 20, Quantity = 1, Amount = 20 * 1},
                    new Line { ProductName = "Prod3", ProductPrice = 45.8, Quantity = 2, Amount = 45.8 * 2},
                    new Line { ProductName = "Prod4", ProductPrice = 3.9, Quantity = 10, Amount = 3.9 * 10},
                    new Line { ProductName = "Prod5", ProductPrice = 555.5, Quantity = 10, Amount = 555.5 * 10}
                }
            };

            string dir = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            string pythonDir = Path.Combine(dir, "PythonScripting");
            var files = new List<string>();
            foreach (string path in Directory.GetFiles(pythonDir))
            {
                if (path.ToLower().EndsWith(".py"))
                {
                    files.Add(path);
                }
            }

            string libPath = Path.Combine(Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName,
                                          "TestConsoleApp.exe");
            Assembly assembly = Assembly.LoadFile(libPath);
            runtime.LoadAssembly(assembly);

            scope.SetVariable("salesBasket", salesBasket);

            foreach (var file in files)
            {
                Console.WriteLine("\nFrom Python file {0}", file);
                ScriptSource source = engine.CreateScriptSourceFromFile(file);
                source.Execute(scope);
            }
        }

        private void RunInScope()
        {
            string code = @"emp.Salary * 0.3";

            ScriptSource source = engine.CreateScriptSourceFromString(code, SourceCodeKind.Expression);

            var emp = new Employee { Id = 1000, Name = "Tester", Salary = 1000 };

            scope.SetVariable("emp", emp);
            float result = source.Execute<float>(scope);
            Console.WriteLine("RunInScope result = {0}", result);
        }

        private void RunExpression()
        {
            string code = @"100 * 2 + 4 / 3";

            ScriptSource source = engine.CreateScriptSourceFromString(code, SourceCodeKind.Expression);

            int result = source.Execute<int>();
            Console.WriteLine("RunExpression result \"{0}\" = {1}", code, result);
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Salary { get; set; }
    }
}
