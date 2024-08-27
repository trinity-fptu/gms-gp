using Application.Utils;

namespace Application.IRepos
{
    public interface IGenericRepo<TModel> where TModel : class
    {
        Task AddAsync(TModel entity);
        Task AddRangeAsync(IList<TModel> entities);
        Task<List<TModel>> GetAllAsync();
        Task<TModel?> GetByIdAsync(int key);
        void SoftRemove(TModel entityToDelete);
        void SoftRemoveRange(IList<TModel> entities);
        Task<Pagination<TModel>> ToPaginationAsync(int pageIndex = 0, int pageSize = 10);
        void Update(TModel entity);
        void UpdateRange(IList<TModel> entities);
    }
}