namespace Blosom_API2.Models.Specifications
{
    public class PagedList<T> : List<T>
    {
        public Metadata MetaData { get; set; }
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new Metadata
            {
                TotalCount = count,
                PagesSize = pageSize,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
            AddRange(items);
        }

        public static PagedList<T> ToPagedList(IEnumerable<T> entidad, int pageNumner, int pageSize)
        {
            var count = entidad.Count();
            var items = entidad.Skip((pageNumner - 1) * pageSize)
                .Take(pageSize).ToList();

            return new PagedList<T>(items, count, pageNumner, pageSize);

        }
    }


}