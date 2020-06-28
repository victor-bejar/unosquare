using System.Collections.Generic;

using App.Model.Interface;
using App.Api.Contract.V1.Request;
using App.Persistence.Interface;
using App.Api.Contract.V1.Interface;
using App.Persistence.Model;

namespace App.Api.Contract.V1.Service
{

    public class ProductService : IEntityService<Product, ProductCreateRequest, ProductUpdateRequest>
    {

        private readonly IUnitOfWork _persistence = null;

        public ProductService(IUnitOfWork persistence)
        {
            this._persistence = persistence;
        }

        public Product Create(ProductCreateRequest productRequest)
        {
            Product productModel = Map.ProductMapper.CreateRequestToModel(productRequest);
            this._persistence.Products.Add(productModel);
            this._persistence.Complete();
            return productModel;
        }

        public Product Get(int productId)
        {
            Product product = this._persistence.Products.Get(productId);
            return product;
        }

        public IEnumerable<Product> GetList(string filter, int? pageIndex, int? pageSize)
        {

            int defaultPageIndex = 0;
            int defaultPageSize = 20;

            int currentPageIndex = pageIndex != null ? (int)pageIndex : defaultPageIndex;
            int currentPageSize = pageSize != null ? (int)pageSize : defaultPageSize;

            IEnumerable<Product> models =
                this._persistence.Products.GetProducts(filter, currentPageIndex, currentPageSize);

            return models;

        }

        public Product Update(ProductUpdateRequest modelUpdateRequest, int id)
        {
            Product model = this._persistence.Products.Get(id);
            model = Map.ProductMapper.UpdateRequestToModel(modelUpdateRequest, id);
            this._persistence.Complete();
            return model;
        }

        public Product Remove(int productId)
        {
            Product productModel = this._persistence.Products.Get(productId);
            this._persistence.Products.Remove(productModel);
            return productModel;
        }

    }

}