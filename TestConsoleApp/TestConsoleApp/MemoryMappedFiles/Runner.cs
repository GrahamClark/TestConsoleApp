using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;

namespace TestConsoleApp.MemoryMappedFiles
{
    class Runner : IRunner
    {
        private const string FileLocation = @"C:\gubbins\TestConsoleApp\TestConsoleApp\MemoryMappedFiles\input.txt";
        static readonly char[] FillerChars = new char[] { '\0' };

        public void RunProgram()
        {
            var fileContents = new List<string>();
            var trimmedLines = new List<string>();
            using (MemoryMappedFile mmFile = MemoryMappedFile.CreateFromFile(FileLocation))
            {

                Stream mmvStream = mmFile.CreateViewStream();
                Console.WriteLine("Stream length: {0}", mmvStream.Length);

                using (StreamReader sr = new StreamReader(mmvStream, ASCIIEncoding.ASCII))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        fileContents.Add(line);
                        trimmedLines.Add(line.Trim(FillerChars));
                    }
                }
            }

            Console.WriteLine("\nFile Contents:");
            for (int i = 0; i < fileContents.Count; i++)
            {
                Console.WriteLine("\tLine: {0}\tLength: {1}", i + 1, fileContents[i].Length);
            }

            Console.WriteLine("\nTrimmed Contents:");
            foreach (var item in trimmedLines)
            {
                Console.WriteLine("\t{0}", item);
            }
        }
    }
}
