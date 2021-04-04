using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Raje.Infra.Util
{
    public static class ExtensionsHelper
    {
        /// <summary>
        /// Método que retorna a descrição de um atributo de um objeto.
        /// </summary>
        /// <returns>Descrição do objeto.</returns>
        public static String GetDescription(this Object value)
        {
            return GetAttributeValue(value, typeof(DescriptionAttribute));
        }

        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            return default;
        }

        public static String GetAttributeValue(this Object value, Type attributeType)
        {
            return GetAttributeValue<String>(value, attributeType);
        }

        /// <summary>
        /// Método que retorna o valor de um atributo de um objeto.
        /// </summary>
        /// <param name="attributeType">O tipo do atributo.</param>
        /// <returns>Valor do atributo.</returns>
        public static T GetAttributeValue<T>(this Object value, Type attributeType)
        {
            Type objectType = null;
            Object[] attributes = null;
            String attributeName = null;

            if (value == null)
                throw new ArgumentNullException("value");

            if (attributeType == null)
                throw new ArgumentNullException("attributeType");

            objectType = value.GetType();
            attributeName = attributeType.Name.Left(attributeType.Name.Length - "Attribute".Length);

            if (value.GetType().BaseType == typeof(Enum))
                attributes = objectType.GetField(value.ToString()).GetCustomAttributes(attributeType, false);
            else
                attributes = objectType.GetCustomAttributes(attributeType, false);

            if (attributes.Length > 0)
                return (T)attributeType.GetProperty(attributeName).GetValue(attributes[0], null);

            return default(T);
        }

        /// <summary>
        /// Retorna os caracteres mais a esquerda de uma String.
        /// </summary>
        /// <param name="length">Tamanho da string a ser retornada.</param>
        /// <returns>Conjunto de caracteres a esquerda de uma string.</returns>
        public static String Left(this String value, Int32 length)
        {
            if (String.IsNullOrEmpty(value))
                return value;

            return value.Substring(0, length);
        }

        /// <summary>
        /// Retorna os caracteres mais a direita de uma string.
        /// </summary>
        /// <param name="length">Tamanho da String a ser retornada.</param>
        /// <returns>Conjunto de caracteres a direita de uma string.</returns>
        public static String Right(this String value, Int32 length)
        {
            if (String.IsNullOrEmpty(value))
                return value;

            return value.Substring(value.Length - length, length);
        }

        /// <summary>
        /// To lowerCamelCase
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            if (!char.IsUpper(s[0]))
                return s;

            char[] chars = s.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                bool hasNext = (i + 1 < chars.Length);
                if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
                    break;

                chars[i] = char.ToLower(chars[i], CultureInfo.InvariantCulture);
            }

            return new string(chars);
        }

        public static string ToDataSizeFormat(this string byteSize)
        {
            if (long.TryParse(byteSize, out long longVal))
            {
                return longVal.ToDataSizeFormat();
            }
            else
            {
                return "0 B";
            }
        }

        public static string ToDataSizeFormat(this int? byteSize)
        {
            if (byteSize.HasValue)
            {
                long val = byteSize.Value;
                return val.ToDataSizeFormat();
            }
            else
            {
                return "0 B";
            }
        }

        public static string ToDataSizeFormat(this int byteSize)
        {
            if (byteSize > 0)
            {
                long val = byteSize;
                return val.ToDataSizeFormat();
            }
            else
            {
                return "0 B";
            }
        }

        public static string ToDataSizeFormat(this long? byteSize)
        {
            if (byteSize.HasValue)
            {
                return byteSize.Value.ToDataSizeFormat();
            }
            else
            {
                return "0 B";
            }
        }

        public static string ToDataSizeFormat(this long byteSize)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            decimal len = byteSize;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }

            string result = String.Format("{0:0.##} {1}", len, sizes[order]);

            return result;
        }

        public static string ToTimeFormat(this int? secondsTotal)
        {
            int sec_num = 0;
            if (secondsTotal.HasValue)
            {
                sec_num = secondsTotal.Value;
            }

            var hours = Math.Floor((decimal)sec_num / 3600);
            var minutes = Math.Floor((sec_num - (hours * 3600)) / 60);
            var seconds = sec_num - (hours * 3600) - (minutes * 60);

            if (hours > 0)
            {
                return string.Format("{0}h {1}min {2}s", hours, minutes, seconds);
            }

            if (minutes > 0)
            {
                return string.Format("{0}min {1}s", minutes, seconds);
            }

            return string.Format("{0}s", seconds);
        }

        public static string RemoveSpecialCharacters(this string str, bool removeSpace = true, bool toLowerInvariant = true)
        {
            str = removeSpace ? str.Trim() : str;
            str = toLowerInvariant ? str.ToLowerInvariant() : str;
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }

        public static string RemoveWhiteSpaces(this string str)
        {
            return Regex.Replace(str, @"\s+", " ");
        }
    }
}