using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DennisCoreDemos.LeetCodes
{
    public class Lc69
    {
        public static async Task<int> Run(int x)
        {
            Task<int> task = new Task<int>(() => { return DoOps(x); });
            task.Start();
            return await task;
        }

        private static int DoOps(int x)
        {
            int left = 0;
            int right = x;
            while (left < right)
            {
                int mid = left + (right - left + 1) / 2;
                long squre = mid * mid;
                if (squre > x)
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid;
                }
            }
            return left;
        }
    }
}
