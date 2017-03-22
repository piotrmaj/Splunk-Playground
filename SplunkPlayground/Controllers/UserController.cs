using System.Linq;
using System.Web.Http;
using Serilog;
using Splunk.DAL;

namespace SplunkPlayground.Controllers
{
    [RoutePrefix("users")]
    public class UserController: ApiController
    {
        private readonly SplunkDbContext _dbContext;
        private readonly ILogger _logger;

        public UserController(SplunkDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            
            _logger.Information("Test log {@user}", _dbContext.Users.First());
            return Ok(_dbContext.Users.ToList());
        }
    }
}