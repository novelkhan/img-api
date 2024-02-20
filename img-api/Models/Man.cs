using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace img_api.Models
{
    public class Man
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name must be at least {2}, and maximum {1} characters")]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "City name must be at least {2}, and maximum {1} characters")]
        public string City { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Country name must be at least {2}, and maximum {1} characters")]
        public string Country { get; set; }
    }
}
