using WebsiteTinTuc.Admin.Models;

namespace WebsiteTinTuc.Admin.TinTucApplication.Homes.Dto
{
    public class PostPagingRequest : PageRequest
    {
        public string Location { get; set; }
        public PostType PostType { get; set; } = PostType.All;
    }

    public enum PostType : byte
    {
        All = 0,
        Hashtag = 1,
        BranchJob = 2
    }
}
