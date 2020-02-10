using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode257
    {
        public IList<string> BinaryTreePaths(TreeNode root)
        {
            List<string> res = new List<string>();
            ConstructPaths(root, "", res);
            return res;
            
        }

        private void ConstructPaths(TreeNode node, string path, List<string> paths) 
        {
            if (node != null)
            {
                path += node.val;

                if (node.left == null && node.right == null)
                {
                    paths.Add(path);
                }
                else
                {
                    path += "->";
                    ConstructPaths(node.left, path, paths);
                    ConstructPaths(node.right, path, paths);
                }
            }
        }
    }
}
