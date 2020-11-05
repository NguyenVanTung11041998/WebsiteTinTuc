using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using WebsiteTinTuc.Admin.Roles.Dto;
using WebsiteTinTuc.Admin.Users.Dto;

namespace WebsiteTinTuc.Admin.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);

        Task<string> GetLanguageAsync();

        Task<bool> ChangePassword(ChangePasswordDto input);

        Task CreateUserAsync(CreateUserDto input);

        Task<UserDto> UpdateCurrentUserAsync(UserDto input);
    }
}
