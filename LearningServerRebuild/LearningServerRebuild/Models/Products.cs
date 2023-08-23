using System.ComponentModel.DataAnnotations;

namespace LearningServerRebuild.Models
{
    public class Products
    {
        public Guid id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string name { get; set; }

        public float price { get; set; }

        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }

        // Relacionamento com User
        public Guid Product_user { get; set; }
    }
}
