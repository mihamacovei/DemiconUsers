using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersApi.Models;

namespace DemiconUsers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UserController>

        private readonly Context _context;
        public UsersController(Context context) { _context = context; }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<dynamic>>> Get()
        {

            var list = await Task.FromResult( _context.Set<Country>().ToList());
                //.Select(a => new Country() { Id = a.Id, Name=a.Name, Users = a.Users }).ToList();

            return list.Select(a => new
            {
                Name = a.Id,
                Users = a.Users.Select(b => new { Name = b.Id, Gender = b.Gender, Email = b.Email }).ToList()
            }).ToList();
        }

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
    }
}
