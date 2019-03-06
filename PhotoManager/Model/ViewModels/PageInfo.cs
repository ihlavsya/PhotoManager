using System;

namespace PhotoManager.Model.ViewModels
{
    public class PageInfo
    {
        public PageInfo(int pageNumber, int pageSize, int totalIems)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItems = totalIems;
        }
        public int PageNumber { get; set; } // current number page
        public int PageSize { get; set; } // quantity of objects on the page
        public int TotalItems { get; set; } // total objects
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / PageSize);
    }
}