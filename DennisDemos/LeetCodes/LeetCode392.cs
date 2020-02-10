using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode392
    {
        public bool IsSubsequence(string s, string t)
        {
            var sArray = s.ToCharArray().ToList();
            int index = -1;
            foreach (var c in sArray)
            {
                index = t.IndexOf(c, index + 1);
                if (index == -1)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
