using System.ComponentModel.DataAnnotations;

namespace Car_Management.Models
{
    public class Manufacturer
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? ContactNo { get; set; }

        [Required]
        public string? RegisteredOffice { get; set; }



    }
}
