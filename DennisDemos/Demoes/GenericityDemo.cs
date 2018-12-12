using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demos
{
    class GenericityDemo
    {
        public void Run()
        {
            List<int> intergers = new List<int>();
            intergers.Add(1);
            intergers.Add(2);
            intergers.Add(3);
            intergers.Add(4);
            intergers.Add(5);
            Converter<int, double> converter = (x) => { return Math.Sqrt(x); };
            var o = CreateInstance<int>();

            Dictionary<string, string> dictionary = new Dictionary<string, string>(100);
        }

        public T CreateInstance<T>() where T : new()
        {
            return new T();
        }
    }

    class TypeWithField<T>
    {
        public static string field;
        public static void PrintField()
        {
            Console.WriteLine(field + ":" + typeof(T).Name);
        }
    }

    class CountingEnumerable : IEnumerable<int>
    {
        public IEnumerator<int> GetEnumerator()
        {
            return new CountingEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    class CountingEnumerator : IEnumerator<int>
    {
        public int Current = -1;

        object IEnumerator.Current { get { return this.Current; } }

        int IEnumerator<int>.Current => throw new NotImplementedException();

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            Current++;
            return Current < 10;
        }

        public void Reset()
        {
            Current = -1;
        }
    }
    
    
}
