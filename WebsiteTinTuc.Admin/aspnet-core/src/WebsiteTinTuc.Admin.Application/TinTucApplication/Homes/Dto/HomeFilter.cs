using WebsiteTinTuc.Admin.Models;

namespace WebsiteTinTuc.Admin.TinTucApplication.Homes.Dto
{
    public class HomeFilter : PageRequest
    {
        public bool? IsHot { get; set; }
    }
}
