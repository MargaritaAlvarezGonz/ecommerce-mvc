using Blossom_API.Models.Dto;

namespace Blossom_API.Data
{
    public static class BlossomStore
    {
        public static List<BlossomDto> blossomList = new List<BlossomDto>
        {
            new BlossomDto{Id=1, Name="Organic cream", Price=2.7,  ProductDescrip="The Best Organic Cream"},
            new BlossomDto{Id=2, Name="Massage Brush", Price=3.7,  ProductDescrip="The Best Organic Massage Brush"},
        };
    }
}
