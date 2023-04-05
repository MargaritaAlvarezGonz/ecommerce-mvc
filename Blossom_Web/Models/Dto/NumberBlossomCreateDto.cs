using System.ComponentModel.DataAnnotations;

namespace Blossom_Web.Models.Dto
{
    public class NumberBlossomCreateDto
    {
        [Required]
        public int BlossomNo { get; set; }

        [Required]
        public int BlossomId { get; set; }

        public string Details { get; set; }
    }
}
