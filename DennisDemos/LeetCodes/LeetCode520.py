class Solution:
    def detectCapitalUse(self, word):
        if len(word) == 1:
            return True
        firstCharacterIsSmall = ord(word[0]) >= ord('a') and ord(word[0]) <= ord('z')
        secondCharacterIsSmall = ord(word[1]) >= ord('a') and ord(word[0]) <= ord('z')
        for index in range(len(word)):
            if index == 0:
                continue
            if firstCharacterIsSmall: 
                if (ord(word[index]) < ord('a')) or (ord(word[index]) > ord('z')):
                    return False
            if not firstCharacterIsSmall:
                if secondCharacterIsSmall:
                    if (ord(word[index]) < ord('a')) or (ord(word[index]) > ord('z')):
                        return False
                else:
                    if (ord(word[index]) < ord('A')) or (ord(word[index]) > ord('Z')):
                        return False

        return True

s = Solution()
res = s.detectCapitalUse("Leetcode")
print(res)

