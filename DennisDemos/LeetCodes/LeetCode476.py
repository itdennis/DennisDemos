class Solution:
    def findComplement(self, num: int) -> int:
        num2 = 1
        while(num2<=num):
            num2 <<= 1
        num2 -= 1
        return num^num2


s = Solution()
res = s.findComplement(5)
print(res)