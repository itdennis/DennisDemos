using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dennis_webapi_demo.Jwt
{
    public class JwtSetting
    {
        public string ValidIssuer { get; set; }

        public string ValidAudience { get; set; }

        public string IssuerSigningKey { get; set; }
    }
}
