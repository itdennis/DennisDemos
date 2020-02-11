using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode441
    {
        public int ArrangeCoins(int n)
        {
            if (n == 0)
            {
                return 0;
            }
            int row = 1;
            while (n - row >= (row + 1))
            {
                n = n - row; row++;
            }

            return row;
        }
    }
}
