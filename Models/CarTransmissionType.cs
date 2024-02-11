using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace Car_Management.Models
{
    
    public class CarTransmissionType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }


    }
}
