using System;
using System.Linq;
using AirportAPI.Models;

namespace AirportAPI.Utils
{
    public static class StringUtils
    {
        public static string ToUnderscoreCase(this string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()))
                .ToLower();
        }

        public static string WhereStatementBuilder(FilterRequest request, string tableName)
        {
            var segments = request.GetType().GetProperties()
                .Where(prop => prop.GetValue(request, null) != null)
                .Select(prop =>
                {
                    if (prop.PropertyType == typeof(DateTime))
                    {
                        return
                            $"{tableName}.{prop.Name.ToUnderscoreCase()}::timestamp>=date_trunc('hour', TIMESTAMP '{prop.GetValue(request)}')::timestamp";
                    }
                    if (prop.PropertyType == typeof(int) && (int) prop.GetValue(request) != 0)
                    {
                        return $"{tableName}.{prop.Name.ToUnderscoreCase()}={prop.GetValue(request)}";
                    }

                    return "";
                })
                .Where(segment => !segment.Equals(""))
                .ToList();
            return (segments.Any()) ? "WHERE " + string.Join(" AND ", segments) : "";
        }
    }
}