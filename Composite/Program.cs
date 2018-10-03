using System;
using System.Collections;
using System.Collections.Generic;

namespace Composite
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public interface IComponent<T>
    {
        void Add(IComponent<T> c);
        IComponent<T> Remove(T c);
        IComponent<T> Find(T c);
        string Display(int depth);
        T Name { get; set; }
    }

    public class Component<T> : IComponent<T>
    {
        public Component(T name)
        {
            Name = name;
        }

        public T Name { get; set; }

        public void Add(IComponent<T> c)
        {
            Console.WriteLine("Cannot add to an item");
        }

        public string Display(int depth)
        {
            return new String('-', depth) + Name + "\n";
        }

        public IComponent<T> Find(T c)
        {
            if (c.Equals(Name))
                return this;

            return null;
        }

        public IComponent<T> Remove(T c)
        {
            Console.WriteLine("Cannot remove directly");
            return this;
        }
    }

    public class Composite<T> : IComponent<T>
    {
        private IList<IComponent<T>> list;

        public Composite(T name)
        {
            Name = name;
            list = new List<IComponent<T>>();
        }

        public T Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Add(IComponent<T> c)
        {
            list.Add(c);
        }

        public string Display(int depth)
        {
            throw new NotImplementedException();
        }

        public IComponent<T> Find(T s)
        {
            if (Name.Equals(s)) return this;
            IComponent<T> found = null;
            foreach (IComponent<T> c in list)
            {
                found = c.Find(s);
                if (found != null)
                    break;
            }
            return found;

        }

        public IComponent<T> Remove(T c)
        {
            var p = this.Find(c);
            var holder = this;

            if (holder == null) return this;
            holder.list.Remove(p);
            return holder;
        }


    }
}
