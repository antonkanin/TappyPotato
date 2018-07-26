using System;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class CryptoUtils : MonoBehaviour
{

    public static byte[] AESEncrypt(string plainText, byte[] key, byte[] IV)
    {
        // Check arguments.
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException("plainText");
        if (key == null || key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");

        byte[] encrypted;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = IV;

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV)
            {
                using (var msEncrypt = new MemoryStream())
                {

                }

            }
        }
    }

}
