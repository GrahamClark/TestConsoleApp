using System;
using System.Collections.Generic;

namespace TestConsoleApp.ExpressionTrees
{
    internal class Runner : IRunner
    {
        public void RunProgram()
        {
            Queue<MathOpOrVal> postfix = ExpressionParser.InfixToPostfix("((1+2)+3)*2-8/4");
            foreach (MathOpOrVal mv in postfix)
            {
                Console.Write("{0} ", mv.ToString());
            }
            Console.WriteLine("\n");

            TreeNode tree = ExpressionParser.ToTree(postfix);
            TreeNode.PrintTree(tree);
            Console.WriteLine("\n");

            int result = ILBuilder.Execute(tree);
            Console.WriteLine("\nResult = " + result);
        }
    }
}
