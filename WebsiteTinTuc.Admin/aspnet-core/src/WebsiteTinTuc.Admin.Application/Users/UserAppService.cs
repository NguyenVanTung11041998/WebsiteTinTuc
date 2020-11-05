using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.Authorization;
using WebsiteTinTuc.Admin.Authorization.Accounts;
using WebsiteTinTuc.Admin.Authorization.Roles;
using WebsiteTinTuc.Admin.Authorization.Users;
using WebsiteTinTuc.Admin.Constants;
using WebsiteTinTuc.Admin.Roles.Dto;
using WebsiteTinTuc.Admin.Users.Dto;

namespace WebsiteTinTuc.Admin.Users
{
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAbpSession _abpSession;
        private readonly LogInManager _logInManager;
        private const string PasswordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{10,})";

        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            IPasswordHasher<User> passwordHasher,
            IAbpSession abpSession,
            LogInManager logInManager)
            : base(repository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _abpSession = abpSession;
            _logInManager = logInManager;
        }

        private bool IsStrongPassword(string password)
        {
            return Regex.IsMatch(password, PasswordRegex);
        }

        public override async Task<UserDto> CreateAsync(CreateUserDto input)
        {
            if (!IsStrongPassword(input.Password))
                throw new UserFriendlyException("Mật khẩu phải từ 10 ký tự, chứa chữ hoa, thường và ký tự đặc biệt");

            CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);

            user.Surname = user.Name;
            user.TenantId = AbpSession.TenantId;
            user.IsEmailConfirmed = true;

            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            CheckErrors(await _userManager.CreateAsync(user, input.Password));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            }

            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(user);
        }

        public override async Task<UserDto> UpdateAsync(UserDto input)
        {
            CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            }

            return await GetAsync(input);
        }

        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }

        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

        public async Task ChangeLanguage(ChangeUserLanguageDto input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                LocalizationSettingNames.DefaultLanguage,
                input.LanguageName
            );
        }

        public async Task<string> GetLanguageAsync()
        {
            return await SettingManager.GetSettingValueForUserAsync(LocalizationSettingNames.DefaultLanguage, AbpSession.TenantId, AbpSession.UserId ?? 0);
        }

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            user.SetNormalizedNames();
            return user;
        }

        protected override void MapToEntity(UserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

        protected override UserDto MapToEntityDto(User user)
        {
            var roleIds = user.Roles.Select(x => x.RoleId).ToArray();

            var roles = _roleManager.Roles.Where(r => roleIds.Contains(r.Id)).Select(r => r.NormalizedName);

            var userDto = base.MapToEntityDto(user);
            userDto.RoleNames = roles.ToArray();

            return userDto;
        }

        protected override IQueryable<User> CreateFilteredQuery(PagedUserResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Roles)
                .WhereIf(input.From.HasValue, x => x.CreationTime >= input.From)
                .WhereIf(input.To.HasValue, x => x.CreationTime < input.To)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.UserName.Contains(input.Keyword) || x.Name.Contains(input.Keyword) || x.EmailAddress.Contains(input.Keyword))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);
        }

        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            var user = await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }

            return user;
        }

        protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedUserResultRequestDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<bool> ChangePassword(ChangePasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attemping to change password.");
            }
            long userId = _abpSession.UserId.Value;
            var user = await _userManager.GetUserByIdAsync(userId);
            var loginAsync = await _logInManager.LoginAsync(user.UserName, input.CurrentPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Existing Password' did not match the one on record.  Please try again or contact an administrator for assistance in resetting your password.");
            }
            if (!new Regex(AccountAppService.PasswordRegex).IsMatch(input.NewPassword))
            {
                throw new UserFriendlyException("Passwords must be at least 8 characters, contain a lowercase, uppercase, and number.");
            }
            user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
            CurrentUnitOfWork.SaveChanges();
            return true;
        }

        public async Task<bool> ResetPassword(ResetPasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attemping to reset password.");
            }
            long currentUserId = _abpSession.UserId.Value;
            var currentUser = await _userManager.GetUserByIdAsync(currentUserId);
            var loginAsync = await _logInManager.LoginAsync(currentUser.UserName, input.AdminPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Admin Password' did not match the one on record.  Please try again.");
            }
            if (currentUser.IsDeleted || !currentUser.IsActive)
            {
                return false;
            }
            var roles = await _userManager.GetRolesAsync(currentUser);
            if (!roles.Contains(StaticRoleNames.Tenants.Admin))
            {
                throw new UserFriendlyException("Only administrators may reset passwords.");
            }

            var user = await _userManager.GetUserByIdAsync(input.UserId);
            if (user != null)
            {
                user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
                CurrentUnitOfWork.SaveChanges();
            }

            return true;
        }

        [AbpAllowAnonymous]
        public async Task<UserDto> UpdateCurrentUserAsync(UserDto input)
        {
            if (AbpSession.UserId != input.Id) throw new UserFriendlyException("Bạn không thể sửa tài khoản của người khác");

            CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
                user.UserType = input.RoleNames.Any(x => x == StaticRoleNames.Host.Hr) ? UserType.Hr : UserType.User;
            }

            return await GetAsync(input);
        }

        [AbpAllowAnonymous]
        public async Task CreateUserAsync(CreateUserDto input)
        {

            if (!IsStrongPassword(input.Password))
                throw new UserFriendlyException("Mật khẩu phải từ 10 ký tự, chứa chữ hoa, thường và ký tự đặc biệt");

            input.RoleNames = (await GetRoles()).Items.Where(x => x.Name != StaticRoleNames.Host.Admin).Select(x => x.Name).ToArray();
            CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.Surname = input.Name;
            user.IsEmailConfirmed = true;
            user.UserType = input.RoleNames.Any(x => x == StaticRoleNames.Host.Hr) ? UserType.Hr : UserType.User;          

            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            //CheckErrors(await _userManager.CreateAsync(user, input.Password));

            //if (input.RoleNames != null)
            //{
            //    CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            //}

            user.Password = _passwordHasher.HashPassword(user, input.Password);
            using var connection = new SqlConnection(ConstantVariable.ConectionString);
            connection.Open();
            string query = "EXEC dbo.CreateUser @UserName, @EmailAddress, @Name, @Password, @PhoneNumber, @SecurityStamp, @UserType";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", user.UserName);
            command.Parameters.AddWithValue("@EmailAddress", user.EmailAddress);
            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
            command.Parameters.AddWithValue("@SecurityStamp", Guid.NewGuid().ToString().Replace("-", ""));
            command.Parameters.AddWithValue("@UserType", (int)user.UserType);

            long id = (long)await command.ExecuteScalarAsync();

            if (id == (long)CreateUserStatus.UserNameExists)
                throw new UserFriendlyException("User name đã tồn tại");
            else if (id == (long)CreateUserStatus.EmailAddressExists)
                throw new UserFriendlyException("Email đã tồn tại");
            
            if (input.RoleNames?.Count() > 0)
            {
                command.CommandText = "EXEC dbo.CreateUserRole @Role, @UserId";
                foreach (var item in input.RoleNames)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Role", item);
                    command.Parameters.AddWithValue("@UserId", id);
                    await command.ExecuteNonQueryAsync();
                }
            }

            CurrentUnitOfWork.SaveChanges();
        }
    }

    public enum CreateUserStatus
    {
        UserNameExists = -2,
        EmailAddressExists = -1
    }
}

