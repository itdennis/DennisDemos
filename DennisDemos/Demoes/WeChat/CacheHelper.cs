using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DennisDemos.Demoes.WeChat.WeChat;

namespace DennisDemos.Demoes.WeChat
{
    class CacheHelper
    {
        private static Dictionary<string, AccessToken> tokenCache = new Dictionary<string, AccessToken>();

        internal static AccessToken GetCache(string v)
        {
            if (tokenCache.Keys.Contains(v))
            {
                return tokenCache[v];
            }
            else
            {
                return null;
            }
        }

        internal static void SetCache(string v, AccessToken token)
        {
            if (tokenCache.Keys.Contains(v))
            {
                tokenCache[v] = token;
            }
            else
            {
                tokenCache.Add(v, token);
            }
        }
    }
}
