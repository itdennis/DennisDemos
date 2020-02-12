class Solution:
    nodeList = list()
    def findMode(self, root):
        maxNums = list()
        maxCount = 0
        currentNumCount = 0
        preValue = 0
        for node in self.nodeList:
            if preValue == node.val:
                currentNumCount += 1
            else:
                if maxCount < currentNumCount:
                    maxCount = currentNumCount
                    if len(maxNums) > 0:
                        maxNums.clear()
                    maxNums.append(node.val)
                if maxCount == currentNumCount:
                    maxNums.append(node.val)
                    
                preValue = node.val
                currentNumCount = 1

        return maxNums

    def midTree(self, root):
        if root == None:
            return
        self.findMode(root.left)
        self.nodeList.append(root.val)
        self.findMode(root.right)
        