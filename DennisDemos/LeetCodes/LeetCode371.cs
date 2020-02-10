using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode371
    {
        public int GetSum(int a, int b)
        {
            var num1 = a ^ b;
            var num2 = a & b;

            if (num2 != 0)
            {
                num1 = GetSum(num1, num2 << 1);
            }

            return num1;
        }
    }
}
