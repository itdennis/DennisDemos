using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demoes
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

        
        public void RunCSharpInDeepDemo()
        {
            // Aggregate demo
            string[] words = { "zero", "one", "two", "threee", "four" };
            int[] numbers = { 0, 1, 2, 3, 4 };
            numbers.Sum();
            numbers.Count();
            numbers.Average();
            numbers.LongCount(x => x % 2 == 0);
            words.Min(w => w.Length);
            words.Max(w => w.Length);

            //
            object[] allStrings = { "There", "are", "all", "strings" };
            object[] notAllStrings = { "Number", "at", "the", "end", "5" };
            allStrings.Cast<string>();


        }
        
    }
}
