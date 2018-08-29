using System.ComponentModel.DataAnnotations;

namespace WebApplication.Dtos
{
    public class AttributeValidatedDto
    {
        [Range(0, 1000)]
        public int Age { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
