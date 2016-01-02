using System.ComponentModel.DataAnnotations;

namespace Quilt4.Service.Controllers
{
    public class LoginUserBindingModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}