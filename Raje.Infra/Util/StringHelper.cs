using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Raje.Infra.Util
{
    public static class StringHelper
    {
        public static string RemoveEnterCharacter(this String text)
        {
            return Regex.Replace(text, @"\s+", " ");
        }

    }
}
