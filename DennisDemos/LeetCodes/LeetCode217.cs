using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode217
    {
        public bool ContainsDuplicate(int[] nums)
        {
            HashSet<int> set = new HashSet<int>(nums.Length);
            foreach (var num in nums)
            {
                if (set.Contains(num))
                {
                    return true;
                }
                else
                {
                    set.Add(num);
                }
            }
            return false;
        }
    }
}
