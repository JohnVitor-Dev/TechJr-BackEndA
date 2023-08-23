using System.ComponentModel.DataAnnotations;

namespace LearningServerRebuild.Models

{
    public class Users
    {
        public Guid id { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [IfmaEmail]
        public string email { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string name { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string password { get; set; }

        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }

        
    }


}

