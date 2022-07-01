using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        public UserController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        { 

            return Ok(await _context.Users.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return BadRequest("user not found");
            }
            return Ok(user);
        }


        [HttpPost]
        public async Task<ActionResult<List<User>>> AddUser(User newuser)
        {
            foreach (var user in _context.Users)
            { 
            if(newuser.Id == user.Id)
                {
                    return BadRequest("id already taken! try ");
                }
            }
            _context.Users.Add(newuser);
            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<User>>> UpdateUser(User request)
        {
            var dbuser = await _context.Users.FindAsync(request.Id);
            if (dbuser == null)
            {
                return BadRequest("user not found");
            }
            dbuser.UserName = request.UserName;
            dbuser.FirstName = request.FirstName;
            dbuser.LastName = request.LastName;

            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<User>> Delete(int id)
        {
            var dbuser = await _context.Users.FindAsync(id);
            if (dbuser == null)
            {
                return BadRequest("user not found");
            }
            _context.Users.Remove(dbuser);
            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }
    }
}
