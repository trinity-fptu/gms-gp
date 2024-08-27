using Domain.CommonBase;

namespace Domain.Entities
{
    public class ProductCategory : BaseTimeInfoEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}