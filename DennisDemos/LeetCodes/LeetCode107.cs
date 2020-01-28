using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DennisDemos.LeetCodes
{
    /// <summary>
    /// 给定一个二叉树，返回其节点值自底向上的层次遍历。 （即按从叶子节点所在层到根节点所在的层，逐层从左向右遍历）
    /// 1. 每一层 new 一个list
    /// 2. 每一层的 node 要根据上层的两个节点获取
    /// </summary>
    public class LeetCode107
    {
        public IList<IList<int>> LevelOrderBottom(TreeNode root)
        {
            IList<IList<int>> results = new List<IList<int>>();

            Func(results, 0, root);


            for (int i = 0, j = results.Count() - 1; i < j; i++, j--)
            {
                var temp = results[i];
                results[i] = results[j];
                results[j] = temp;
            }

            return results;
        }

        private void Func(IList<IList<int>> results, int level, TreeNode node)
        {
            if (node == null)
            {
                return;
            }

            if (results.Count() == level)
            {
                List<int> list = new List<int>();
                list.Add(node.val);
                results.Add(list);
            }
            else
            {
                results[level].Add(node.val);
            }

            Func(results, level + 1, node.left);
            Func(results, level + 1, node.right);
        }
    }
}
