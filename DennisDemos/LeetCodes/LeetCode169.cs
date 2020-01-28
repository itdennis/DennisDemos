using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode169
    {
        public int MajorityElement(int[] nums)
        {
            int count = 0;
            int res = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (count == 0)
                {
                    res = nums[i];
                    count++;
                }
                else if(res == nums[i])
                {
                    count++;
                }
                else
                {
                    count--;
                }
            }
            return res;
        }
    }
}
