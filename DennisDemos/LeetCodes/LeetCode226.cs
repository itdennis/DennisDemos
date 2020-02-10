using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode226
    {
        public TreeNode InvertTree(TreeNode root)
        {
            if (root == null)
            {
                return root;
            }

            var tempNode = root.left;
            root.left = root.right;
            root.right = tempNode;

            if (root.left != null)
            {
                InvertTree(root.left);
            }

            if (root.right != null)
            {
                InvertTree(root.right);
            }

            return root;
        }
    }
}
