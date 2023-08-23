using System.ComponentModel.DataAnnotations;

namespace LearningServerRebuild.Models
{
    public class EmailToReset
    {
        [Required(ErrorMessage = "Email is required.")]
        [IfmaEmail]
        public string email { get; set; }

    }
}
