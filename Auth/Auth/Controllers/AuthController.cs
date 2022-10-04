using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Auth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly EntityDbContext _dbContext;
        public AuthController(EntityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public string? GetUserName(int id)
        {
            var user = _dbContext.Auths?.FirstOrDefault(User => User.Id == id)?.Name;
            return user;
        }
    }
}