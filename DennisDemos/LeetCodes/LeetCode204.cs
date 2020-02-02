using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode204
    {
        public int CountPrimes(int n)
        {
            bool[] isPrim = new bool[n];
            for (int i = 0; i < isPrim.Length; i++)
            {
                isPrim[i] = true;
            }

            for (int i = 2; i * i < n; i++)
                if (isPrim[i])
                    for (int j = i * i; j < n; j += i)
                        isPrim[j] = false;

            int count = 0;
            for (int i = 2; i < n; i++)
                if (isPrim[i]) count++;

            return count;
        }
    }
}
