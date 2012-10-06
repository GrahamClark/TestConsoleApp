using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsoleApp.Rounding
{
    class Runner : IRunner
    {
        private const double centimetresInInch = 2.54;
        private const int inchesInFoot = 12;
        
        public void RunProgram()
        {
            int feet = 6, inches = 7;
            Console.WriteLine(string.Format("Feet = {0}, Inches = {1}", feet, inches));

            double centimetres = ((feet * inchesInFoot) + inches) * centimetresInInch;
            Console.WriteLine("Un-rounded centimetres = " + centimetres);

            short castedCentimetres = (short)centimetres;
            Console.WriteLine("Casted centimetres = " + castedCentimetres);

            double roundedCentimetres = Math.Round(centimetres, 0);
            Console.WriteLine("Rounded centimetres = " + roundedCentimetres);
        }
    }
}
