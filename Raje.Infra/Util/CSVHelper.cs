
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Raje.Infra.Util
{
    public static class CSVHelper<T>
    {
        public static string WriteContent(IEnumerable<T> content)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(GenerateTableHeader());
            sb.AppendLine(GenerateTableBody(content));
            return sb.ToString();
        }

        public static string GenerateTableHeader()
        {
            StringBuilder sb = new StringBuilder();
            var lastProp = typeof(T).GetProperties().LastOrDefault();
            foreach (var prop in typeof(T).GetProperties())
            {
                var displayName = prop.GetCustomAttribute(typeof(DisplayNameAttribute)) as DisplayNameAttribute;
                if (lastProp == prop) sb.Append($"{displayName?.DisplayName}");
                else sb.Append($"{displayName?.DisplayName};");
            }
            return sb.ToString();
        }

        public static string GenerateTableBody(IEnumerable<T> content)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var contentLine in content)
            {
                sb.AppendLine(GenerateTableBodyLine(contentLine));
            }
            return sb.ToString();
        }

        public static string GenerateTableBodyLine(T content)
        {
            StringBuilder sb = new StringBuilder();
            Type myType = content.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            var lastProp = props.LastOrDefault();
            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(content, null);
                if (lastProp == prop) sb.Append($"{propValue}");
                else sb.Append($"{propValue};");
            }
            return sb.ToString();
        }
    }
}
