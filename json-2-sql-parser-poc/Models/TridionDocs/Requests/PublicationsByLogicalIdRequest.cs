using json_2_sql_parser_poc.Common.TridionDocs.Enums;
using json_2_sql_parser_poc.Common.TridionDocs.TridionDocs;

namespace json_2_sql_parser_poc.Models.TridionDocs.Requests;

public class PublicationsByLogicalIdRequest
{
    public List<string> LogicalIds { get; set; } = default!;
    public StatusFilterEnum StatusFilter { get; set; }
    public SelectedPropertiesEnum SelectedProperties { get; set; }
    public FieldGroupEnum FieldGroup { get; set; }
    public List<RequestField>? Fields { get; set; }
    public bool IncludeLinks { get; set; } = default!;
}