using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Utils
{
    public class BearerToken
    {
        public static async Task<string> GetBearerToken(string resourceUrl)
        {
            var ac = new AuthenticationContext("https://login.windows.net/microsoft.onmicrosoft.com");
            var cred = new ClientCredential("23d30219-aa1c-4b71-970b-629a7b9da25d", "6NeuVQkybupEY8j7rMigCb+gyqvhMos1buZt1W4dJ04=");
            var authResult = await ac.AcquireTokenAsync(resourceUrl, cred);
            return authResult.AccessToken;
        }

    }
}
