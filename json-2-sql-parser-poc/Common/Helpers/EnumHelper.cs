using System.ComponentModel;

namespace json_2_sql_parser_poc.Common.Enums;

public static class EnumHelper
{
    public static string GetEnumStringDescriptionAttribute<T>(T source)
    {
        if (source == null) return string.Empty;

        var type = source.GetType();

        if (type == null) return string.Empty;

        var asString = source.ToString();

        if (asString == null) return string.Empty;

        var fi = type.GetField(asString);

        if (fi == null) return string.Empty;

        DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
            typeof(DescriptionAttribute), false);

        if (attributes != null && attributes.Length > 0) return attributes[0].Description;

        else return asString;
    }
}
