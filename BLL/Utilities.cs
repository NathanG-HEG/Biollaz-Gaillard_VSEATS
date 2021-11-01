using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL
{
    public abstract class Utilities
    {

        public static bool IsEmailAddressCorrect(string emailAddress)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex rg = new Regex(pattern);

            return rg.IsMatch(emailAddress);
        }

        public static bool IsPasswordSyntaxCorrect(string password)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            return (hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) &&
                    hasMinimum8Chars.IsMatch(password));
        }

    }
}
