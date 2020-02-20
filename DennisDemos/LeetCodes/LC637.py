class Solution:
    def averageOfLevels(self, root):
        sums = list()
        counts = list()
        i = 0
        self.depthFirstTraversal(root, i, sums, counts)
        res = list()
        for index in range(len(sums)):
            res.append(sums[index] / counts[index])

        return res
        

    def depthFirstTraversal(self, root, i, sums, counts):
            if root == None:
                return
            # operations of sums and counts
            if i < len(sums):
                sums[i] += root.val
                counts[i] += 1
            else:
                sums.append(root.val)
                counts.append(1)

            self.depthFirstTraversal(root.left, i + 1, sums, counts)
            self.depthFirstTraversal(root.right, i + 1, sums, counts)