using Newtonsoft.Json;

namespace json_2_sql_parser_poc.Models.TridionDocs;

public class AccessToken
{
    [JsonProperty("access_token")]
    public string Token { get; set; } = default!;
    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }
    [JsonProperty("token_type")]
    public string TokenType { get; set; } = default!;
    public string Scope { get; set; } = default!;
}
