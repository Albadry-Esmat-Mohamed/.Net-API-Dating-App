using System.ComponentModel.DataAnnotations;

namespace Dating_App_API.DTO
{
    public class RegisterDTO
    {


        [Required]
        public string? UserName { get; set; }

        [Required]
        [DataType("Password")]
        public string?  Password { get; set; }


    }
}
