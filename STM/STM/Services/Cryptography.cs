using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace STM.Services
{
    public class MD5Cryptography : ICryptography
    {
        public string GetHash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

                return Convert.ToBase64String(hash);
            }
        }
    }
}
