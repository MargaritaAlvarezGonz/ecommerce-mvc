using System.ComponentModel.DataAnnotations;

namespace Blosom_API2.Models.Dto
{
    public class NumberBlossomDto
    {
        [Required]
        public int BlossomNo { get; set; }

        [Required]
        public int BlossomId { get; set; }

        public string Details { get; set; }
    }
}
