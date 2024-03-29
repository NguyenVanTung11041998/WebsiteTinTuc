﻿using Abp.Authorization.Users;
using Abp.Extensions;
using System;
using System.Collections.Generic;
using WebsiteTinTuc.Admin.Entities;

namespace WebsiteTinTuc.Admin.Authorization.Users
{
    public class User : AbpUser<User>
    {
        public const string DefaultPassword = "123qwe";

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Roles = new List<UserRole>()
            };

            user.SetNormalizedNames();

            return user;
        }
        public UserType UserType { get; set; }
        public virtual ICollection<CV> CVs { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
    }

    public enum UserType
    {
        Admin = 0,
        Hr = 1,
        User = 2
    }
}
