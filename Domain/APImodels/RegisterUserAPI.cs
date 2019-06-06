
using System.ComponentModel.DataAnnotations;

namespace chat.Domain.APImodels
{
    public class RegisterUserAPI
    {
        [Required(ErrorMessage="This field is requered")]
        [Display(Name="Email")]
        public string Email {get;set;}

        [Required(ErrorMessage="This field is requered")]
        [Display(Name="Password")]
        public string Password {get;set;}
    }
}