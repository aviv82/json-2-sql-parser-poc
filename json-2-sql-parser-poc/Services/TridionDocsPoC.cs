using json_2_sql_parser_poc.Common;
using json_2_sql_parser_poc.Common.Enums;
using json_2_sql_parser_poc.Common.TridionDocs.TridionDocs;
using json_2_sql_parser_poc.Models.TridionDocs;
using json_2_sql_parser_poc.Models.TridionDocs.Requests;
using json_2_sql_parser_poc.Models.TridionDocs.Responses;
using json_2_sql_parser_poc.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace json_2_sql_parser_poc.Services;

public class TridionDocsPoC : ITridionDocsPoC
{
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;

    private readonly string _baseUrl;
    private readonly string _clientId;
    private readonly string _clientSecret;

    public TridionDocsPoC(IConfiguration config)
    {
        _config = config;

        _baseUrl = _config.GetValue<string>(Constants.Environment.Variables.TridionDocs.BaseUrl) ?? "";
        _clientId = _config.GetValue<string>(Constants.Environment.Variables.TridionDocs.ClientId) ?? "";
        _clientSecret = _config.GetValue<string>(Constants.Environment.Variables.TridionDocs.ClientSecret) ?? "";

        _httpClient = new HttpClient();

        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        _httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
        _httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");

    }

    public async Task<string> GetListOfValues(ListOfValuesEnum value)
    {
        string paramVlue = EnumHelper.GetEnumStringDescriptionAttribute(value);

        string endPoint = $"{_baseUrl}ishws/api/v3/Lists/{paramVlue}/Values?activityFilter=active";

        string token = await GetAuth();

        _httpClient.DefaultRequestHeaders.Add("Authorization", token);

        try
        {
            var result = await _httpClient.GetAsync(new Uri(endPoint));

            if (result.StatusCode != HttpStatusCode.OK)
            {
                return $"failed to fetch list of values - \n status code : {result.StatusCode.ToString()}";
            }
            else
            {
                if (result.Content != null)
                {
                    var resultBody = await result.Content.ReadAsStringAsync();
                    List<ListOfValues> data = JsonConvert.DeserializeObject<List<ListOfValues>>(resultBody) ?? new List<ListOfValues>();
                    string toReturn = JsonConvert.SerializeObject(data, Formatting.Indented);

                    return toReturn;
                }
                return $"failed to process list of values response - \n status code : {result.StatusCode.ToString()}";
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    // "GetPublicationsAsync on soap - DocumentDeliveryPlatform.Proxy.Services.TridionDocs"
    public async Task<List<PublicationIshObject>> GetPublicationsByLogicalId(PublicationsByLogicalIdRequest request)
    {
        string endPoint = $"{_baseUrl}ishws/api/v3/Publications/ByLogicalId/Get";

        string token = await GetAuth();

        _httpClient.DefaultRequestHeaders.Add("Authorization", token);

        //string[] fields =
        //{
        //    Constants.TridionDocs.Field.Title,
        //    Constants.TridionDocs.Field.PublicationType,
        //    Constants.TridionDocs.Field.MasterRef,
        //    Constants.TridionDocs.Field.Version,
        //    Constants.TridionDocs.Field.Baseline,
        //    Constants.TridionDocs.Field.Resources,
        //    Constants.TridionDocs.Field.PubSourceLanguages,
        //    Constants.TridionDocs.Field.RequiredResolutions,
        //    Constants.TridionDocs.Field.ProjectName,
        //    Constants.TridionDocs.Field.MachineCategory,
        //    Constants.TridionDocs.Field.StartDate,
        //    Constants.TridionDocs.Field.PubStatus,
        //    Constants.TridionDocs.Field.LanguageCombination,
        //    Constants.TridionDocs.Field.RequestedLanguages,
        //    Constants.TridionDocs.Field.OutputFormat,
        //    Constants.TridionDocs.Field.PrintedMatterNumber
        //};

        var requestAsString = JsonConvert.SerializeObject(request, new StringEnumConverter());
        var content = new StringContent(requestAsString, Encoding.UTF8, "application/json");

        try
        {
            var result = await _httpClient.PostAsync(new Uri(endPoint), content);

            if (result.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"failed to fetch publications for request: \n {JsonConvert.SerializeObject(request, Formatting.Indented, new StringEnumConverter())} - \n status code : {result.StatusCode.ToString()}");
            }
            else
            {
                if (result.Content != null)
                {
                    var resultBody = await result.Content.ReadAsStringAsync();
                    List<PublicationIshObject> data = JsonConvert.DeserializeObject<List<PublicationIshObject>>(resultBody) ?? new List<PublicationIshObject>();

                    return data;
                }
                throw new Exception($"failed to serialize response: \n {await result.Content.ReadAsStringAsync() ?? "no content"} - \n status code : {result.StatusCode.ToString()}");
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<string> GetVersion()
    {
        string endPoint = $"{_baseUrl}ishws/api/Application/Version";

        string token = await GetAuth();

        _httpClient.DefaultRequestHeaders.Add("Authorization", token);

        try
        {
            var result = await _httpClient.GetAsync(new Uri(endPoint));

            if (result.StatusCode != HttpStatusCode.OK)
            {
                return $"failed to fetch version - \n status code : {result.StatusCode.ToString()}";
            }
            else
            {
                if (result.Content != null)
                {
                    var resultBody = await result.Content.ReadAsStringAsync();

                    return resultBody;
                }
                return $"failed to process version response - \n status code : {result.StatusCode.ToString()}";
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private async Task<string> GetAuth()
    {
        string endPoint = $"{_baseUrl}ISHAM/connect/token";

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

        var content = new FormUrlEncodedContent(
        [
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
            new KeyValuePair<string, string>("client_id", _clientId),
            new KeyValuePair<string, string>("client_secret", _clientSecret),
        ]);

        try
        {
            var result = await _httpClient.PostAsync(new Uri(endPoint), content);

            if (result.StatusCode != HttpStatusCode.OK)
            {
                return $"failed to authenticate for client: {_clientId} - status code : {result.StatusCode.ToString()}";
            }
            else
            {
                if (result.Content != null)
                {
                    var resultBody = await result.Content.ReadAsStringAsync();
                    AccessToken data = JsonConvert.DeserializeObject<AccessToken>(resultBody) ?? new AccessToken();

                    if (data.Token != string.Empty)
                    {
                        return $"{data.TokenType} {data.Token}";
                    }
                }
                return $"failed to process authentication response for client: {_clientId} - \n status code : {result.StatusCode.ToString()}";
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    // "GetPublicationAsync" on soap - DocumentDeliveryPlatform.Proxy.Services.TridionDocs
    private string GetPublicationAsync(
        string publicationGuid,
        string language = "EN",
        List<PublicationIshObject> publications = null,
        bool isEngineeredSolution = false)
    {
        if(publications == null)
        {
            throw new Exception("No publication list provided");        
        }

        string languageId = Constants.TridionDocs.Variable.LanguagePrefix + language;

        List<PublicationIshObject> publicationsForLanguage = publications
            .Where(x => x.LanguageCombinationIds.Contains(languageId) && x.OutputFormatId == Constants.TridionDocs.Variable.OutputFormatModelBook)
            .ToList();

        var publication = publicationsForLanguage
                .OrderBy(x => int.Parse(x.Version))
                .LastOrDefault();

        return publication?.LogicalId ?? string.Empty;
    }
}
