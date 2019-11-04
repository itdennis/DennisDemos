using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.JustForTest
{
    public class Solution
    {
        public int Reverse(int x)
        {
            if (x == 0) return 0;
            bool inputLessThan0 = x < 0 ? true : false;

            try
            {
                int testInputIs32Bit;
                x = System.Math.Abs(x);
                if (!int.TryParse(x.ToString(), out testInputIs32Bit))
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }

            System.Collections.Generic.List<int> inversedNumCache = new System.Collections.Generic.List<int>();
            int y;
            while (x > 0)
            {
                y = x % 10;
                x = x / 10;
                inversedNumCache.Add(y);
            }

            int tenNum = inversedNumCache.Count - 1;
            int res = 0;
            try
            {
                foreach (var inversedNum in inversedNumCache)
                {
                    res += Convert.ToInt32(inversedNum * System.Math.Pow(10, tenNum));
                    tenNum--;
                }
            }
            catch (Exception)
            {
                return 0;
            }
            return inputLessThan0 ? -res : res;
        }
    }
}
