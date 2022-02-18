using System;
using System.Linq;

namespace EnterpriseArchitecture.Utilities.SharedOperations
{
    /// <summary>
    /// Base64 Encoding is a technique that enables storing/transmitting binary data by converting 
    /// it to text format. Binary data, consisting of 8-bit bytes, is divided into 6-bit parts. 
    /// 64 different numbers expressed in 6-bit represent 64 different characters in the ASCII 
    /// character set, expressed as Printable Characters. Base64 Encoding is done by matching 
    /// the 64-bit data at hand with this table.
    /// 
    ///     A              S                D       Text To Base64 Encode
    ///   0x65           0x83              0x68     Hex Representation
    /// 01000001       01010011         01000100    Binary Representation
    ///  010000     010101  001101        000100    Grouped By 6-Bits
    ///     Q          V       N            E       Base64 Representation
    ///     
    /// In cases where the number of bytes in the data to be base64 encoded is not a multiple 
    /// of three, the data is padded until the number of bytes in the data is exactly a multiple 
    /// of three, and the parts that are not in the original data are indicated with "=" in the output.
    /// 
    ///     A              S                    Text To Base64 Encode
    ///   0x65           0x83                   Hex Representation
    /// 01000001       01010011      00000000   Binary Representation
    ///  010000     010101  001101    000000    Grouped By 6-Bits
    ///     Q          V       N         =      Base64 Representation
    ///     
    /// Most Base64 encoded data ends with "=" or "==" due to the padding operation.
    /// 
    /// Base64 Encoding's main usage area is to transmit binary data with protocols such as mail,
    /// HTTP, FTP, which are designed to transmit text and are not binary compatible. The reason 
    /// why binary data cannot be transmitted with these protocols is the risk of passing special
    /// characters such as CRLF, which are considered special by these protocols, in binary data. 
    /// For example, CRLF (Carriage Return/Line Feed) will be detected as Line Ending by HTTP client
    /// or server. With Base64 Encoding, it is guaranteed that there will be no special characters 
    /// in the data.
    /// 
    /// Another usage area of ​​Base64 Encoding is adding images and other files to HTML and CSS 
    /// documents by encoding with Base64 using "Data URLs" format in modern browsers. 
    /// Ex: <img src="data:image/jpeg:base64,a2ossA== />
    /// </summary>
    public static class HelperExtensions
    {
        // Characters that are used in base64 strings.
        private static Char[] Base64Chars = new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '/' };

        /// <summary>
        /// Extension method to test whether the value is a base64 string
        /// </summary>
        /// <param name="value">Value to test</param>
        /// <returns>Boolean value, true if the string is base64, otherwise false</returns>
        public static Boolean IsBase64String(this String value)
        {
            // The quickest test. If the value is null or is equal to 0 it is not base64. Base64 string's length is always divisible by four, i.e. 8, 16, 20 etc. 
            // If it is not you can return false. Quite effective Further, if it meets the above criterias, then test for spaces. If it contains spaces, it is not base64
            if (value == null || value.Length == 0 || value.Length % 4 != 0 || value.Contains(' ') || value.Contains('\t') || value.Contains('\r') || value.Contains('\n'))
                return false;

            // 98% of all non base64 values are invalidated by this time.
            var index = value.Length - 1;

            // If there is padding step back
            if (value[index] == '=')
                index--;

            // If there are two padding chars step back a second time
            if (value[index] == '=')
                index--;

            // Now traverse over characters. You should note that I'm not creating any copy of the existing strings, assuming that they may be quite large.
            for (var i = 0; i <= index; i++)
                // If any of the character is not from the allowed list
                if (!Base64Chars.Contains(value[i]))
                    // return false
                    return false;

            // If we got here, then the value is a valid base64 string
            return true;
        }
    }
}