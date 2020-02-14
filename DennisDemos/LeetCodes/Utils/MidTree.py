import Utils.PythonTreeNode
class MidTree:
    nodeList = list()
    def midTree(self, node):
        if node == None:
            return
        self.midTree(node.left)
        self.nodeList.append(node.val)
        self.midTree(node.right)

node = Utils.PythonTreeNode.PythonTreeNode(4)
mid = MidTree()
mid.midTree(node)
print(MidTree.nodeList)
