class Solution:
    def reverseWords(self, s):

        sList = s.split(' ')
        for index in range(len(sList)):
            sList[index] = ''.join(reversed(sList[index]))

        newS = ' '.join(sList)
        return newS

s = Solution()
s.reverseWords("Let's take LeetCode contest")