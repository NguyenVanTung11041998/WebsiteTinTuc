using Abp;
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
    public class WorkScope : AbpServiceBase, IWorkScope
    {
        IQueryable<TEntity> IWorkScope.GetAll<TEntity, TPrimaryKey>()
        {
            return (this as IWorkScope).GetRepo<TEntity, TPrimaryKey>().GetAll();
        }

        IQueryable<TEntity> IWorkScope.GetAll<TEntity>()
        {
            return (this as IWorkScope).GetRepo<TEntity, Guid>().GetAll();
        }

        IQueryable<TEntity> IWorkScope.All<TEntity>()
        {
            return (this as IWorkScope).GetRepo<TEntity, Guid>().GetAll();
        }

        IRepository<TEntity, TPrimaryKey> IWorkScope.GetRepo<TEntity, TPrimaryKey>()
        {
            var repoType = typeof(IRepository<,>);
            Type[] typeArgs = { typeof(TEntity), typeof(TPrimaryKey) };
            var repoGenericType = repoType.MakeGenericType(typeArgs);
            var resolveMethod = IocManager.Instance.GetType()
                .GetMethods()
                .First(s => s.Name == "Resolve" && !s.IsGenericMethod && s.GetParameters().Length == 1 && s.GetParameters()[0].ParameterType == typeof(Type));
            var repo = resolveMethod.Invoke(IocManager.Instance, new object[] { repoGenericType });
            return repo as IRepository<TEntity, TPrimaryKey>;
        }

        IRepository<TEntity, Guid> IWorkScope.GetRepo<TEntity>()
        {
            return (this as IWorkScope).GetRepo<TEntity, Guid>();
        }

        IRepository<TEntity, Guid> IWorkScope.Repository<TEntity>()
        {
            return (this as IWorkScope).GetRepo<TEntity, Guid>();
        }

        TEntity IWorkScope.Clone<TEntity>(TEntity entity)
        {
            entity.Id = new Guid("00000000-0000-0000-000000000000");
            return (this as IWorkScope).GetRepo<TEntity, Guid>().Insert(entity);
        }

        Guid IWorkScope.CloneAndGetId<TEntity>(TEntity entity)
        {
            entity.Id = new Guid("00000000-0000-0000-000000000000");
            return (this as IWorkScope).GetRepo<TEntity, Guid>().InsertAndGetId(entity);
        }

        IEnumerable<TEntity> IWorkScope.InsertRange<TEntity>(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                yield return (this as IWorkScope).GetRepo<TEntity, Guid>().Insert(entity);
            }
        }

        async Task<IEnumerable<TEntity>> IWorkScope.InsertRangeAsync<TEntity>(IEnumerable<TEntity> entities)
        {
            var updatedEntities = new List<TEntity>();
            foreach (var entity in entities)
            {
                updatedEntities.Add(await (this as IWorkScope).GetRepo<TEntity, Guid>().InsertAsync(entity));
            }

            return updatedEntities;
        }

        public TEntity Insert<TEntity>(TEntity entity) where TEntity : class, IEntity<Guid>
        {
            return (this as IWorkScope).GetRepo<TEntity, Guid>().Insert(entity);
        }

        public async Task<TEntity> InsertAsync<TEntity>(TEntity entity) where TEntity : class, IEntity<Guid>
        {
            return await (this as IWorkScope).GetRepo<TEntity, Guid>().InsertAsync(entity);
        }

        public Guid InsertAndGetId<TEntity>(TEntity entity) where TEntity : class, IEntity<Guid>
        {
            return (this as IWorkScope).GetRepo<TEntity, Guid>().InsertAndGetId(entity);
        }

        public async Task<Guid> InsertAndGetIdAsync<TEntity>(TEntity entity) where TEntity : class, IEntity<Guid>
        {
            return await (this as IWorkScope).GetRepo<TEntity, Guid>().InsertAndGetIdAsync(entity);
        }


        public TEntity Update<TEntity>(TEntity entity) where TEntity : class, IEntity<Guid>
        {
            return (this as IWorkScope).GetRepo<TEntity, Guid>().Update(entity);
        }

        public async Task<TEntity> UpdateAsync<TEntity>(TEntity entity) where TEntity : class, IEntity<Guid>
        {
            return await (this as IWorkScope).GetRepo<TEntity, Guid>().UpdateAsync(entity);
        }

        public Guid InsertOrUpdateAndGetId<TEntity>(TEntity entity) where TEntity : class, IEntity<Guid>
        {
            return (this as IWorkScope).GetRepo<TEntity, Guid>().InsertOrUpdateAndGetId(entity);
        }

        public async Task<Guid> InsertOrUpdateAndGetIdAsync<TEntity>(TEntity entity) where TEntity : class, IEntity<Guid>
        {
            return await (this as IWorkScope).GetRepo<TEntity, Guid>().InsertOrUpdateAndGetIdAsync(entity);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity<Guid>
        {
            (this as IWorkScope).GetRepo<TEntity, Guid>().Delete(entity);
        }

        public void Delete<TEntity>(Guid id) where TEntity : class, IEntity<Guid>
        {
            (this as IWorkScope).GetRepo<TEntity, Guid>().Delete(id);
        }

        public async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class, IEntity<Guid>
        {
            await (this as IWorkScope).GetRepo<TEntity, Guid>().DeleteAsync(entity);
        }

        public async Task DeleteAsync<TEntity>(Guid id) where TEntity : class, IEntity<Guid>
        {
            await (this as IWorkScope).GetRepo<TEntity, Guid>().DeleteAsync(id);
        }

        public void SoftDelete<TEntity>(TEntity entity) where TEntity : class, IEntity<Guid>, ISoftDelete
        {
            entity.IsDeleted = true;
            (this as IWorkScope).GetRepo<TEntity, Guid>().Update(entity);
        }

        public async Task SoftDeleteAsync<TEntity>(TEntity entity) where TEntity : class, IEntity<Guid>, ISoftDelete
        {
            entity.IsDeleted = true;
            await (this as IWorkScope).GetRepo<TEntity, Guid>().UpdateAsync(entity);
        }

        public TEntity Get<TEntity>(Guid id) where TEntity : class, IEntity<Guid>
        {
            return (this as IWorkScope).GetRepo<TEntity, Guid>().Get(id);
        }

        public async Task<TEntity> GetAsync<TEntity>(Guid id) where TEntity : class, IEntity<Guid>
        {
            return await (this as IWorkScope).GetRepo<TEntity, Guid>().GetAsync(id);
        }

        IEnumerable<TEntity> IWorkScope.UpdateRange<TEntity>(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                yield return (this as IWorkScope).GetRepo<TEntity, Guid>().Update(entity);
            }
        }

        async Task<IEnumerable<TEntity>> IWorkScope.UpdateRangeAsync<TEntity>(IEnumerable<TEntity> entities)
        {
            var updatedEntities = new List<TEntity>();
            foreach (var entity in entities)
            {
                updatedEntities.Add(await (this as IWorkScope).GetRepo<TEntity, Guid>().UpdateAsync(entity));
            }

            return updatedEntities;
        }

        Task<IEnumerable<TEntityDto>> IWorkScope.Sync<TEntityDto, TEntity>(IEnumerable<TEntityDto> input, Expression<Func<TEntity, bool>> condition, Func<TEntityDto, TEntityDto> merge)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<TEntityDto>> IWorkScope.Sync<TEntityDto, TEntity>(IEnumerable<TEntityDto> input)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<TEntityDto>> IWorkScope.Sync<TEntityDto, TEntity>(IEnumerable<TEntityDto> input, Expression<Func<TEntity, bool>> condition)
        {
            throw new NotImplementedException();
        }
    }
}
