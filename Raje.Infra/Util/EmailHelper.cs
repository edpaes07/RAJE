using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Raje.Infra.Util
{
    public static class EmailHelper
    {
        /// <summary>Pattern para validar o E-mail</summary>
        public const string EmailPattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

        /// <summary>Verifica se o parâmetro é um e-mail válido</summary>
        /// <param name="email_">Email</param>
        /// <returns>Se está válido</returns>
        public static bool ValidarEmail(string email_)
        {
            Regex rx = new Regex(EmailPattern);
            return rx.Match(email_).Success;
        }

        public static IEnumerable<string> SepararEmail(string emails)
        {
            if (string.IsNullOrWhiteSpace(emails))
                return new List<string>();

            return emails.Split(new char[2] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}