namespace WebsiteTinTuc.Admin.Models
{
    public class PageRequest
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public string SearchText { get; set; }
    }
}
