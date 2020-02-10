using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode283
    {
        public void MoveZeroes(int[] nums)
        {
            var numsSize = nums.Length;
            int i = 0, j = 0;
            for (i = 0; i < numsSize; i++)
            {
                if (nums[i] != 0)
                {
                    nums[j++] = nums[i];
                }
            }
            while (j < numsSize)
            {
                nums[j++] = 0;
            }
        }
    }
}
