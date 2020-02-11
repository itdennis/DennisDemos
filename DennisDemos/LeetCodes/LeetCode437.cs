using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode437
    {
        int pathNum = 0;
        public int PathSum(TreeNode root, int sum)
        {
            if (root == null)
            {
                return 0;
            }

            PathSum(root.left, sum);
            PathSum(root.right, sum);
            return pathNum;

        }

        private void Sum(TreeNode node, int sum) 
        {
            if (node == null)
            {
                return;
            }

            if (sum - node.val == 0)
            {
                pathNum++;
            }

            Sum(node.left, sum - node.val);
            Sum(node.right, sum - node.val);
        }
    }
}
