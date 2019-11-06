using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DennisCoreDemos.LeetCodes
{
    public class Lc35
    {
        public static async Task<int> Run(int[] nums, int target)
        {
            Task<int> task = new Task<int>(() => { return DoOps(nums, target); });
            task.Start();
            return await task;
        }

        private static int DoOps(int[] nums, int target)
        {
            int len = nums.Length;
            if (len == 0)
            {
                return len;
            }
            int left = 0;
            int right = len;
            
            while (left < right)
            {
                int mid = left + (right - left) / 2;
                if (nums[mid] < target)
                {
                    left = mid + 1;
                }
                else
                    right = mid;
            }
            return left;
        }
    }
}
