using System.ComponentModel.DataAnnotations;

namespace Blossom_Web.Models.Dto
{
    //Aquí se incluyen las propiedaes que deseo mostrar cuando exponga los datos
    public class BlossomCreateDto
    {
        

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        
        public string ProductDescrip { get; set; }

        [Required]
        public double Price { get; set; }

        
        public string Brand { get; set; }

        
        public string ImageUrl { get; set; }
        

    }
}

