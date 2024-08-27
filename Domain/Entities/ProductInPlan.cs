using Domain.CommonBase;

namespace Domain.Entities
{
    public class ProductInPlan : BaseTimeInfoEntity
    {
        public int Id { get; set; }
        public double Quantity { get; set; }

        public int ProductionPlanId { get; set; }
        public ProductionPlan? ProductionPlan { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}