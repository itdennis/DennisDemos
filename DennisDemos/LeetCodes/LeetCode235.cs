using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode235
    {
        public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            if ((p.val < root.val && q.val > root.val) || (p.val > root.val && q.val < root.val))
            {
                return root;
            }
            else if (p.val <= root.val && q.val <= root.val)
            {
                if (p.val == root.val || q.val == root.val)
                {
                    return root;
                }
                return LowestCommonAncestor(root.left, p, q);
            }
            else
            {
                if (p.val == root.val || q.val == root.val)
                {
                    return root;
                }
                return LowestCommonAncestor(root.right, p, q);
            }
        }
    }
}
