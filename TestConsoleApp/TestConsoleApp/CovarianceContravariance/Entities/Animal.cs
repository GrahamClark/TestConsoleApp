using System.Collections.Generic;
namespace TestConsoleApp.CovarianceContravariance.Entities
{
    class Animal : IComparer<Animal>
    {
        private string latinName = null;

        public string LatinName
        {
            get { return this.latinName ?? "latin name here."; }
            set { this.latinName = value; }
        }

        public bool Hungry { get; set; }

        public int Age { get; set; }

        public int Compare(Animal x, Animal y)
        {
            int nameCompare = x.LatinName.CompareTo(y.LatinName);
            int ageCompare = x.Age.CompareTo(y.Age);

            if (nameCompare != 0)
            {
                return nameCompare;
            }
            else if (ageCompare != 0)
            {
                return ageCompare;
            }
            else
            {
                return 0;
            }
        }
    }
}
