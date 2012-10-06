using System.Diagnostics.Contracts;

namespace TestConsoleApp.CodeContracts
{
    class Rational
    {
        int numerator;
        int denominator;

        public Rational(int numerator, int denominator)
        {
            Contract.Requires(denominator != 0);

            this.numerator = numerator;
            this.denominator = denominator;
        }

        public int Denominator
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() != 0);

                return this.denominator;
            }
        }

        [ContractInvariantMethod]
        void ObjectInvariant()
        {
            Contract.Invariant(this.denominator != 0);
        }
    }
}
