namespace json_2_sql_parser_poc.Services.Interfaces;

public interface IServiceConnectPoC
{
    Task<string> GetProductDefinitionsBySerialNumber(string serialNumber);
    Task<string> GetMachineBySerialNumber(string serialNumber);
    Task<string> GetAslDocumentBySerialNumber(string serialNumber);
}
