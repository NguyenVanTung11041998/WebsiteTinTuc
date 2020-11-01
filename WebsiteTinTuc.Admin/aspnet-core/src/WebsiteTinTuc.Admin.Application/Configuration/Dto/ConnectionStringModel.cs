using System.ComponentModel.DataAnnotations;

namespace WebsiteTinTuc.Admin.Configuration.Dto
{
    public class ConnectionStringModel
    {
        [Required]
        public string ServerName { get; set; }
        [Required]
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAuthenticate { get; set; }
    }
}
