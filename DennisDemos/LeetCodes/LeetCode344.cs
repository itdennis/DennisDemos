using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode344
    {
        public void ReverseString(char[] s)
        {
            for (int i = 0, j = s.Length -1; i < s.Length; i++, j--)
            {
                if (i <= j)
                {
                    var temp = s[i];
                    s[i] = s[j];
                    s[j] = temp;
                }
            }
        }
    }
}
