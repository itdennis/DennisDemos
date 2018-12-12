using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demos
{
    public class LinqDemo
    {
        public void Run()
        {
            var collection = Enumerable.Range(0, 10)
                .Where(x => x%2!=0)
                .Select(x => new { Original =x, SquareRoot = Math.Sqrt(x)});
            foreach (var element in collection)
            {
                Console.WriteLine("sqrt({0}) = {1}",
                    element.Original,
                    element.SquareRoot);
            }
        }
        
    }
}
