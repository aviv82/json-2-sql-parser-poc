using json_2_sql_parser_poc.Models.TridionDocs.Requests;

namespace json_2_sql_parser_poc.Models.TridionDocs.Responses
{
    public class ResponseField : RequestField
    {
        public object Value { get; set; } = default!;
        public string Name { get; set; } = default!;
        public bool IsModified { get; set; }
        public string Type { get; set; } = default!;
    }
}
