using App.Model.Interface;
using App.Persistence.Model;

namespace App.Persistence.Interface
{

    public interface IProductRepository : IRepository<Product>
    {
        IItemsList<Product> GetProducts(string filter, int pageIndex, int pageSize);
    }
    
}
