using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DennisDemos.LeetCodes
{
    public class LeetCode125
    {
        public bool IsPalindrome(string s)
        {
            if (null == s)
            {
                return true;
            }
            var charArray = s.ToCharArray();

            int r = 0, l = charArray.Length - 1;
            while (r < l)
            {
                if (charArray[r] == charArray[l])
                {
                    r++;
                    l--;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
