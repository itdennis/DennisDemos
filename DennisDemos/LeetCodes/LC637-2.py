class Solution:
    def averageOfLevels(self, root):
        oneLevelNode = list()
        res = list()
        oneLevelNode.append(root) 

        while oneLevelNode.count != 0:
            sum = count = 0
            temp = list()
            while oneLevelNode.count != 0:
                currentNode = oneLevelNode.pop()
                sum += currentNode.val
                count += 1
                if currentNode.left != None:
                    temp.append(currentNode.left)
                if currentNode.right != None:
                    temp.append(currentNode.right)
            average = sum / count
            res.append(average)
            oneLevelNode = temp

        return res