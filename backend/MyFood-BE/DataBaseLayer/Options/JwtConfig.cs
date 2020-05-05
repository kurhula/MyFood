
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseLayer.Options
{
    public class JwtConfig
    {
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string SecretKey { get; set; }
    }
}
