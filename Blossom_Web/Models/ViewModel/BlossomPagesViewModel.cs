using Blossom_Web.Models.Dto;

namespace Blossom_Web.Models.ViewModel
{
    public class BlossomPagesViewModel
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }

        public string Previous { get; set; } = "diabled";
        public string Next { get; set; } = "";
        public IEnumerable<BlossomDto> BlossomList { get; set; }
    }
}
