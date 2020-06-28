using System.Threading.Tasks;

using Moq;
using Xunit;

using App.Model.Interface;
using App.Persistence.Interface;
using App.Api.Contract.V1.Request;
using App.Api.Contract.V1.Service;
using App.Api.Contract.V1.Interface;
using App.Persistence.Model;

namespace App.Api.Test.Contract.V1.Service
{

    public class ProductServiceTest
    {

        private readonly Mock<IUnitOfWork> _mockUoW = new Mock<IUnitOfWork>();

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
        public void CreateCallsPersistenceAsync()
        {

            ProductCreateRequest productRequest = this.GetProductCreateRequest();
            ProductService productService = new ProductService(this._mockUoW.Object);

            this._mockUoW.Setup(x => x.Products.Add(It.IsAny<Product>()));

            productService.Create(productRequest);

            this._mockUoW.Verify(x => x.Products.Add(It.IsAny<Product>()), Times.Once);
            this._mockUoW.Verify(x => x.Complete(), Times.Once);

        }

        [Fact]
        public void GetCallsPersistenceAsync()
        {

            int productId = 1;

            ProductService productService = new ProductService(this._mockUoW.Object);

            this._mockUoW.Setup(x => x.Products.Get(It.IsAny<int>()));

            productService.Get(productId);

            this._mockUoW.Verify(x => x.Products.Get(productId), Times.Once);

        }

        [Fact]
        public void GetListCallsPersistenceAsync()
        {

            string filter = "filter";
            int pageIndex = 0;
            int pageSize = 10;

            ProductService productService = new ProductService(this._mockUoW.Object);

            this._mockUoW.Setup(x => x.Products.GetProducts(filter, pageIndex, pageSize));

            productService.GetList(filter, pageIndex, pageSize);

            this._mockUoW.Verify(x => x.Products.GetProducts(filter, pageIndex, pageSize), Times.Once);

        }

        [Fact]
        public void UpdateCallsPersistence()
        {

            int id = 1;

            Product product = new Product() { ProductId = id };
            ProductUpdateRequest productRequest = this.GetProductUpdateRequest();
            ProductService productService = new ProductService(this._mockUoW.Object);

            this._mockUoW.Setup(x => x.Products.Get(id)).Returns(product);
            this._mockUoW.Setup(x => x.Products.Add(It.IsAny<Product>()));

            productService.Update(productRequest, id);

            this._mockUoW.Verify(x => x.Complete(), Times.Once);

        }

        [Fact]
        public void DeleteCallsPersistence()
        {

            int productId = 1;

            ProductService productService = new ProductService(this._mockUoW.Object);

            this._mockUoW.Setup(x => x.Products.Remove(It.IsAny<Product>()));

            productService.Remove(productId);

            this._mockUoW.Verify(x => x.Products.Remove(It.IsAny<Product>()), Times.Once);

        }

    }

}
