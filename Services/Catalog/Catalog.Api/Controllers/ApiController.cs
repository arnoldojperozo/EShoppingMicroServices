using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

[ApiVersion("1")]
[Route("api/v{version:apiversion}/[controller])"]
public class ApiController : ControllerBase
{
    
}