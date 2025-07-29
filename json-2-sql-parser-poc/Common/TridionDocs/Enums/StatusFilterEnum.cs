using System.ComponentModel;

namespace json_2_sql_parser_poc.Common.TridionDocs.Enums;

public enum StatusFilterEnum
{
    [Description("All")]
    All,
    [Description("AllReleased  ")]
    AllReleased,
    [Description("DraftOrLatestReleased ")]
    DraftOrLatestReleased,
    [Description("LatestReleased ")]
    LatestReleased
}
