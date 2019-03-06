using System.Collections.Generic;
using System.Linq;

namespace PhotoManager.Model.ViewModels
{
    public class ItemListViewModel <T>
    {
        public ItemListViewModel(List<T> viewModels, PageInfo pageInfo)
        {
            ViewModels = viewModels;
            PageInfo=pageInfo;
        }
        public ItemListViewModel() { }
        public List<T> ViewModels { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}