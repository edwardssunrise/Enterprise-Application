using System;
using System.Linq;

namespace EnterpriseArchitecture.Utilities.SharedOperations
{
    /// <summary>
    /// T.C. Identity Number validation operations are provided by the methods defined in the IdentityManager static class.
    /// </summary>
    public static class IdentityManager
    {
        /// <summary>
        
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsIdentityNumber(string id) => FirstValidationAlgorithm(id) || SecondValidationAlgorithm(id);

        /// <summary>
        /// <summary>The dataset used to test the ID Number.</summary>
        /// </summary>
        private static string[] identityNumberTestData = new string[] { "11111111110", "22222222220", "33333333330", "44444444440", "55555555550", "66666666660", "77777777770", "88888888880", "99999999990" };

        /// <summary>
        /// T.C. Identity Number verification algorithm.
        /// </summary>
        /// <param name="identityNumber"></param>
        /// <returns></returns>
        private static bool FirstValidationAlgorithm(string identityNumber)
        {
            if (identityNumber.Length != 11 || identityNumberTestData.Any(id => id == identityNumber))
            {
                return false;
            }

            int singleDigitTotal = 0, doubleDigitTotal = 0, singularCalculationResult, total = 0;

            for (int i = 0; i < 9; i += 2)
            {
                singleDigitTotal += int.Parse(identityNumber.Substring(i, 1));
            }

            for (int i = 1; i < 9; i += 2)
            {
                doubleDigitTotal += int.Parse(identityNumber.Substring(i, 1));
            }

            singularCalculationResult = ((singleDigitTotal * 7) - doubleDigitTotal) % 10;

            for (int i = 0; i < 10; i++)
            {
                byte numberToBeCollected = byte.Parse(identityNumber.Substring(i, 1));
                total += numberToBeCollected;
                total %= 10;
            }

            identityNumber += Convert.ToInt32(total);
            return (singularCalculationResult.ToString() == identityNumber.Substring(9, 1)) && (total.ToString() == identityNumber.Substring(10, 1));
        }

        /// <summary>
        /// T.C. Identity Number verification algorithm.
        /// </summary>
        /// <param name="identityNumber"></param>
        /// <returns></returns>
        private static bool SecondValidationAlgorithm(string identityNumber)
        {
            bool result = false;

            if (identityNumber.Length == 11 && !identityNumberTestData.Any(id => id == identityNumber))
            {
                Int64 FirstIdentityNumber, SecondIdentityNumber, IdentityNumber;

                long C1, C2, C3, C4, C5, C6, C7, C8, C9, Q1, Q2;

                IdentityNumber = Int64.Parse(identityNumber);
                FirstIdentityNumber = IdentityNumber / 100;
                SecondIdentityNumber = IdentityNumber / 100;

                C1 = FirstIdentityNumber % 10; FirstIdentityNumber = FirstIdentityNumber / 10;
                C2 = FirstIdentityNumber % 10; FirstIdentityNumber = FirstIdentityNumber / 10;
                C3 = FirstIdentityNumber % 10; FirstIdentityNumber = FirstIdentityNumber / 10;
                C4 = FirstIdentityNumber % 10; FirstIdentityNumber = FirstIdentityNumber / 10;
                C5 = FirstIdentityNumber % 10; FirstIdentityNumber = FirstIdentityNumber / 10;
                C6 = FirstIdentityNumber % 10; FirstIdentityNumber = FirstIdentityNumber / 10;
                C7 = FirstIdentityNumber % 10; FirstIdentityNumber = FirstIdentityNumber / 10;
                C8 = FirstIdentityNumber % 10; FirstIdentityNumber = FirstIdentityNumber / 10;
                C9 = FirstIdentityNumber % 10; FirstIdentityNumber = FirstIdentityNumber / 10;

                Q1 = ((10 - ((((C1 + C3 + C5 + C7 + C9) * 3) + (C2 + C4 + C6 + C8)) % 10)) % 10);
                Q2 = ((10 - (((((C2 + C4 + C6 + C8) + Q1) * 3) + (C1 + C3 + C5 + C7 + C9)) % 10)) % 10);

                result = ((SecondIdentityNumber * 100) + (Q1 * 10) + Q2 == IdentityNumber);
            }
            return result;
        }
    }
}