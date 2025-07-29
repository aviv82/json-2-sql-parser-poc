namespace json_2_sql_parser_poc.Models.ServiceConnect;

public class MachineDetails
{
    public Brand Brand { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string DezideUri { get; set; } = default!;
    public string ManufacturingDate { get; set; } = default!;
    public Model Model { get; set; } = default!;
    public string ProductCompany { get; set; } = default!;
    public string ProductGroupCode { get; set; } = default!;
    public string ProductNumbers { get; set; } = default!;
    public string SerialNumber { get; set; } = default!;
    public string SerialNumberType { get; set; } = default!;
    public string ShippingDate { get; set; } = default!;
    public string SoldToFamCode { get; set; } = default!;
}
