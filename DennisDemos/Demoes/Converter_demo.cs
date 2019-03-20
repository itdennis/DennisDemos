using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demoes
{
    class Converter_demo : DemoBase
    {
        public override void Run()
        {
            Cat[] cats = {
                new Cat(name: "xiaoming1", age: 33),
                new Cat(name: "xiaoming2", age: 34),
                new Cat(name: "xiaoming3", age: 35)
            };

            string[] catsName = Array.ConvertAll(cats, new Converter<Cat, string>(GetName));
            Action<string> messageAction  = s => Console.WriteLine(s);
            foreach (var s in catsName)
            {
                messageAction(s);
            }
        }

        public string GetName(Cat cat)
        {
            return cat.Name;
        }
    }

    public class Cat
    {
        public string Name { get; }
        public int Age { get; }
        public Cat(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }
    }
}
