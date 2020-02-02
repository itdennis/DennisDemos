using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode193
    {
        /// <summary>
        /// f(k) = Max(f(k-2)+Ak , f(k-1))
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int Rob(int[] nums)
        {
            if (nums.Length == 0)
            {
                return 0;
            }

            if (nums.Length == 1)
            {
                return nums[0];
            }

            if (nums.Length == 2)
            {
                return Math.Max(nums[0], nums[1]);
            }

            int prevMax = 0;
            int currMax = 0;
            foreach (var num in nums)
            {
                int temp = currMax;
                currMax = Math.Max(prevMax + num, currMax);
                prevMax = temp;
            }

            return currMax;
        }
    }
}
