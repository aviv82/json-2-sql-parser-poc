namespace json_2_sql_parser_poc.Models;

public class ShopOrder
{
    public int SoId { get; set; }
    public string SerialNumber { get; set; } = string.Empty;
    public int ShopOrderStatusId { get; set; }
    public string OperationNumber { get; set; } = string.Empty;
    public int BcpsStatus { get; set; }
    public string Model { get; set; } = string.Empty;
    public string StartDate { get; set; } = string.Empty;
    public string LastUSBCreation { get; set; } = string.Empty;
    public string FolderDeleted { get; set; } = string.Empty;
    public string ProductCompany { get; set; } = string.Empty;
    public string LANGUAGES { get; set; } = string.Empty;
}