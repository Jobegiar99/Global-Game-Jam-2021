using System;
using System.IO;
using System.Security.Cryptography;

public class AES_Crypting
{
    public static string Encrypt_AES(string plainText)
    {
        Aes myAes = Aes.Create();
        string key1 = Convert.ToBase64String(myAes.IV);
        string key2 = Convert.ToBase64String(myAes.Key);
        // Encrypt the string to an array of bytes.
        byte[] encrypted = EncryptStringToBytes_Aes(plainText, myAes.Key, myAes.IV);

        string key3 = Convert.ToBase64String(encrypted);

        int max = 44;
        if (key3.Length > 44)
            max = key3.Length;

        string resultKey = "";
        for (int i = 0; i < max; i++)
        {
            if (i < 24)
                resultKey += key1[i];

            if (i < 44)
                resultKey += key2[i];

            if (i < key3.Length)
                resultKey += key3[i];
        }

        return resultKey;
    }

    public static string Dencrypt_AES(string encrypted)
    {
        string key1 = "";
        string key2 = "";
        string key3 = "";
        for (int i = 0; i < encrypted.Length; i++)
        {
            bool sumar = false;
            if (key1.Length < 24)
            {
                key1 += encrypted[i];
                sumar = true;

            }
            if (key2.Length < 44)
            {
                if (sumar)
                {
                    i++;
                }
                else
                {
                    sumar = true;
                }
                key2 += encrypted[i];
            }
            if (key3.Length < (encrypted.Length - 24 - 44))
            {
                if (sumar)
                    i++;

                key3 += encrypted[i];
            }
        }

        string resultText = "";
        resultText += DecryptStringFromBytes_Aes(Convert.FromBase64String(key3), Convert.FromBase64String(key2), Convert.FromBase64String(key1));

        return resultText;
    }

    private static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException("plainText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");
        byte[] encrypted;

        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        // Return the encrypted bytes from the memory stream.
        return encrypted;
    }

    private static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException("cipherText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");

        // Declare the string used to hold
        // the decrypted text.
        string plaintext = null;

        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {

                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        return plaintext;
    }
}