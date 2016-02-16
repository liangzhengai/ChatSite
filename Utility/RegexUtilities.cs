using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Rns.Utility
{
    public class RegexUtility
    {
        /// <summary>
        /// check if the email is correct
        /// </summary>
        /// <param name="strIn">email address to check</param>
        /// <returns>true - correct, false - incorrect</returns>
        public static bool IsValidEmail(string strIn)
        {
            if (String.IsNullOrEmpty(strIn))
                return false;

            bool isEmail = Regex.IsMatch(
                strIn, 
                @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", 
                RegexOptions.IgnoreCase);

            return isEmail;
        }
    }
}