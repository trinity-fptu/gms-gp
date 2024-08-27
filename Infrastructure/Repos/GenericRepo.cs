using Application.IRepos;
using Application.Utils;
using Domain.CommonBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos
{
    public class GenericRepo<TModel> : IGenericRepo<TModel> where TModel : BaseTimeInfoEntity
    {
        protected DbSet<TModel> _dbSet;

        public GenericRepo(AppDbContext context)
        {
            _dbSet = context.Set<TModel>();
        }

        public async Task<List<TModel>> GetAllAsync()
        {

            return await _dbSet.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<Pagination<TModel>> ToPaginationAsync(int pageIndex = 0, int pageSize = 10)
        {
            IQueryable<TModel> query = _dbSet;

            // perform pagination
            var itemCount = await _dbSet.CountAsync();
            var items = await _dbSet
                    .Where(x => x.IsDeleted == false)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .AsNoTracking()
                    .ToListAsync();

            var result = new Pagination<TModel>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = itemCount,
                Items = items,
            };

            return result;
        }

        public async Task<TModel?> GetByIdAsync(int key)
        {
            var item = await _dbSet.FindAsync(key);
            if (item != null && item.IsDeleted == false)
            {
                return item;
            }
            return null;
        }

        public virtual async Task AddAsync(TModel entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IList<TModel> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void Update(TModel entity)
        {
            _dbSet.Update(entity);
        }

        public void UpdateRange(IList<TModel> entities)
        {
            _dbSet.UpdateRange(entities);
        }


        public virtual void SoftRemove(TModel entityToDelete)
        {
            entityToDelete.IsDeleted = true;
        }

        public void SoftRemoveRange(IList<TModel> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
            }
        }
    }
}
