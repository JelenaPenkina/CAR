using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAR.Core.Domain
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? Make { get; set; }

        [Required]
        [StringLength(50)]
        public string? Model { get; set; }

        [Required]
        [Range(1886, 2026, ErrorMessage = "Year must be between 1886 (first car) and 2026 (current year)")]
        public int Year { get; set; }

        [Required]
        [StringLength(30)]
        public string? Color { get; set; }

        [Required]
        [Range(0, 1000000)]
        public int Price { get; set; }

        //[Required]
        //[Range(0, 1000000)]
        //[Column(TypeName = "decimal(18,2)")] 
        //public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
