using System.ComponentModel.DataAnnotations;

namespace Quilt4.Service.Models
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