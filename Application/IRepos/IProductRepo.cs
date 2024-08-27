using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepos
{
    public interface IProductRepo : IGenericRepo<Product>
    {
        public Task<Product> GetByCodeAsync(string code);
        Task<Product> GetByIdWithDetailAsync(int id);
        Task<List<Product>> GetAllWithDetailAsync();
    }
}
