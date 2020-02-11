using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode448
    {
        public IList<int> FindDisappearedNumbers(int[] nums)
        {
            List<int> res = new List<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                var valueAsIndex = Math.Abs(nums[i]) - 1;
                if (nums[valueAsIndex] > 0)
                {
                    nums[valueAsIndex] = -nums[valueAsIndex];
                }
            }

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] > 0)
                {
                    res.Add(i + 1);
                }
            }

            return res;
        }

    }
}
