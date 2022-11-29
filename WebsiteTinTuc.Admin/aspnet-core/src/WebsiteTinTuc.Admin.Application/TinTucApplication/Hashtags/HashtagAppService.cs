using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.Authorization;
using WebsiteTinTuc.Admin.Entities;
using WebsiteTinTuc.Admin.Helpers;
using WebsiteTinTuc.Admin.Models;
using WebsiteTinTuc.Admin.TinTucApplication.Hashtags.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.Hashtags
{
    [AbpAuthorize]
    public class HashtagAppService : AdminAppServiceBase, IHashtagAppService
    {
        [AbpAuthorize(PermissionNames.Pages_Create_Hashtag)]
        public async Task CreateHashtagAsync(HashtagRequest input)
        {
            await SaveHashtagAsync(input);
        }

        [AbpAuthorize(PermissionNames.Pages_Update_Hashtag)]
        public async Task UpdateHashtagAsync(HashtagRequest input)
        {
            await SaveHashtagAsync(input);
        }

        private async Task SaveHashtagAsync(HashtagRequest input)
        {
            bool checkExists = await WorkScope.GetAll<Hashtag>().AnyAsync(x => x.Name == input.Name && x.Id != input.Id);
            if (checkExists)
                throw new UserFriendlyException("Hashtag đã tồn tại");

            if (input.Id.HasValue)
            {
                var hashtag = await WorkScope.GetAll<Hashtag>().FirstOrDefaultAsync(x => x.Id == input.Id);
                if (hashtag == default)
                    throw new UserFriendlyException("Hashtag không tồn tại");

                hashtag.Name = input.Name;
                hashtag.IsHot = input.IsHot;
                hashtag.HashtagUrl = input.Name.RemoveSign4VietnameseString().ToIdentifier();
                await WorkScope.UpdateAsync(hashtag);
            }
            else
            {
                var hashtag = new Hashtag
                {
                    Name = input.Name,
                    IsHot = input.IsHot,
                    HashtagUrl = input.Name.RemoveSign4VietnameseString().ToIdentifier()
                };
                await WorkScope.InsertAsync(hashtag);
            }
        }

        public async Task<HashtagModel> GetHashtagByIdAsync(Guid id)
        {
            var hashtag = await WorkScope.GetAll<Hashtag>().Select(x => new HashtagModel
            {
                Id = x.Id,
                HashtagUrl = x.HashtagUrl,
                Name = x.Name,
                IsHot = x.IsHot
            }).FirstOrDefaultAsync(x => x.Id == id);
            if (hashtag == default)
                throw new UserFriendlyException("Hashtag không tồn tại");

            return hashtag;
        }

        [AbpAuthorize(PermissionNames.Pages_Delete_Hashtag)]
        public async Task DeleteAsync(Guid id)
        {
            var hashtag = await WorkScope.GetAll<Hashtag>().FirstOrDefaultAsync(x => x.Id == id);
            if (hashtag == default)
                throw new UserFriendlyException("Hashtag không tồn tại");

            await WorkScope.DeleteAsync(hashtag);
        }

        [AbpAuthorize(PermissionNames.Pages_View_Hashtag)]
        public async Task<PagedResultDto<HashtagModel>> GetAllHashtagPagingAsync(PageRequest input)
        {
            var query = WorkScope.GetAll<Hashtag>()
                                      .WhereIf(!input.SearchText.IsNullOrWhiteSpace(), x => x.Name.Contains(input.SearchText))
                                      .Select(x => new HashtagModel
                                      {
                                          HashtagUrl = x.HashtagUrl,
                                          Id = x.Id,
                                          Name = x.Name,
                                          CreationTime = x.CreationTime,
                                          IsHot = x.IsHot
                                      });
            int totalCount = await query.CountAsync();
            var hashtags = await query.PageBy((input.CurrentPage - 1) * input.PageSize, input.PageSize).ToListAsync();
            return new PagedResultDto<HashtagModel>(totalCount, hashtags);
        }

        [AbpAllowAnonymous]
        public async Task<List<HashtagModel>> GetAllHashtags()
        {
            return await WorkScope.GetAll<Hashtag>()
                        .Select(x => new HashtagModel
                        {
                            HashtagUrl = x.HashtagUrl,
                            Id = x.Id,
                            Name = x.Name,
                            CreationTime = x.CreationTime,
                            IsHot = x.IsHot
                        }).ToListAsync();
        }

        [AbpAllowAnonymous]
        public async Task<List<HashtagModel>> GetAllHashtagIsHot()
        {
            var list = await WorkScope.GetAll<Hashtag>()
                            .Where(x => !x.IsHot)
                            .OrderByDescending(x => x.CreationTime)
                            .Select(x => new HashtagModel
                            {
                                HashtagUrl = x.HashtagUrl,
                                Name = x.Name,
                                Id = x.Id
                            }).ToListAsync();
            return list;
        }
    }
}
