import Utils.PythonTreeNode

class Solution:
    def findTilt(self, root):
        self.ans = 0
        def traverse(root):
            if root == None:
                return 0
            L = traverse(root.left)
            R = traverse(root.right)
            self.ans += abs(L - R)
            return L + R + root.val
        traverse(root)
        return self.ans

node1 = Utils.PythonTreeNode.PythonTreeNode(1)
node2 = Utils.PythonTreeNode.PythonTreeNode(2)
node3 = Utils.PythonTreeNode.PythonTreeNode(3)
node1.left = node2
node1.right = node3

s = Solution()
s.findTilt(node1)