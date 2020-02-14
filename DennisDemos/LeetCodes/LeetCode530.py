import Utils.PythonTreeNode

class Solution:
    midSortedList = list()
    def getMinimumDifference(self, root):
        currentMin = -1
        self.getMidSortedTreeList(root)
        for index in range(len(self.midSortedList) - 1):
            if currentMin == -1 or currentMin > abs(self.midSortedList[index] - self.midSortedList[index + 1]):
                    currentMin = abs(self.midSortedList[index] - self.midSortedList[index + 1])
        return currentMin

    def getMidSortedTreeList(self, root):
        if root == None:
            return
        self.getMidSortedTreeList(root.left)
        self.midSortedList.append(root.val)
        self.getMidSortedTreeList(root.right)

treeNode1 = Utils.PythonTreeNode.PythonTreeNode(1) 
treeNode3 = Utils.PythonTreeNode.PythonTreeNode(5) 
treeNode2 = Utils.PythonTreeNode.PythonTreeNode(3)

treeNode1.right = treeNode3
treeNode3.left = treeNode2

s = Solution()
s.getMinimumDifference(treeNode1)