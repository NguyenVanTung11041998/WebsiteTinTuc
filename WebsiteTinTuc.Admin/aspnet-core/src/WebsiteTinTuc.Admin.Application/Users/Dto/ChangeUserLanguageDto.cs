using System.ComponentModel.DataAnnotations;

namespace WebsiteTinTuc.Admin.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}