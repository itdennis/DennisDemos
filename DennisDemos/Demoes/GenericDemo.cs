using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos
{
    class GenericDemo
    {
        public void Run()
        {
            TestClass<int> intObj = new TestClass<int>();

            intObj.Add(1);
            intObj.Add(2);
            intObj.Add(3);
            intObj.Add(4);
            intObj.Add(5);

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(intObj[i]);
            }
            Console.ReadKey();
        }
    }
    public class TestClass<T>
    {
        private readonly T[] _obj = new T[5];
        public int Count = 0;

        public void Add(T item)
        {
            //checking length  
            if (Count + 1 < 6)
            {
                _obj[Count] = item;

            }
            Count++;
        }
        //foreach
        public T this[int index]
        {
            get => _obj[index];
            set => _obj[index] = value;
        }
    }
}
