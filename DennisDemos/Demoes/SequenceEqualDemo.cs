using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demoes
{
    public class SequenceEqualDemo : DemoBase
    {
        /// <summary>
        /// sequenceEqual 只比较两个list当中元素的值是否相等, 
        /// 以及元素的顺序是否相同.
        /// 不会比较元素在内存中的引用是否一样.
        /// </summary>
        public override void Run()
        {
            int a = 1, b = 2;
            string s1 = "a";
            string s2 = "b";
            List<int> listA = new List<int>() { a, b };
            List<string> listB = new List<string>() { "a", "b" };
            List<string> listD = new List<string>() { "a", "b" };
            List<int> listC = new List<int>() { a, b };
            if (listB.SequenceEqual(listD))
            {

            }

            if (Object.ReferenceEquals(listB, listD))
            {

            }
        }
    }
}
