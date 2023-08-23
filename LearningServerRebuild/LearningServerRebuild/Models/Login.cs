using System.ComponentModel.DataAnnotations;

namespace LearningServerRebuild.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Email is required.")]
        [IfmaEmail]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string password { get; set; }

    }
}
