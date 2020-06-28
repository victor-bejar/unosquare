using System.Collections.ObjectModel;
using System.Linq;
using App.Api.Contract.V1.Request;
using App.Api.Contract.V1.Response;
using App.Model.Interface;
using App.Persistence.Model;

namespace App.Api.Contract.V1.Map
{

    public static class ProductMapper
    {

        public static Product CreateRequestToModel(ProductCreateRequest modelRequest)
        {

            Product model =
                new Product()
                {
                    Name = modelRequest.Name,
                    Description = modelRequest.Description,
                    AgeRestriction = modelRequest.AgeRestriction,
                    Company = modelRequest.Company,
                    Price = modelRequest.Price
                };

            return model;

        }

        public static Product UpdateRequestToModel(ProductUpdateRequest modelRequest, Product model)
        {

            model.Name = modelRequest.Name;
            model.Description = modelRequest.Description;
            model.AgeRestriction = modelRequest.AgeRestriction;
            model.Company = modelRequest.Company;
            model.Price = modelRequest.Price;

            return model;

        }

        public static ProductResponse ModelToResponse(IProduct model)
        {

            ProductResponse modelResponse =
                new ProductResponse()
                {
                    ProductId = model.ProductId,
                    Name = model.Name,
                    Description = model.Description,
                    AgeRestriction = model.AgeRestriction,
                    Company = model.Company,
                    Price = model.Price
                };

            return modelResponse;

        }

        public static ProductsResponse ProductsListToResponse(IItemsList<Product> model)
        {

            ProductsResponse modelResponse =
                new ProductsResponse()
                {
                    TotalItemsCount = model.TotalItemsCount,
                    RenderedItemsCount = model.RenderedItemsCount,
                    Items = model?.Items.Select(x => ProductMapper.ModelToResponse(x)).ToList()
                };

            return modelResponse;

        }

    }

}