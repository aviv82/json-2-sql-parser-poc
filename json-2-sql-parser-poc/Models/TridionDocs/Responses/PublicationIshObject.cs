namespace json_2_sql_parser_poc.Models.TridionDocs.Responses;

public class PublicationIshObject
{
    public int VersionCardId { get; set; }
    public int LanguageCardId { get; set; }
    public string LogicalId { get; set; } = default!;
    public string Version { get; set; } = default!;
    public string OutputFormatId { get; set; } = default!;
    public List<string> LanguageCombinationIds { get; set; } = default!;
    public List<ResponseField> Fields { get; set; } = default!;
    public string Id { get; set; } = default!;
    public string Type { get; set; } = default!;
}
