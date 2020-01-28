using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode111
    {

        public int MinDepth(TreeNode root)
        {
            if (null  == root)
            {
                return 0;
            }

            // null节点不参与比较
            if (root.left == null && root.right != null)
            {
                return 1 + MinDepth(root.right);
            }
            // null节点不参与比较
            if (root.right == null && root.left != null)
            {
                return 1 + MinDepth(root.left);
            }

            return 1 + Math.Min(MinDepth(root.left), MinDepth(root.right));

        }
    }
}
