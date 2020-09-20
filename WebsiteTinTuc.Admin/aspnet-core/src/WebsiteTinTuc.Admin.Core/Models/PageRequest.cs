namespace WebsiteTinTuc.Admin.Models
{
    public class PageRequest
    {
        public virtual int PageSize { get; set; }
        public virtual int CurrentPage { get; set; }
        public virtual string SearchText { get; set; }
    }
}
