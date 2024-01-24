namespace DTO.Utils
{
    public class Paging
    {
        public const int MaxPageSize = 50;
        public const int DefaultPageSize = 3;
        public const int DefaultFirstPage = 1;

        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        public Paging() { }
        
        public Paging(int? pageSize , int? pageIndex) 
        {
            PageSize = pageSize ?? DefaultPageSize;
            PageIndex = pageIndex ?? DefaultFirstPage;
        }
    }
}
