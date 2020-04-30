using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ProjectBlog.Core.Extensions
{
    public static class Crypto
    {
    public static string Encrypt(string toEncrypt) {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            string key = "*******";

            MD5CryptoServiceProvider md = new MD5CryptoServiceProvider();
            keyArray = md.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            md.Clear();
            TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider();
            trip.Key = keyArray;
            trip.Mode = CipherMode.ECB;
            trip.Padding = PaddingMode.PKCS7;

            ICryptoTransform transform = trip.CreateEncryptor();

            byte[] resultArr = transform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            trip.Clear();
            return Convert.ToBase64String(resultArr, 0, resultArr.Length);
     }
    public static string Decrypt(string cipherString) {
            byte[] keyArray;
            cipherString = cipherString.Replace(" ", "+");
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);
            string key = "*******";
            MD5CryptoServiceProvider md = new MD5CryptoServiceProvider();

            keyArray = md.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            md.Clear();

            TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider();
            trip.Key = keyArray;
            trip.Mode = CipherMode.ECB;
            trip.Padding = PaddingMode.PKCS7;

            ICryptoTransform transform = trip.CreateDecryptor();
            try
            {
                byte[] resultArr = transform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return UTF8Encoding.UTF8.GetString(resultArr);
            }
            catch(Exception e) { Console.WriteLine(e); }
            trip.Clear();
            return "";
        }

        public static string CreateHash(string hashString)
        {
            HashAlgorithm hash = new SHA1Managed();
            var hashbytes = Encoding.UTF8.GetBytes(hashString);
            var bytes = hash.ComputeHash(hashbytes);
            var result = Convert.ToBase64String(bytes);
            return result;
        }

    }
}
