using PagedList;
using System;
using System.Collections.Generic;

namespace DTO
{
    public class PageList<T> : List<T>, IPagedList
    {
        public List<T> Items { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public string SearchString { get; set; }
        public int Pages { get; set; }

        public PageList() { }

        public PageList(List<T> items, int? count,int pageIndex, int pageSize, string searchString)
        {
            Items = items;
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling((int)count / (double)pageSize);
            TotalItems = (int)count;
            SearchString = searchString;
            Pages = pageSize;
        }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public int PageCount => TotalPages;

        public int TotalItemCount => TotalItems;

        public int PageNumber => PageIndex; 

        public int PageSize => Pages;

        public bool IsFirstPage => PageIndex == 1;

        public bool IsLastPage => PageIndex == TotalPages;

        public int FirstItemOnPage => (PageIndex - 1) * PageSize + 1;

        public int LastItemOnPage => Math.Min(PageIndex * PageSize, TotalItems); 
    }
}
