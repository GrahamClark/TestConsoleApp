using System;
using System.Collections.Generic;

namespace TestConsoleApp.AutoMapperExample.B
{
    public class MagicThing
    {
        public string Name { get; set; }

        public IEnumerable<int> Numbers { get; set; }

        public DateTime DateLastModified { get; set; }
    }
}
