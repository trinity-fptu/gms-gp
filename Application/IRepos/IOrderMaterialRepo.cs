using Domain.Entities;

namespace Application.IRepos
{
    public interface IOrderMaterialRepo : IGenericRepo<OrderMaterial>
    {
        Task<List<OrderMaterial>> GetOrderMaterialByPOId(int purchasingOrderId);
    }
}
