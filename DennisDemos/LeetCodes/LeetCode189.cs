using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode189
    {
        public void Rotate(int[] nums, int k)
        {
            if (nums.Length == 0)
            {
                return;
            }
            if (k < 0)
            {
                return;
            }

            int[] newNums = new int[nums.Length];

            for (int i = 0; i < nums.Length; i++)
            {
                var targetIndex = i + k;
                while (targetIndex >= nums.Length)
                {
                    targetIndex = targetIndex - nums.Length;
                }
                newNums[targetIndex] = nums[i];
            }
            for (int i = 0; i < newNums.Length; i++)
            {
                nums[i] = newNums[i];
            }
        }
    }
}
