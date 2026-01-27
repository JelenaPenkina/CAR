using System.ComponentModel.DataAnnotations;
namespace CAR.Models
{
    public class CarViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Make is required")]
        [StringLength(50)]
        public string? Make { get; set; }

        [Required(ErrorMessage = "Model is required")]
        [StringLength(50)]
        public string? Model { get; set; }

        [Required(ErrorMessage = "Year is required")]
        [Range(1886, 2026, ErrorMessage = "Year must be between 1886 (first car) and 2026 (current year)")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Color is required")]
        [StringLength(30)]
        public string? Color { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, 1000000, ErrorMessage = "Price must be positive")]
        public int Price { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
