using System;

namespace TestConsoleApp.StringFormat
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            string format = "number one is {1}, and the fourth is {4}";
            Console.WriteLine(string.Format(format, null, "first", null, null, "second"));

            DateTime d = DateTime.Now;
            Console.WriteLine(First(d));
            Console.WriteLine(Second(d));
        }

        /// <remarks>
        /// Mulitple calls to Environment.NewLine means the IL generated is much larger: each individual string and 
        /// Environment.NewLine call are put in a string array, which is then concatenated into one string. Then Format
        /// is called.
        /// </remarks>
        static string First(DateTime d)
        {
            return String.Format("first string" + Environment.NewLine + "second string" + Environment.NewLine + "{0}", d.ToShortTimeString());
        }

        /// <remarks>
        /// Replacing the multiple calls to Environment.NewLine with a token slims down the IL: the two strings are
        /// inlined by the compiler, so one string is loaded, one call to Environment.NewLine is made, and then the call
        /// to Format. No arrays or concatenation.
        /// </remarks>
        static string Second(DateTime d)
        {
            return String.Format("1st string{1}" + "2nd string{1}{0}", d.ToShortTimeString(), Environment.NewLine);
        }
    }
}
