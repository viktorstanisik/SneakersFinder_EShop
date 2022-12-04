using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EShop_Client_Shared.Helpers
{
    public static class Methods
    {
        public static string Sha512Hash(string s)
        {
            var inputHashBytes = System.Text.Encoding.UTF8.GetBytes(s);
            using var shaM = SHA512.Create();
            var outputHashBytes = shaM.ComputeHash(inputHashBytes);
            var ret = BitConverter.ToString(outputHashBytes).Replace("-", "").ToLower();
            return ret;

        }
    }
}
