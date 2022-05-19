using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dating_App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseAPIController : ControllerBase
    {
        public DataBaseContext Db { get; }
        public BaseAPIController(DataBaseContext db)
        {
            Db = db;
        }

    }
}
