using Xunit;

using App.Model.Interface;
using App.Api.Contract.V1.Request;
using App.Api.Contract.V1.Response;
using App.Api.Contract.V1.Map;
using App.Persistence.Model;

namespace App.Api.Test.Contract.V1.Map
{
    public class ProductMapperTest
    {

        private IProduct GetDefaultModel()
        {

            const string productName = "Pickle Rick";
            const string productDescription = "Morty as You seen on your PC";
            const int productAgeRestriction = 18;
            const string productCompany = "Company";
            const decimal productPrice = 10.99M;

            IProduct product =
                new Product()
                {
                    Name = productName,
                    Description = productDescription,
                    AgeRestriction = productAgeRestriction,
                    Company = productCompany,
                    Price = productPrice
                };

            return product;

        }

        private ProductCreateRequest GetProductCreateRequest()
        {

            IProduct model = this.GetDefaultModel();

            ProductCreateRequest product =
                new ProductCreateRequest()
                {
                    Name = model.Name,
                    Description = model.Description,
                    AgeRestriction = model.AgeRestriction,
                    Company = model.Company,
                    Price = model.Price
                };

            return product;

        }

        private ProductUpdateRequest GetProductUpdateRequest()
        {

            IProduct model = this.GetDefaultModel();

            ProductUpdateRequest product =
                new ProductUpdateRequest()
                {
                    Name = model.Name,
                    Description = model.Description,
                    AgeRestriction = model.AgeRestriction,
                    Company = model.Company,
                    Price = model.Price
                };

            return product;

        }

        [Fact]
        public void CreateRequestToModelMapValues()
        {

            ProductCreateRequest productRequest = this.GetProductCreateRequest();
            IProduct productModel = ProductMapper.CreateRequestToModel(productRequest);

            Assert.Equal(productRequest.Name, productModel.Name);
            Assert.Equal(productRequest.Description, productModel.Description);
            Assert.Equal(productRequest.AgeRestriction, productModel.AgeRestriction);
            Assert.Equal(productRequest.Company, productModel.Company);
            Assert.Equal(productRequest.Price, productModel.Price);

        }

        [Fact]
        public void UpdateRequestToModelMapValues()
        {

            int id = 1;

            ProductUpdateRequest productRequest = this.GetProductUpdateRequest();
            Product productModel = new Product() { ProductId = id };
            
            productModel = ProductMapper.UpdateRequestToModel(productRequest, productModel);

            Assert.Equal(id, productModel.ProductId);
            Assert.Equal(productRequest.Name, productModel.Name);
            Assert.Equal(productRequest.Description, productModel.Description);
            Assert.Equal(productRequest.AgeRestriction, productModel.AgeRestriction);
            Assert.Equal(productRequest.Company, productModel.Company);
            Assert.Equal(productRequest.Price, productModel.Price);

        }

        [Fact]
        public void ModelToResponseMapValues()
        {

            IProduct model = this.GetDefaultModel();
            ProductResponse productResponse = ProductMapper.ModelToResponse(model);

            Assert.Equal(model.ProductId, productResponse.ProductId);
            Assert.Equal(model.Name, productResponse.Name);
            Assert.Equal(model.Description, productResponse.Description);
            Assert.Equal(model.AgeRestriction, productResponse.AgeRestriction);
            Assert.Equal(model.Company, productResponse.Company);
            Assert.Equal(model.Price, productResponse.Price);

        }

    }

}