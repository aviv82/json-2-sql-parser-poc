using json_2_sql_parser_poc.Common.TridionDocs.TridionDocs;

namespace json_2_sql_parser_poc.Models.TridionDocs;

public class IshField
{
    public string Name { get; set; } = default!;
    public IshFieldLevelEnum Level { get; set; }
}
