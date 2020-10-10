using WebsiteTinTuc.Admin.Models;

namespace WebsiteTinTuc.Admin.TinTucApplication.Homes.Dto
{
    public class PostPagingRequest : PageRequest
    {
        public string Location { get; set; }
    }
}
