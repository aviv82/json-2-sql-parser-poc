using json_2_sql_parser_poc.Common;
using json_2_sql_parser_poc.Models.ServiceConnect;
using json_2_sql_parser_poc.Services.Interfaces;
using Newtonsoft.Json;
using System.Net;

namespace json_2_sql_parser_poc.Services;

public class ServiceConnectPoC : IServiceConnectPoC
{
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;

    private readonly string _baseUrl;
    private readonly string _apiKey;
    private readonly string _apiKeyValue;

    public ServiceConnectPoC(IConfiguration config)
    {
        _config = config;

        _baseUrl = _config.GetValue<string>(Constants.Environment.Variables.ServiceConnect.BaseUrl) ?? "";
        _apiKey = _config.GetValue<string>(Constants.Environment.Variables.ServiceConnect.ApiKey) ?? "";
        _apiKeyValue = _config.GetValue<string>(Constants.Environment.Variables.ServiceConnect.ApiValue) ?? "";

        _httpClient = new HttpClient();

        _httpClient.DefaultRequestHeaders.Add(_apiKey, _apiKeyValue);
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        _httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
        _httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");

    }

    public async Task<string> GetProductDefinitionsBySerialNumber(string serialNumber)
    {
        string endPoint = $"{_baseUrl}machines/{serialNumber}/productdefinitions";

        try
        {
            var result = await _httpClient.GetAsync(new Uri(endPoint));

            if (result.StatusCode != HttpStatusCode.OK)
            {
                return $"failed to retreive product definition for serial number: {serialNumber} - status code : {result.StatusCode.ToString()}";
            }
            else
            {
                string bodyAsString = await result.Content.ReadAsStringAsync();
                if (result.Content != null)
                {
                    var resultBody = await result.Content.ReadAsStringAsync();
                    RootProductDefinition data = JsonConvert.DeserializeObject<RootProductDefinition>(resultBody) ?? new RootProductDefinition();

                    if (data.ProductDefResp != null && data.ProductDefResp.ProductDefinitions.Count > 0)
                    {
                        string toReturn = JsonConvert.SerializeObject(data.ProductDefResp, Formatting.Indented);
                        return $"product definition for serial number: {serialNumber} - \n \n {toReturn}";
                    }

                    return $"product definition for serial number: {serialNumber} could not be read from content - status code : {result.StatusCode.ToString()}";
                }

                return $"product definition for serial number: {serialNumber} not availabele - status code : {result.StatusCode.ToString()}";
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    public async Task<string> GetMachineBySerialNumber(string serialNumber)
    {
        string endPoint = $"{_baseUrl}machines/{serialNumber}";

        try
        {
            var result = await _httpClient.GetAsync(new Uri(endPoint));
            if (result.StatusCode != HttpStatusCode.OK)
            {
                return $"failed to retreive machine details for serial number: {serialNumber} - status code : {result.StatusCode.ToString()}";
            }
            else
            {
                if (result.Content != null)
                {
                    var resultBody = await result.Content.ReadAsStringAsync();
                    RootMachineDetails data = JsonConvert.DeserializeObject<RootMachineDetails>(resultBody) ?? new RootMachineDetails();

                    if (data.MachineResp != null && data.MachineResp.MachineDetails.SerialNumberType != string.Empty)
                    {
                        string toReturn = JsonConvert.SerializeObject(data.MachineResp.MachineDetails, Formatting.Indented);
                        return $"machine details for serial number: {serialNumber} - \n \n {toReturn}";
                    }

                    return $"machine details for serial number: {serialNumber} not availabele - status code : {result.StatusCode.ToString()}";
                }

                return $"machine details for serial number: {serialNumber} not availabele - status code : {result.StatusCode.ToString()}";
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<string> GetAslDocumentBySerialNumber(string serialNumber)
    {
        string endPoint = $"{_baseUrl}machines/{serialNumber}/asldoclinks";

        try
        {
            var result = await _httpClient.GetAsync(new Uri(endPoint));
            if (result.StatusCode != HttpStatusCode.OK)
            {
                return $"failed to retreive asl document links for serial number: {serialNumber} - status code : {result.StatusCode.ToString()}";
            }
            else
            {
                if (result.Content != null)
                {
                    var resultBody = await result.Content.ReadAsStringAsync();
                    RootAslDocLinks data = JsonConvert.DeserializeObject<RootAslDocLinks>(resultBody) ?? new RootAslDocLinks();

                    if (data.DocLinkDescResp != null && data.DocLinkDescResp.DocLinkDesList.Count > 0)
                    {
                        foreach (var doc in data.DocLinkDescResp.DocLinkDesList)
                        {
                            var identifier = doc.DocumentIdentifier;
                            var docName = doc.DocumentName;

                            var isFile = await GetDocumentByIdentifier(serialNumber, identifier, docName);

                            if (isFile)
                            {
                                return $"asl document for serial number: {serialNumber}, with document identifier: {identifier} successfully created - \n file name: {docName} \n status code : {result.StatusCode.ToString()}";
                            }
                        }
                    }

                    return $"asl document links for serial number: {serialNumber} not availabele - status code : {result.StatusCode.ToString()}";
                }

                return $"asl document links for serial number: {serialNumber} not availabele - status code : {result.StatusCode.ToString()}";
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private async Task<bool> GetDocumentByIdentifier(string serialNumber, string docIdentifier, string docName)
    {
        string endPoint = $"{_baseUrl}machines/{serialNumber}/asldocs/{docIdentifier}";
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/pdf");
        try
        {
            var result = await _httpClient.GetAsync(new Uri(endPoint));
            if (result.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine($"failed to download asl document for serial number: {serialNumber} with document identifier: {docIdentifier} - status code : {result.StatusCode.ToString()}");
                return false;
            }
            else
            {
                if (result.Content != null)
                {
                    var resultBody = await result.Content.ReadAsByteArrayAsync();
                    File.WriteAllBytes($@"{docName}", resultBody);

                    if (File.Exists($@"{docName}"))
                    {
                        Console.WriteLine($"asl document for serial number: {serialNumber} with document identifier: {docIdentifier} downloaded - \n file name: {docName} \n status code : {result.StatusCode.ToString()}");
                        return true;
                    }
                    Console.WriteLine($"failed to write asl document for serial number: {serialNumber} with document identifier: {docIdentifier} an unexpected error occurred - \n file name: {docName} \n status code : {result.StatusCode.ToString()}");
                    return false;
                }
                Console.WriteLine($"failed to download asl document for serial number: {serialNumber} with document identifier: {docIdentifier} - \n content not available. \n status code : {result.StatusCode.ToString()}");
                return false;
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}