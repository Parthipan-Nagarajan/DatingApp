using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : BaseApiController
    {
        private DataContext _context;

        public BuggyController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("auth")]
        [Authorize]
        public ActionResult<string> GetSecret()
        {
            return "secret String";
        }

        [HttpGet("not-found")]
        public ActionResult<string> GetNotFound()
        {
            return NotFound("Not Found");
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            AppUser usr = null;
#pragma warning disable CS8604 // Possible null reference argument.
            return usr.ToString();
#pragma warning restore CS8604 // Possible null reference argument.
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("Bad Request");
        }
    }
}