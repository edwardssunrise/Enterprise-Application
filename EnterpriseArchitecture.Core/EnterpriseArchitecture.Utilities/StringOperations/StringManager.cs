using System.Text.RegularExpressions;

namespace EnterpriseArchitecture.Utilities.StringOperations
{
    /// <summary>
    /// External methods that operate on string expressions are defined in the static class StringManager.
    /// </summary>
    public static class StringManager
    {
        /// <summary>
        /// The "Slug" expression is used to create a URL from text information.
        /// </summary>
        /// <param name="incomingText"></param>
        /// <returns></returns>
        public static string ToSlug(string incomingText)
        {
            incomingText = incomingText.Replace("ş", "s");
            incomingText = incomingText.Replace("Ş", "s");
            incomingText = incomingText.Replace("İ", "i");
            incomingText = incomingText.Replace("I", "i");
            incomingText = incomingText.Replace("ı", "i");
            incomingText = incomingText.Replace("ö", "o");
            incomingText = incomingText.Replace("Ö", "o");
            incomingText = incomingText.Replace("ü", "u");
            incomingText = incomingText.Replace("Ü", "u");
            incomingText = incomingText.Replace("Ç", "c");
            incomingText = incomingText.Replace("c", "c");
            incomingText = incomingText.Replace("Ğ", "g");
            incomingText = incomingText.Replace("ğ", "g");
            incomingText = incomingText.Replace(" ", "-");
            incomingText = incomingText.Replace("---", "-");
            incomingText = incomingText.Replace("?", "");
            incomingText = incomingText.Replace("/", "");
            incomingText = incomingText.Replace(".", "");
            incomingText = incomingText.Replace("'", "");
            incomingText = incomingText.Replace("#", "");
            incomingText = incomingText.Replace("%", "");
            incomingText = incomingText.Replace("&", "");
            incomingText = incomingText.Replace("*", "");
            incomingText = incomingText.Replace("!", "");
            incomingText = incomingText.Replace("@", "");
            incomingText = incomingText.Replace("+", "");
            incomingText = incomingText.ToLower();
            incomingText = incomingText.Trim();

            /* All characters are converted to lowercase characters. */
            string encodedUrl = (incomingText ?? "").ToLower();

            /* Use the '&' character instead of the ' ' character. */
            encodedUrl = Regex.Replace(encodedUrl, @"\&+", "and");

            /* Use the "" character instead of the "'" character. */
            encodedUrl = encodedUrl.Replace("'", "");

            /* Delete all characters except lowercase letters and numbers. */
            encodedUrl = Regex.Replace(encodedUrl, @"[^a-z0-9]", "-");

            /* Delete repeating characters. */
            encodedUrl = Regex.Replace(encodedUrl, @"-+", "-");

            /* Put '-' between characters. */
            encodedUrl = encodedUrl.Trim('-');

            return encodedUrl;
        }
    }
}