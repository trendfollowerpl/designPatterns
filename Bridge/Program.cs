using System;

namespace Bridge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bridge Pattern");
            Console.WriteLine(new Abstraction(new ImplementationA()).Operation());
            Console.WriteLine(new Abstraction(new ImplementationB()).Operation());
            Console.ReadLine();
        }
    }

    interface Bridge
    {
        string OperationImp();
    }

    class Abstraction
    {
        private Bridge bridge;

        public Abstraction(Bridge implementation)
        {
            bridge = implementation;
        }

        public string Operation()
        {
            return "Abstraction" + "<<<BRIDGE>>>" + bridge.OperationImp();
        }
    }

    class ImplementationA : Bridge
    {
        public string OperationImp()
        {
            return "ImplementationA";
        }
    }

    class ImplementationB : Bridge
    {
        public string OperationImp()
        {
            return "ImplementationB";
        }
    }
}
