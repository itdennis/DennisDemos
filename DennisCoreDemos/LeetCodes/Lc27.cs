using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DennisCoreDemos.LeetCodes
{
    public class Lc27
    {
        public static async Task<int> Run(int[] nums, int val)
        {
            Task<int> task = new Task<int>(() => { return DoOps(nums, val); });
            task.Start();
            return await task;
        }

        private static int DoOps(int[] nums, int val)
        {
            int i = 0;
            int n = nums.Length;
            while (i < n)
            {
                if (nums[i] == val)
                {
                    nums[i] = nums[n - 1];
                    // reduce array size by one
                    n--;
                }
                else
                {
                    i++;
                }
            }
            return n;
        }
    }
}
