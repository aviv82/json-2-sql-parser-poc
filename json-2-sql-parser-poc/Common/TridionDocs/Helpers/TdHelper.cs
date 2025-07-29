using json_2_sql_parser_poc.Common.Enums;
using json_2_sql_parser_poc.Common.TridionDocs.TridionDocs;

namespace json_2_sql_parser_poc.Common.TridionDocs.Helpers;

public static class TdHelper
{
    public static string FieldToLevelMapper(string field)
    {
        switch (field)
        {
            case "FISHPUBSTATUS":
            case "FISHPUBLNGCOMBINATION":
            case "FISHOUTPUTFORMATREF":
            case "FATCPRINTEDMATTERNUMBER":
                return EnumHelper.GetEnumStringDescriptionAttribute(IshFieldLevelEnum.Language);

            case "FTITLE":
            case "FISHPUBLICATIONTYPE":
            case "FATCPROJECTNAME":
            case "FATCMACHINECATEGORY":
                return EnumHelper.GetEnumStringDescriptionAttribute(IshFieldLevelEnum.Logical);

            case "FISHMASTERREF":
            case "FISHBASELINE":
            case "FISHRESOURCES":
            case "FISHPUBSOURCELANGUAGES":
            case "FISHREQUIREDRESOLUTIONS":
            case "FREQUESTEDLANGUAGES":
            case "FATCSTARTDATE":
            case "VERSION":
                return EnumHelper.GetEnumStringDescriptionAttribute(IshFieldLevelEnum.Logical);

            default: return string.Empty;

        }
    }
}
