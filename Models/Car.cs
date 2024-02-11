using System.ComponentModel.DataAnnotations;

namespace Car_Management.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int ManufacturerId { get; set; }
        public Manufacturer? Manufacturer { get; set; }

        [Required]
        public int CarTypeId { get; set; }
        public CarType? CarType { get; set; }

        [Required]
        [RegularExpression(@"^\d\.\dL$",ErrorMessage = "Should be 4 characters long. With first and third\r\ncharacter number, second should be a “.” And last character should be “L”\r\n")]
        public string Engine { get; set; }

        [Required]
        public int BHP { get; set; }

        [Required]
        public int CarTransmissionTypeId { get; set; }
        public CarTransmissionType? CarTransmissionType { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required]
        public int Seats { get; set; }

        [Required]
        public string AirBagDetails { get; set; }

        [Required]
        public string BootSpace { get; set; }

        [Required]
        public float Price { get; set; }
    }
}
