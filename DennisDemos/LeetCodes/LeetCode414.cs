using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode414
    {
        public int ThirdMax(int[] nums)
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
                return nums[0] > nums[1] ? nums[0] : nums[1];
            }

            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = i+1; j < nums.Length; j++)
                {
                    if (nums[i] < nums[j])
                    {
                        var temp = nums[j];
                        nums[j] = nums[i];
                        nums[i] = temp;
                    }
                }
            }

            int bigCount = 1;
            int bigNum = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                if (bigNum != nums[i])
                {
                    bigNum = nums[i];
                    bigCount++;
                    if (bigCount == 3)
                    {
                        break;
                    }
                }
            }
            return bigCount == 3 ? bigNum : nums[0];
        }
    }
}
