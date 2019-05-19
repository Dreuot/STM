using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STM_React.Infrastructure
{
    public class AuthOptions
    {
        public const string ISSUER = "STM_App"; // издатель токена
        public const string AUDIENCE = "User"; // потребитель токена
        const string KEY = "1337 secretKey 010$4 pwnzd";   // ключ для шифрации
        public const int LIFETIME = 24 * 60; // время жизни токена - 24 часа
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
