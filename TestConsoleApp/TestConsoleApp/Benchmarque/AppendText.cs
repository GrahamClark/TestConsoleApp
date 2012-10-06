using System;
using System.Text;

namespace TestConsoleApp.Benchmarque
{
    public interface IAppendText
    {
        string Append(params string[] args);
    }

    class ConcatAppendText : IAppendText
    {
        public string Append(params string[] args)
        {
            string result = String.Empty;

            for (int i = 0; i < args.Length; i++)
            {
                result += args[i];
            }

            return result;
        }
    }

    class StringBuilderAppendText : IAppendText
    {
        public string Append(params string[] args)
        {
            var builder = new StringBuilder();

            for (int i = 0; i < args.Length; i++)
            {
                builder.Append(args[i]);
            }

            return builder.ToString();
        }
    }

    class JoinAppendText : IAppendText
    {
        public string Append(params string[] args)
        {
            return String.Join("", args);
        }
    }

    class StringBuilderForeachAppendText : IAppendText
    {
        public string Append(params string[] args)
        {
            var builder = new StringBuilder();

            foreach (var item in args)
            {
                builder.Append(item);
            }

            return builder.ToString();
        }
    }
}
