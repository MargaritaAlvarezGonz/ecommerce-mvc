using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Blossom_API.Models
{
    //Aquí pongo los campos que vamos a llenar
    public class Blossom
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductDescrip { get; set; }

        [Required]
        public double Price { get; set; }
        public string Brand { get; set; }

        public int Stock { get; set; }

        public string ImageUrl { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        

    }
}
