using Dating_App_API.DTO;
using Dating_App_API.Interfaces;
using Dating_App_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Dating_App_API.Controllers
{
    public class AccountController : BaseAPIController
    {
        public ITokenService tokenService;
        public AccountController(DataBaseContext db , ITokenService _tokenService) : base(db)
        {
            tokenService = _tokenService;
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO regDtoUser)
        {
           
                if ( await IsUserExist(regDtoUser.UserName) )
                {
                    return BadRequest("User Name Is Exist.");
                }
                //generate  security obj 
                using var hmac = new HMACSHA512();
                //Identify New User
                var newUser = new User
            {
                UserName     = regDtoUser.UserName.ToLower(),
                Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(regDtoUser.Password)),
                PasswordSalt = hmac.Key
            };

               await Db.Users.AddAsync(newUser);
               await Db.SaveChangesAsync();

                return new UserDTO {
                UserName = newUser.UserName,
                Token = tokenService.CreateToken(newUser)
            };
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> LogIn(LogInDTO logingDtoUser)
        {

            User loggedUser = await Db.Users.SingleOrDefaultAsync(user => user.UserName == logingDtoUser.UserName);
            if (loggedUser is null) return BadRequest("User Is Not Exist.");

            using var hmac =  new HMACSHA512(loggedUser.PasswordSalt);

            byte[] computedPassword =  hmac.ComputeHash(Encoding.UTF8.GetBytes(logingDtoUser.Password));

            for( int i = 0; i <= computedPassword.Length-1; i++ )
            {

                if(computedPassword[i] != loggedUser.Password[i])
                {
                    return BadRequest("Your user name OR Password is not correct. ") ;
                }

            }

            return new UserDTO
            {
                UserName = loggedUser.UserName,
                Token = tokenService.CreateToken(loggedUser)
            };


        }

        private async Task<bool> IsUserExist(string UserName)
        {
            return await Db.Users.AnyAsync(user => user.UserName == UserName.ToLower()) ;

        }














    }
}
