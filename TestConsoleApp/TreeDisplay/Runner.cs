﻿using System;
using System.IO;

namespace TestConsoleApp.TreeDisplay
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            var root = 
                new Node(
                    "a",
                    new Node(
                        "b",
                        new Node(
                            "c",
                            new Node("d")
                        ),
                        new Node(
                            "e",
                            new Node("f")
                        )
                    ),
                    new Node(
                        "g",
                        new Node(
                            "h",
                            new Node("i")
                        ),
                        new Node("j")
                    )
                );

            string output = DepthFirstDumper.Dump(root);
            File.WriteAllText(@"C:\tmp\treedump.txt", output);
            Console.WriteLine(output);
        }
    }
}
