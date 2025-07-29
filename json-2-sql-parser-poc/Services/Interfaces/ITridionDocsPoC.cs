using json_2_sql_parser_poc.Common.TridionDocs.TridionDocs;
using json_2_sql_parser_poc.Models.TridionDocs.Requests;
using json_2_sql_parser_poc.Models.TridionDocs.Responses;

namespace json_2_sql_parser_poc.Services.Interfaces;

public interface ITridionDocsPoC
{
    Task<string> GetListOfValues(ListOfValuesEnum value);
    Task<List<PublicationIshObject>> GetPublicationsByLogicalId(PublicationsByLogicalIdRequest request);
    Task<string> GetVersion();
}
