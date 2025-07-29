namespace json_2_sql_parser_poc.Models.TridionDocs.Responses;

public class ListOfValues
{
    public bool Active { get; set; }
    public string Id { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Type { get; set; } = default!;
}
