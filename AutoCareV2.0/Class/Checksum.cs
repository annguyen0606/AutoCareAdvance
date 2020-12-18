using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AutoCareV2._0.Class
{
    class Checksum
    {
        public static string GetMd5Hash(string input, string key)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] binput = Encoding.UTF8.GetBytes(input);
                byte[] bkey = Encoding.UTF8.GetBytes(key);
                md5Hash.TransformBlock(binput, 0, binput.Length, binput, 0);
                md5Hash.TransformFinalBlock(bkey, 0, bkey.Length);

                byte[] data = md5Hash.Hash;
                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();
                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }
    }
}
