﻿using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.Entities;
using WebsiteTinTuc.Admin.Helpers;
using WebsiteTinTuc.Admin.TinTucApplication.Hashtags.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.Hashtags
{
    [AbpAuthorize]
    public class HashtagAppService : AdminAppServiceBase
    {
        public async Task SaveHashtagAsync(HashtagRequest input)
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
                hashtag.HashtagUrl = input.Name.RemoveSign4VietnameseString().ToIdentifier();
                await WorkScope.UpdateAsync(hashtag);
            }
            else
            {
                var hashtag = new Hashtag
                {
                    Name = input.Name,
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
                Name = x.Name
            }).FirstOrDefaultAsync(x => x.Id == id);
            if (hashtag == default)
                throw new UserFriendlyException("Hashtag không tồn tại");

            return hashtag;
        }

        public async Task DeleteAsync(Guid id)
        {
            var hashtag = await WorkScope.GetAll<Hashtag>().FirstOrDefaultAsync(x => x.Id == id);
            if (hashtag == default)
                throw new UserFriendlyException("Hashtag không tồn tại");

            await WorkScope.DeleteAsync(hashtag);
        }

        public async Task<PagedResultDto<HashtagModel>> GetAllHashtagPagingAsync(PageRequest input)
        {
            var hashtags = WorkScope.GetAll<Hashtag>()
                                      .WhereIf(!input.SearchText.IsNullOrWhiteSpace(), x => x.Name.Contains(input.SearchText))
                                      .Select(x => new HashtagModel
                                      {
                                          HashtagUrl = x.HashtagUrl,
                                          Id = x.Id,
                                          Name = x.Name,
                                          CreationTime = x.CreationTime
                                      });
            int totalCount = await hashtags.CountAsync();
            var categories = await hashtags.Skip((input.CurrentPage - 1) * input.PageSize).Take(input.PageSize).ToListAsync();
            return new PagedResultDto<HashtagModel>(totalCount, categories);
        }
    }
}
