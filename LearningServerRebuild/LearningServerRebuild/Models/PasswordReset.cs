using System.ComponentModel.DataAnnotations;

namespace LearningServerRebuild.Models
{
    public class PasswordReset
    {
        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }
    }
}
