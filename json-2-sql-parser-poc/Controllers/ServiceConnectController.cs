using json_2_sql_parser_poc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace json_2_sql_parser_poc.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServiceConnectController : Controller
{
    private readonly IServiceConnectPoC _serviceConnectPoc;

    public ServiceConnectController(IServiceConnectPoC serviceConnectPoc)
    {
        _serviceConnectPoc = serviceConnectPoc;
    }

    [HttpGet]
    [Route("Health")]
    public IActionResult GetHealth()
    {
        return Ok();
    }

    [HttpGet]
    [Route("GetProductDefinitionsBySerialNumber")]

    public async Task<IActionResult> GetProductDefinitionsBySerialNumber(string serialNumber)
    {
        var result = await _serviceConnectPoc.GetProductDefinitionsBySerialNumber(serialNumber);

        return Ok(result);
    }

    [HttpGet]
    [Route("GetMachineBySerialNumber")]

    public async Task<IActionResult> GetMachineBySerialNumber(string serialNumber)
    {
        var result = await _serviceConnectPoc.GetMachineBySerialNumber(serialNumber);

        return Ok(result);
    }

    [HttpGet]
    [Route("GetAslDocumentBySerialNumber")]
    public async Task<IActionResult> GetAslDocumentBySerialNumber(string serialNumber)
    {
        var result = await _serviceConnectPoc.GetAslDocumentBySerialNumber(serialNumber);

        return Ok(result);
    }
}
