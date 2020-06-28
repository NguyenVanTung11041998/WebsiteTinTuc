using Abp.Application.Services.Dto;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebsiteTinTuc.Admin.IoC
{
    public interface IWorkScope : ITransientDependency
    {
        IRepository<TEntity, TPrimaryKey> GetRepo<TEntity, TPrimaryKey>() where TEntity : class, IEntity<TPrimaryKey>;
        IRepository<TEntity, Guid> GetRepo<TEntity>() where TEntity : class, IEntity<Guid>;
        IRepository<TEntity, Guid> Repository<TEntity>() where TEntity : class, IEntity<Guid>;
        IQueryable<TEntity> GetAll<TEntity, TPrimaryKey>() where TEntity : class, IEntity<TPrimaryKey>;
        IQueryable<TEntity> GetAll<TEntity>() where TEntity : class, IEntity<Guid>;
        IQueryable<TEntity> All<TEntity>() where TEntity : class, IEntity<Guid>;

        TEntity Clone<TEntity>(TEntity entity) where TEntity : class, IEntity<Guid>;
        Guid CloneAndGetId<TEntity>(TEntity entity) where TEntity : class, IEntity<Guid>;

        IEnumerable<TEntity> InsertRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity<Guid>;
        Task<IEnumerable<TEntity>> InsertRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity<Guid>;
        TEntity Insert<TEntity>(TEntity entity) where TEntity : class, IEntity<Guid>;
        Task<TEntity> InsertAsync<TEntity>(TEntity entity) where TEntity : class, IEntity<Guid>;
        Guid InsertAndGetId<TEntity>(TEntity entity) where TEntity : class, IEntity<Guid>;
        Task<Guid> InsertAndGetIdAsync<TEntity>(TEntity entity) where TEntity : class, IEntity<Guid>;
        TEntity Update<TEntity>(TEntity entity) where TEntity : class, IEntity<Guid>;
        Task<TEntity> UpdateAsync<TEntity>(TEntity entity) where TEntity : class, IEntity<Guid>;
        Guid InsertOrUpdateAndGetId<TEntity>(TEntity entity) where TEntity : class, IEntity<Guid>;
        Task<Guid> InsertOrUpdateAndGetIdAsync<TEntity>(TEntity entity) where TEntity : class, IEntity<Guid>;
        void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity<Guid>;
        void Delete<TEntity>(Guid id) where TEntity : class, IEntity<Guid>;
        Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class, IEntity<Guid>;
        Task DeleteAsync<TEntity>(Guid id) where TEntity : class, IEntity<Guid>;
        void SoftDelete<TEntity>(TEntity entity) where TEntity : class, IEntity<Guid>, ISoftDelete;
        Task SoftDeleteAsync<TEntity>(TEntity entity) where TEntity : class, IEntity<Guid>, ISoftDelete;
        TEntity Get<TEntity>(Guid id) where TEntity : class, IEntity<Guid>;
        Task<TEntity> GetAsync<TEntity>(Guid id) where TEntity : class, IEntity<Guid>;
        Task<IEnumerable<TEntity>> UpdateRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity<Guid>;
        IEnumerable<TEntity> UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity<Guid>;
        Task<IEnumerable<TEntityDto>> Sync<TEntityDto, TEntity>(IEnumerable<TEntityDto> input)
            where TEntity : class, IEntity<Guid>
            where TEntityDto : class, IEntityDto<Guid?>;
        Task<IEnumerable<TEntityDto>> Sync<TEntityDto, TEntity>(IEnumerable<TEntityDto> input, Expression<Func<TEntity, bool>> condition)
            where TEntity : class, IEntity<Guid>
            where TEntityDto : class, IEntityDto<Guid?>;
        Task<IEnumerable<TEntityDto>> Sync<TEntityDto, TEntity>(
            IEnumerable<TEntityDto> input,
            Expression<Func<TEntity, bool>> condition,
            Func<TEntityDto, TEntityDto> merge)
            where TEntity : class, IEntity<Guid>
            where TEntityDto : class, IEntityDto<Guid?>;
    }
}
