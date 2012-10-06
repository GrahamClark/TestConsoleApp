using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace TestConsoleApp.Grammars
{
    sealed class Grammar
    {
        private Dictionary<string, List<Tuple<string, string>>> rules;
        private Dictionary<Tuple<string, int>, List<string>> memorizer = new Dictionary<Tuple<string, int>, List<string>>();

        private static string[] empty = { };

        public Grammar(string g)
        {
            rules = new Dictionary<string, List<Tuple<string, string>>>();
            foreach (string s in g.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
            {
                int i = s.IndexOf(':');
                if (i < 0)
                {
                    continue;
                }

                var name = s.Substring(0, i).Trim();
                var list = new List<Tuple<string, string>>();

                foreach (var o in s.Substring(i + 1).Split('|'))
                {
                    var words = o.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    Debug.Assert(words.Length == 2);
                    list.Add(Tuple.Create(words[0], words[1]));
                }

                rules.Add(name, list);
            }
        }

        public IEnumerable<string> All(string symbol, int substitutions)
        {
            List<string> results;
            var tuple = Tuple.Create(symbol, substitutions);
            if (!memorizer.TryGetValue(tuple, out results))
            {
                results = AllCore(symbol, substitutions).ToList();
                memorizer.Add(tuple, results);
            }

            return results;
        }

        public IEnumerable<string> AllCore(string symbol, int substitutions)
        {
            bool terminal = !rules.ContainsKey(symbol);
            if (substitutions == 0)
            {
                // No more substitutions; we'd better have a terminal.
                if (terminal)
                {
                    return new string[] { symbol == "NIL" ? String.Empty : symbol + " " };
                }

                return empty;
            }

            // We're doing substitutions; we'd better have a nonterminal.
            if (terminal)
            {
                return empty;
            }

            return from r in rules[symbol]
                   from i in Enumerable.Range(0, substitutions)
                   from left in this.All(r.Item1, i)
                   from right in this.All(r.Item2, substitutions - i - 1)
                   select left + right;
        }
    }
}
