import math
class Solution:
    def judgeSquareSum(self, c):
        a = 0
        while a * a <= c:
            b = math.sqrt( c - a * a )
            if b == int(b):
                return True
            a += 1
        return False
s = Solution()
s.judgeSquareSum(10000000)