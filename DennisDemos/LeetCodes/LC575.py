class Solution:
    def distributeCandies(self, candies):
        candiesDict = dict()
        for index in range(len(candies)):
            if candies[index] not in candiesDict:
                candiesDict[candies[index]] = 1
            else:
                candiesDict[candies[index]] += 1

        sisterShouldGetCandiesNums = int(len(candies) / 2)
        candiesCategoriesNums = len(candiesDict)

        if sisterShouldGetCandiesNums < candiesCategoriesNums:
            return sisterShouldGetCandiesNums

        if sisterShouldGetCandiesNums >= candiesCategoriesNums:
            return candiesCategoriesNums

s = Solution()
s.distributeCandies([1,1,2,3])