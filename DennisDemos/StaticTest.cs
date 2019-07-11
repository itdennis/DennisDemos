using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos
{
    public class StaticTest
    {
        private static Dictionary<string, string> staticDic = new Dictionary<string, string>();

        public StaticTest()
        {
        }

        public void Run(string key, string value)
        {
            if (staticDic.ContainsKey(key))
            {
                Console.WriteLine($"the static dic contains key: {key}");
            }
            else
            {
                staticDic.Add(key, value);
            }
        }
    }
}
