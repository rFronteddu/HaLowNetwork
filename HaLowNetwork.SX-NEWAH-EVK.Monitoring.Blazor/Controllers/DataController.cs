using Microsoft.AspNetCore.Mvc;

namespace HaLowNetwork.SX_NEWAH_EVK.Monitoring.Blazor.Controllers;

public class DataController : Controller
{

    [HttpPost("/data/add")]
    public IActionResult AddData([FromBody] dynamic data, string mac)
    {

        return Ok();
    }
    
}