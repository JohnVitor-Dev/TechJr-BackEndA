using System.ComponentModel.DataAnnotations;

namespace LearningServerRebuild.Models
{
    public class Product
    {

        [Required(ErrorMessage = "Name is required.")]
        public string name { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        public float? price { get; set; }

    }
}
