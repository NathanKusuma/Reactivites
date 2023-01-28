using System.ComponentModel.DataAnnotations;
namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$",ErrorMessage ="Password must be complex")]
        public string Password { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}

//?=.*\\d one of the password must to be a number
//(?=.*[a-z]) at least one of the char must to be a lowercase
//(?=.*[A-Z]) at least one of the char must to be a uppercase
//.{4,8} Password needs to between 4-8 character
//$ to finish of requirment