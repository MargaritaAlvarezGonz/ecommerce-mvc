using System.ComponentModel.DataAnnotations;

namespace Blossom_API.Models.Dto
{
    //Aquí se incluyen las propiedaes que deseo mostrar cuando exponga los datos
    public class BlossomDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        public string ProductDescrip { get; set; }
        public double Price { get; set; }

        public string Brand { get; set; }

        public string ImageUrl { get; set; }
        

    }
}

