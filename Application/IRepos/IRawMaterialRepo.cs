
using Domain.Entities;

namespace Application.IRepos
{
    public interface IRawMaterialRepo : IGenericRepo<RawMaterial>
    {
        Task<List<RawMaterial>> GetRawMaterialByMaterialCategoryId(int materialCategoryId);
        Task<RawMaterial> GetByMaterialCode(string code);
    }
}
