using Microsoft.EntityFrameworkCore;

namespace PDFLines
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int AllItems { get; private set; }
        public int TotalPages { get; private set; }
        public int FirstPage { get; private set; }
        public int LastPage { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            AllItems = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            FirstPage = 1;
            LastPage = TotalPages;

            this.AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
        public bool HasFirstPage => PageIndex > 1;
        public bool HasLastPage => TotalPages > 1;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}