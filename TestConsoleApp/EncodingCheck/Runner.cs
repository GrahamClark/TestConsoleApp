using System;
using System.IO;

namespace TestConsoleApp.EncodingCheck
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            GetEncodingFromFile("../../ansi.txt");
            GetEncodingFromFile("../../iso.txt");
            GetEncodingFromFile("../../unicode.txt");
            GetEncodingFromFile("../../utf-7.txt");
            GetEncodingFromFile("../../utf-8.txt");
            GetEncodingFromFile("../../utf-16-LE.txt");
            GetEncodingFromFile("../../utf-16-LE-BOM.txt");
        }

        private static void GetEncodingFromFile(string fileName)
        {
            using (FileStream file = File.Open(fileName, FileMode.Open))
            {
                byte[] buffer = new byte[file.Length];
                file.Read(buffer, 0, buffer.Length);

                Console.WriteLine(EncodingDetector.GetEncoding(buffer));
            }
        }
    }
}
