using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            PrototypeManager manager = new PrototypeManager();
            Prototype c2, c3;

            c2 = manager.prototypes["Australia"].Clone();
            PrototypeClient.Report("Shallow cloning Australia\n===============",
            manager.prototypes["Australia"], c2);

            c2.Capital = "Sydney";
            PrototypeClient.Report("Altered Clone's shallow state, prototype unaffected",
                manager.prototypes["Australia"], c2);

            c2.Language.Data = "Chinese";
            PrototypeClient.Report("Altering Clone deep state: prototype affected * ****",
                manager.prototypes["Australia"], c2);

            // Make a copy of Germany's data
            c3 = manager.prototypes["Germany"].DeepCopy();
            PrototypeClient.Report("Deep cloning Germany\n============",
                manager.prototypes["Germany"], c3);


            // Change the capital of Germany
            c3.Capital = "Munich";
            PrototypeClient.Report("Altering Clone shallow state, prototype unaffected",
                manager.prototypes["Germany"], c3);

            // Change the language of Germany (deep data)
            c3.Language.Data = "Turkish";
             PrototypeClient.Report("Altering Clone deep state, prototype unaffected",
                 manager.prototypes["Germany"], c3);


            Console.ReadLine();
        }
    }

    [Serializable]
    public abstract class IPrototype<T>
    {
        //shallow Copy
        public T Clone()
        {
            return (T)this.MemberwiseClone();
        }

        //Deep copy
        public T DeepCopy()
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Seek(0, SeekOrigin.Begin);
            T copy = (T)formatter.Deserialize(stream);
            stream.Close();
            return copy;
        }
    }

    [Serializable]
    class DeeperData
    {
        public string Data { get; set; }

        public DeeperData(string s)
        {
            Data = s;
        }

        public override string ToString()
        {
            return Data;
        }
    }
    [Serializable]
    class Prototype : IPrototype<Prototype>
    {
        public string Country { get; set; }
        public string Capital { get; set; }
        public DeeperData Language { get; set; }

        public Prototype(string country, string capital, string language)
        {
            Country = country;
            Capital = capital;
            Language = new DeeperData(language);
        }

        public override string ToString()
        {
            return Country + "\t\t" + Capital + "\t\t->" + Language;
        }
    }

    class PrototypeManager : IPrototype<Prototype>
    {
        public Dictionary<string, Prototype> prototypes = new Dictionary<string, Prototype>
        {
            ["Germany"] = new Prototype("Germany", "Berlin", "German"),
            ["Italy"] = new Prototype("Italy", "Rome", "Italian"),
            ["Australia"] = new Prototype("Australia", "Canberra", "English")
        };
    }

    class PrototypeClient : IPrototype<Prototype>
    {
        public static void Report(string s, Prototype a, Prototype b)
        {
            Console.WriteLine("\n" + s);
            Console.WriteLine("Prototype " + a + "\nClone " + b);
        }
    }
}
