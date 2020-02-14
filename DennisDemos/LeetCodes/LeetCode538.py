import Utils.PythonTreeNode
class Solution:
    num = 0
    def convertBST(self, root):
        if root == None:
            return
        
        self.convertBST(root.right)
        root.val += self.num
        self.num = root.val
        self.convertBST(root.left)
        return root
