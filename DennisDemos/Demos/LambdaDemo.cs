using DennisDemos.DemosHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demos
{
    class LambdaDemo
    {
        public void Run()
        {
            //Func<int> func = () => { return 1; };
            //List<Product> products = Product.GetSampleProducts();
            //Predicate<Product> test = delegate (Product p) { return p.Price > 10m; };
            //List<Product> matches = products.FindAll(test);
            //Action<Product> print = Console.WriteLine;
            //matches.ForEach(print);

            Func<int, int, string> func = (x, y) => (x + y).ToString();
            Console.WriteLine(func(5,6));
            dynamic o = "hello";
            Console.WriteLine(o.Length);
            o = new string[] { "hi", "helllo" };
            Console.WriteLine(o.Length);


        }
    }
}
