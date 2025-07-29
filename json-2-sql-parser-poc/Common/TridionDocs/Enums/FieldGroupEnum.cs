using System.ComponentModel;

namespace json_2_sql_parser_poc.Common.TridionDocs.TridionDocs;

public enum FieldGroupEnum
{
    [Description("None")]
    None,
    [Description("All")]
    All,
    [Description("Basic")]
    Basic,
    [Description("Descriptive")]
    Descriptive,
    [Description("System")]
    System
}
