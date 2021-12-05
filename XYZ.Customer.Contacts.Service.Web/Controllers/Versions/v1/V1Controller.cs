using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace XYZ.Customer.Contacts.Service.Web.Controllers.Versions.v1
{
 
    /// <summary>
    /// Base class for controllers in ApiVersion 1.0
    /// </summary>
    [ApiController]
    [Route("api/v1")]
    [Produces("application/json")]
    public abstract class V1Controller : ControllerBase
    {
    }
}
