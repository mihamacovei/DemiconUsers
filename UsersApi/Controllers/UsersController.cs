using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UsersApi.Models;
using System.Web.Http.Description;
using UsersApi.BusinessLayer;
using Microsoft.Extensions.DependencyInjection;

namespace DemiconUsers.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UserController>

        private readonly IUserService _userService;
        private readonly IServiceScopeFactory _dbContextScopeFactory;

        public UsersController(IUserService userService, IServiceScopeFactory dbContextScopeFactory) 
        { 
            _userService = userService;
            _dbContextScopeFactory = dbContextScopeFactory;
        }


        [HttpGet]
        [Route("/users")]
        [ResponseType(typeof(UsersResultModel))]
        public async Task<ActionResult<UsersResultModel>> GetUsers(string country="")
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateScope())
            {
                return Ok(await _userService.GetUsersByCountry(country));
            }
        }
        /*

        // GET: api/X/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/X
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/X/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
