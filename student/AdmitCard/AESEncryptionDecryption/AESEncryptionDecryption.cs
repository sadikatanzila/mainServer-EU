using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Text;


    public class AESEncryptionDecryption
    {
        public static string Encrypt(string InputText, string KeyString)
        {

            MemoryStream memoryStream = null;
            CryptoStream cryptoStream = null;
            try
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 128;
                    AES.BlockSize = 128;
                    byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(InputText);
                    PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(KeyString, Encoding.ASCII.GetBytes(KeyString.Length.ToString()));
                    using (ICryptoTransform Encryptor = AES.CreateEncryptor(SecretKey.GetBytes(16), SecretKey.GetBytes(16)))
                    {
                        using (memoryStream = new MemoryStream())
                        {
                            using (cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(PlainText, 0, PlainText.Length);
                                cryptoStream.FlushFinalBlock();
                                return Convert.ToBase64String(memoryStream.ToArray());
                            }
                        }
                    }
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                if (memoryStream != null)
                    memoryStream.Close();
                if (cryptoStream != null)
                    cryptoStream.Close();
            }
        }

        public static string Decrypt(string InputText, string KeyString)
        {
            MemoryStream memoryStream = null;
            CryptoStream cryptoStream = null;
            try
            {
                    using (RijndaelManaged AES = new RijndaelManaged())
                    {
                        AES.KeySize = 128;
                        AES.BlockSize = 128;
                        byte[] EncryptedData = Convert.FromBase64String(InputText);
                        PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(KeyString, Encoding.ASCII.GetBytes(KeyString.Length.ToString()));
                        using (ICryptoTransform Decryptor = AES.CreateDecryptor(SecretKey.GetBytes(16), SecretKey.GetBytes(16)))
                        {
                            using (memoryStream = new MemoryStream(EncryptedData))
                            {
                                using (cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read))
                                {
                                    byte[] PlainText = new byte[EncryptedData.Length];
                                    return Encoding.Unicode.GetString(PlainText, 0, cryptoStream.Read(PlainText, 0, PlainText.Length));
                                }
                            }
                        }
                    }
                
            }
            catch
            {
                throw;
            }
            finally
            {
                if (memoryStream != null)
                    memoryStream.Close();
                if (cryptoStream != null)
                    cryptoStream.Close();
            }
        }

    }
