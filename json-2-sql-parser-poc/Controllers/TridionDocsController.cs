using json_2_sql_parser_poc.Common.TridionDocs.TridionDocs;
using json_2_sql_parser_poc.Models.TridionDocs.Requests;
using json_2_sql_parser_poc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace json_2_sql_parser_poc.Controllers;

[Route("api/[controller]")]
[ApiController]

public class TridionDocsController : Controller
{
    private readonly ITridionDocsPoC _tridionDocsPoC;

    public TridionDocsController(ITridionDocsPoC tridionDocsPoC)
    {
        _tridionDocsPoC = tridionDocsPoC;
    }

    [HttpGet]
    [Route("Health")]
    public IActionResult GetHealth()
    {
        return Ok();
    }

    [HttpGet]
    [Route("ListOfValues")]
    public async Task<IActionResult> GetListOfValues(ListOfValuesEnum value)
    {
        var result = await _tridionDocsPoC.GetListOfValues(value);

        return Ok(result);
    }

    [HttpGet]
    [Route("Version")]
    public async Task<IActionResult> GetVersion()
    {
        var result = await _tridionDocsPoC.GetVersion();

        return Ok(result);
    }

    [HttpPost]
    [Route("Publications")]
    public async Task<IActionResult> GetPublications(PublicationsByLogicalIdRequest request)
    {
        var result = await _tridionDocsPoC.GetPublicationsByLogicalId(request);

        return Ok(result);
    }
}
