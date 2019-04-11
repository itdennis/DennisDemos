using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DennisDemos.Demos
{
    public class RegexDemo
    {
        private static readonly Regex WoodblocksAdsFedPModelRegex = new Regex("((?<FeatureId>[0-9]+):(?<SlotId>[0-9]+))");
        public void Run()
        {
            
            string input = "{\"2646\":\"27\"}";

            switch (input)
            {
                case "1":
                case "2":
                    // do something;
                    break;
                default: break;
            }


            var newInput = Regex.Replace(input, @"[\""\{\}]", "");
            var m = WoodblocksAdsFedPModelRegex.Matches(input);
            if (m.Count == 0)
            {
                Console.WriteLine("error.");
            }
            else
            {
                Console.WriteLine("yes.");
            }
            Console.ReadKey();
        }
    }
}
