using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;

namespace TestConsoleApp.ExpressionTrees
{
    internal static class ExpressionParser
    {
        private static Queue<MathOpOrVal> queue;

        public static Queue<MathOpOrVal> InfixToPostfix(string expression)
        {
            queue = new Queue<MathOpOrVal>();

            Stack<char> stack = new Stack<char>();
            StringReader sr = new StringReader(expression);
            char c;

            do
            {
                c = (char)sr.Read();

                if (char.IsWhiteSpace(c))
                {
                    continue;
                }
                else if (char.IsDigit(c))
                {
                    int val = (int)(c - '0');

                    char p = (char)sr.Peek();
                    while (p >= '0' && p <= '9')
                    {
                        val = val * 10 + (int)(sr.Read() - '0');
                        p = (char)sr.Peek();
                    }
                    Append(val);
                }
                else if (c == '(')
                {
                    stack.Push(c);
                }
                else if (c == '+' || c == '-' || c == '*' || c == '/')
                {
                    bool repeat;
                    do
                    {
                        repeat = false;
                        if (stack.Count == 0)
                        {
                            stack.Push(c);
                        }
                        else if (stack.Peek() == '(')
                        {
                            stack.Push(c);
                        }
                        else if (Precedence(c) > Precedence(stack.Peek()))
                        {
                            stack.Push(c);
                        }
                        else
                        {
                            Append(stack.Pop());
                            repeat = true;
                        }
                    } while (repeat);
                }
                else if (c == ')')
                {
                    bool ok = false;
                    while (stack.Count > 0)
                    {
                        char p = stack.Pop();
                        if (p == '(')
                        {
                            ok = true;
                            break;
                        }
                        else
                        {
                            Append(p);
                        }
                    }
                    if (!ok)
                        throw new Exception("Unbalanced parentheses.");
                }
                else
                {
                    throw new Exception("Invalid character: " + c);
                }
            } while (sr.Peek() != -1);

            while (stack.Count > 0)
            {
                char p = stack.Pop();
                if (p == '(')
                    throw new Exception("Unbalanced parentheses.");
                Append(p);
            }

            return queue;
        }

        public static TreeNode ToTree(Queue<MathOpOrVal> q)
        {
            Stack<TreeNode> stack = new Stack<TreeNode>();

            foreach (MathOpOrVal mv in q)
            {
                if (mv.Value != null)
                {
                    stack.Push(new TreeNode(mv.Value.Value));
                }
                else
                {
                    TreeNode right = stack.Pop();
                    TreeNode left = stack.Pop();
                    stack.Push(new TreeNode(mv.Op.Value, left, right));
                }
            }
            return stack.Pop();
        }

        private static int Precedence(char c)
        {
            switch (c)
            {
                case '*':
                case '/':
                    return 2;
                case '+':
                case '-':
                    return 1;
                default:
                    throw new Exception(); //should not happen
            }
        }

        private static void Append(int value)
        {
            queue.Enqueue(new MathOpOrVal(value));
        }

        private static void Append(char value)
        {
            switch (value)
            {
                case '+':
                    queue.Enqueue(new MathOpOrVal(MathOp.Add));
                    break;
                case '-':
                    queue.Enqueue(new MathOpOrVal(MathOp.Sub));
                    break;
                case '*':
                    queue.Enqueue(new MathOpOrVal(MathOp.Mul));
                    break;
                case '/':
                    queue.Enqueue(new MathOpOrVal(MathOp.Div));
                    break;
                default:
                    throw new Exception("Parse error.");
            }
        }
    }

    internal class TreeNode
    {
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }
        public MathOp? Op { get; set; }
        public int? Value { get; set; }

        public TreeNode(MathOp op, TreeNode left, TreeNode right)
        {
            Op = op;
            Left = left;
            Right = right;
        }

        public TreeNode(int value)
        {
            Value = value;
        }

        public static void PrintTree(TreeNode tree)
        {
            PrintTree(tree, "");
        }

        public static void PrintTree(TreeNode tree, string spacing)
        {
            if (tree.Op != null)
            {
                Console.WriteLine(spacing + "+" + tree.Op);
                PrintTree(tree.Left, spacing + " ");
                PrintTree(tree.Right, spacing + " ");
            }
            else
                Console.WriteLine(spacing + tree.Value);
        }
    }

    internal class ILBuilder
    {
        public static Stack<MathOpOrVal> ILStack { get; set; }
        private static int i = 0;

        public static void BuildStack(Stack<MathOpOrVal> stack, TreeNode root)
        {
            if (root.Op != null)
            {
                MathOpOrVal mv = new MathOpOrVal(root.Op.Value);
                stack.Push(mv);
                BuildStack(stack, root.Right);
                BuildStack(stack, root.Left);
            }
            else
            {
                MathOpOrVal mv = new MathOpOrVal(root.Value.Value);
                stack.Push(mv);
            }
        }

        public static int Execute(TreeNode root)
        {
            DynamicMethod method = new DynamicMethod("Exec_" + i++,
                                                     typeof(int),
                                                     new Type[] { },
                                                     typeof(Program).Module);

            ILGenerator generator = method.GetILGenerator();

            ILStack = new Stack<MathOpOrVal>();
            BuildStack(ILStack, root);

            while (ILStack.Count > 0)
            {
                MathOpOrVal mv = ILStack.Pop();
                if (mv.Op != null)
                {
                    switch (mv.Op)
                    {
                        case MathOp.Add:
                            Console.WriteLine("add");
                            generator.Emit(OpCodes.Add);
                            break;

                        case MathOp.Sub:
                            Console.WriteLine("sub");
                            generator.Emit(OpCodes.Sub);
                            break;

                        case MathOp.Mul:
                            Console.WriteLine("mul");
                            generator.Emit(OpCodes.Mul);
                            break;

                        case MathOp.Div:
                            Console.WriteLine("div");
                            generator.Emit(OpCodes.Div);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ldc " + mv.Value.Value);
                    generator.Emit(OpCodes.Ldc_I4, mv.Value.Value);
                }
            }

            generator.Emit(OpCodes.Ret);

            return (int)method.Invoke(null,
                                      BindingFlags.ExactBinding,
                                      null,
                                      new object[] { },
                                      null);
        }
    }

    internal class MathOpOrVal
    {
        public MathOp? Op { get; set; }
        public int? Value { get; set; }

        public MathOpOrVal(int value)
        {
            Value = value;
        }

        public MathOpOrVal(MathOp op)
        {
            Op = op;
        }

        public override string ToString()
        {
            return (Op != null ? Op.Value.ToString() : Value.Value.ToString());
        }
    }

    internal enum MathOp
    {
        Add,
        Sub,
        Mul,
        Div
    }
}
