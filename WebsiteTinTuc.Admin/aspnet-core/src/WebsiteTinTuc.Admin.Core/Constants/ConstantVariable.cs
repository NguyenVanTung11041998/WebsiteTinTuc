namespace WebsiteTinTuc.Admin.Constants
{
    public static class ConstantVariable
    {
        public const string RootFolder = "wwwroot";
        public const string UploadFolder = "upload";
        public const string Company = "company";
        public const string CV = "cv";
        public const string Nationality = "nationality";
        public static string ConectionString { get; set; }
        public static string ConectionStringDefault { get; set; }
        public static string ServerNameDefault { get; set; } = ".";
        public static string UserNameDefault { get; set; }
        public static string PasswordDefault { get; set; }
        public static string DatabaseNameDefault { get; set; } = "TuyenDungDB";
        public static string ServerName { get; set; } = ".";
        public static string UserName { get; set; }
        public static string Password { get; set; }
        public static string DatabaseName { get; set; } = "TuyenDungDB";
        public static string WebUserUrl { get; set; }
    }
}
