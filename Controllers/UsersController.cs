using Dating_App_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dating_App_API.Controllers
{
    public class UsersController : BaseAPIController
    {
        public UsersController(DataBaseContext db) : base(db)
        {
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return await Db.Users.ToListAsync();
            
        }
        
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> GetUser(int id)
        {

            //if(id is null )
            //{
            var selectedUser = await Db.Users.FirstOrDefaultAsync(u=>u.Id == id);

            return selectedUser is null ? BadRequest("No User With This Id.") : selectedUser;
            //}   
            //return BadRequest("No User With This Id.");



        }
    }
}
