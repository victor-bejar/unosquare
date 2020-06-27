using System.Collections.Generic;

using App.Persistence.Model;


namespace App.Persistence.Interface
{

    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetProducts(string filter, int pageIndex, int pageSize);
    }
    
}
