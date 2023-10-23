using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecurePass.Common
{
    internal static class Utility
    {
        public static string GetHash(string str)
        {
            byte[] data = SHA256.HashData(Encoding.UTF8.GetBytes(str));
            var sBuilder = new StringBuilder();
            foreach (var item in data)
                sBuilder.Append(item.ToString("x2"));
            return sBuilder.ToString();
        }
    }
}
