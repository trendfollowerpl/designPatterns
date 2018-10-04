using System;

namespace Adapter
{
    class Program
    {
        static void Main(string[] args)
        {
            var first = new Adaptee();
            Console.Write("Before the new standard\nPrecise reading: ");
            Console.WriteLine(first.SpecificRequest(5, 3));
            ITarget second = new Adapter();
            Console.WriteLine("\nMoving to the new standard");
            Console.WriteLine(second.Request(5));

            Console.ReadLine();
        }
    }

    class Adaptee
    {
        public double SpecificRequest(double a, double b)
        {
            return a / b;
        }
    }

    interface ITarget
    {
        string Request(int i);
    }

    class Adapter : Adaptee, ITarget
    {
        public string Request(int i)
        {
            return "Rough estimate is " + (int)Math.Round(SpecificRequest(i, 3));
        }
    }
}
