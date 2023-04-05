using Blossom_API.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Blosom_API2.Models
{
    public class NumberBlossom
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BlossomNo { get; set; }

        [Required]
        public int BlossomId { get; set; }

        [ForeignKey("BlossomId")]
        public Blossom Blossom { get; set; }

        public string Details { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}
