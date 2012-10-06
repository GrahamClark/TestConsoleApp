using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TestConsoleApp.CovarianceContravariance.Entities;

namespace TestConsoleApp.CovarianceContravariance
{
    class Runner : IRunner
    {
        static Giraffe MakeGiraffe()
        {
            return new Giraffe();
        }

        void Foo(Giraffe g)
        { }

        void Bar(Animal a)
        { }

        delegate void Something<in A>(A a);

        delegate void Meta<out A>(Something<A> action);

        public void RunProgram()
        {
            BrokenArrayCovariance();

            MethodGroupCovariance();

            ExpressionCovarianceAndContravariance();

            HigherOrderFunctions();

            InterfaceVariance();
        }

        private static void BrokenArrayCovariance()
        {
            try
            {
                Animal[] animals = new Giraffe[10];
                animals[0] = new Turtle();
            }
            catch (ArrayTypeMismatchException ex)
            {
                Console.WriteLine(String.Format("Illegal:\n{0}\n\n", ex.ToString()));
            }
        }

        private void MethodGroupCovariance()
        {
            // define a delegate that returns an Animal - MakeGiraffe fits.
            // method group to delegate conversions are covariant in their return types.
            Func<Animal> func = MakeGiraffe;

            // illegal - action1 could take a Tiger as a parameter, but Foo only accepts Giraffes.
            // method group to delegate conversions are contravariant in their argument types.
            // Action<Mammal> action1 = Foo;

            // action2 takes a Mammal as a parameter - Bar takes an Animal so that's ok.
            Action<Mammal> action2 = Bar;

            try
            {
                // ok but won't work
                Tiger t = (Tiger)func();
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine(string.Format("Illegal:\n{0}\n\n", ex.ToString()));
            }
        }

        private void ExpressionCovarianceAndContravariance()
        {
            // legal from C# 4
            // f1's parameter (of type Animal) is contravariant, and its return type (of type Giraffe) is covariant.
            Func<Animal, Giraffe> f1 = x => (Giraffe)x;
            Func<Mammal, Mammal> f2 = f1;

            // because of the "in" modifier in the delegate declaration, the parameter is contravariant, 
            // so we can pass in a smaller type without error.
            Something<Animal> contra1 = (Animal a) => Console.WriteLine(a.LatinName);
            Something<Giraffe> contra2 = contra1;
        }

        private void HigherOrderFunctions()
        {
            Something<Animal> contra1 = (Animal a) => Console.WriteLine(a.LatinName);
            Meta<Mammal> meta1 = (Something<Mammal> action) => action(new Giraffe());

            // this is legal because Something<Animal> is smaller than Something<Mammal> (?)
            meta1(contra1);

            Something<Tiger> contra2 = tiger => tiger.Growl();
            // This is illegal, otherwise we'd end up calling (new Giraffe()).Growl();
            // Meta<A> is not contravariant in A, but it is covariant.
            //Meta<Tiger> meta2 = meta1;
            //meta2(contra2);

            Meta<Animal> meta3 = meta1;
        }

        private void InterfaceVariance()
        {
            List<Giraffe> giraffes = new List<Giraffe>() { new Giraffe() { Age = 10, Hungry = true }, new Giraffe() { Age = 4 } };
            IEnumerable<Giraffe> adultGiraffes = from g in giraffes where g.Age > 5 select g;

            // both these work in .NET 4 as IEnumerable<T> is IEnumerable<out T>, i.e. covariant in T, so more smaller 
            // types can be passed in.
            FeedAnimals(adultGiraffes);
            FeedAnimals(giraffes);
        }

        private void FeedAnimals(IEnumerable<Animal> animals)
        {
            foreach (Animal animal in animals)
            {
                if (animal.Hungry)
                    Feed(animal);
            }

            IEnumerable<Animal> a = new C();
            Console.WriteLine(a.First().GetType().ToString());
        }

        private void Feed(Animal animal)
        {
        }
    }

    class C : IEnumerable<Turtle>, IEnumerable<Giraffe>
    {
        IEnumerator<Giraffe> IEnumerable<Giraffe>.GetEnumerator()
        {
            yield return new Giraffe();
        }

        IEnumerator<Turtle> IEnumerable<Turtle>.GetEnumerator()
        {
            yield return new Turtle();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
