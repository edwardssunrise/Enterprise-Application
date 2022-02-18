using System;

namespace EnterpriseArchitecture.Utilities.PasswordOperations
{
    /// <summary>
    /// Base64 Encoding and Decoding operations are performed through methods defined in the CryptoManager static class.
    /// </summary>
    public static class CryptoManager
    {
        public static string Base64Encrypt(string data)
        {
            try
            {
                byte[] dataByteArray = System.Text.ASCIIEncoding.ASCII.GetBytes(data);
                string encryptedData = System.Convert.ToBase64String(dataByteArray);
                return encryptedData;
            }
            catch (Exception)
            {
                return data;
            }
        }

        public static string Base64Decrypt(string descryptData)
        {
            try
            {
                byte[] descryptDataArray = System.Convert.FromBase64String(descryptData);
                string originalData = System.Text.ASCIIEncoding.ASCII.GetString(descryptDataArray);
                return originalData;
            }
            catch (Exception)
            {
                return descryptData;
            }
        }
    }
}