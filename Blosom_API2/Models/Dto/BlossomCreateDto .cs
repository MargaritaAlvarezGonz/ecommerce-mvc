using System.ComponentModel.DataAnnotations;

namespace Blossom_API.Models.Dto
{
    //Aquí se incluyen las propiedaes que deseo mostrar cuando exponga los datos
    public class BlossomCreateDto
    {
        

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        public string ProductDescrip { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string ImageUrl { get; set; }
        

    }
}

