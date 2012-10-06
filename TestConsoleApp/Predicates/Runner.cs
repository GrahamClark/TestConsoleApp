using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsoleApp.Predicates
{
    internal class Runner : IRunner
    {
        public void RunProgram()
        {
            List<Paragraph> paragraphs = new List<Paragraph>()
            {
                new Paragraph() { Text = "hello", Styles = new Style[] { Style.Bold } },
                new Paragraph() { Text = "there", Styles = new Style[] { Style.Italic, Style.Bold } },
                new Paragraph() { Text = "to", Styles = new Style[] { Style.Italic, Style.Superscript, Style.Bold } },
                new Paragraph() { Text = "you", Styles = new Style[] { Style.Superscript, Style.Bold } }
            };

            //3 identical methods

            Console.WriteLine(paragraphs.FindIndex(
                delegate(Paragraph p)
                {
                    foreach (var style in p.Styles)
                    {
                        if (style == Style.Superscript)
                        {
                            return true;
                        }
                    }
                    return false;
                }));

            Console.WriteLine(paragraphs.FindIndex(new Predicate<Paragraph>(HasSuperscript)));

            Console.WriteLine(paragraphs.FindIndex(HasSuperscript));
        }

        internal bool HasSuperscript(Paragraph p)
        {
            foreach (var style in p.Styles)
            {
                if (style == Style.Superscript)
                {
                    return true;
                }
            }
            return false;
        }
    }

    class Paragraph
    {
        public string Text { get; set; }
        public Style[] Styles { get; set; }
    }

    enum Style
    {
        Bold,
        Italic,
        Superscript
    }
}
