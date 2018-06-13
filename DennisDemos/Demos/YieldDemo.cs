using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demos
{
    /// <summary>
    /// 使用yield之后会对降低程序对于内存的消耗.
    /// </summary>
    class YieldDemo
    {
        private int i = 0;
        private int j = 0;
        public void Run()
        {

            foreach (string line in ReadLines("LogicDocument_DocAveLoadBalance.docx"))
            {
                Console.WriteLine("I ======" + i++);
            }
        }
        public IEnumerable<string> ReadLines(string fileName)
        {
            return ReadLines(fileName, Encoding.UTF8);
        }
        public IEnumerable<string> ReadLines(string fileName, Encoding encoding)
        {
            return ReadLines(() => { return File.OpenText(fileName); }
                );
        }
        public IEnumerable<string> ReadLines(Func<TextReader> provider)
        {
            using (TextReader reader = provider())
            {
                string line;
                while ((line = reader.ReadLine())!=null)
                {
                    Console.WriteLine("J ======" + j++);
                    yield return line;
                }
            }
        }

        Action<string> action = (string text) => 
        {
            text.ToString();
        };
    }
}
