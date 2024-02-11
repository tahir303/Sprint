using System.ComponentModel.DataAnnotations;

namespace Car_Management.Models
{
    public class CarType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Type { get; set; }

    }
}
