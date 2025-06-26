namespace Kino.Core.Pagination
{
    public class PaginationMetadata
    {
        public PaginationMetadata() { }
        public PaginationMetadata(int ItemsCount, int PageCount)
        {
            this.ItemsCount = ItemsCount;
            this.PageCount = PageCount;
        }
        public int ItemsCount { get; set; }
        public int PageCount { get; set; }
    }
}
