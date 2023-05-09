using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Models
{
    public class SignUpModel
    {
        [Required]



        public string Firstname { get; set; }



        [Required]

        public string Lastname { get; set; }



        [Required]

        [EmailAddress]

        public string Email { get; set; }



        [Required]

        [Compare("ConformPassword")]

        public string Password { get; set; }

        [Required]
         
        public string ConformPassword { get; set; }
    }
}
