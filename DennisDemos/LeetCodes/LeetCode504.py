class Solution:
    def convertToBase7(self, num):
        if num == 0:
            return 0
        symple = ''
        if num < 0:
            symple = '-'
        cacheList = list()
        num = abs(num)
        while num != 0:
            cacheList.insert(0, (str)(num % 7))
            num //= 7

        return symple + ''.join(cacheList)