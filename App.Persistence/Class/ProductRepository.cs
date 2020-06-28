using System.Collections.Generic;
using System.Linq;


using App.Model.Interface;
using App.Persistence.Interface;
using App.Persistence.Model;


namespace App.Persistence.Class
{

    public class ProductRepository : Repository<Product>, IProductRepository
    {

        public ProductRepository(UnoSquareContext context) : base(context)
        {
        }

        public IItemsList<Product> GetProducts(string filter, int pageIndex, int pageSize)
        {

            IItemsList<Product> productsList = null;
            IEnumerable<Product> products = null;
            IQueryable<Product> productQuery = null;

            productQuery = ((UnoSquareContext)this._context).Products;

            if (!string.IsNullOrWhiteSpace(filter))
            {

                string appliedFilter = filter.ToLower();

                productQuery =
                    productQuery.Where
                    (
                        x =>
                            x.Name.ToLower().Contains(appliedFilter) ||
                            x.Description.ToLower().Contains(appliedFilter) ||
                            x.Company.ToLower().Contains(appliedFilter)
                    );

            }

            productsList = new App.Model.Class.ItemsList<Product>();
            productsList.TotalItemsCount = productQuery.Count();

            products = productQuery.
                    Skip(pageIndex * pageSize).
                    Take(pageSize).
                    ToList();

            productsList.RenderedItemsCount = products.Count();
            productsList.Items = products;

            return productsList;

        }

    }

}