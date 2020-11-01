namespace WebsiteTinTuc.Admin.Configuration.Dto
{
    public class ConnectionStringModel
    {
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAuthenticate { get; set; }
    }
}
