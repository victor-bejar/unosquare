using System.Collections.Generic;
using System.Linq;

using Xunit;

using App.Persistence.Class;
using App.Persistence.Model;
using App.Model.Interface;

namespace App.Persistence.Test.Api
{

    public class ProductRepositoryTest
    {

        private Product CreateDefaultModel()
        {

            const string productName = "Pickle Rick";
            const string productDescription = "Morty as You seen on your PC";
            const int productAgeRestriction = 18;
            const string productCompany = "Company";
            const decimal productPrice = 10.99M;

            Product product =
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

        [Fact]
        public void CreateUpdatesSavedEntityId()
        {

            List<Product> models = new List<Product>();

            for (int i = 1; i <= 5; i++)
            {

                Product model = this.CreateDefaultModel();

                model.Name = model.Name + $" { i.ToString() }";
                model.Description = model.Description + $" { i.ToString() }";
                model.AgeRestriction = model.AgeRestriction + i;
                model.Company = model.Company + $" { i.ToString() }";
                model.Price = model.Price + i;

                models.Add(model);

            }

            using (UnitOfWork uow = new UnitOfWork(UnoSquareContextFactory.Create(nameof(CreateUpdatesSavedEntityId))))
            {

                int previousId = 0;

                models.ForEach
                (
                    x =>
                    {
                        uow.Products.Add(x);
                        uow.Complete();
                        Assert.True(x.ProductId > previousId);
                        previousId = x.ProductId;
                    }
                );

            }

        }

        [Theory]
        [InlineData(10, "", 0, 20, 10, 10)]
        [InlineData(10, "", 0, 5, 10, 5)]
        [InlineData(10, "", 1, 5, 10, 5)]
        [InlineData(10, "", 2, 5, 10, 0)]
        [InlineData(12, "", 2, 5, 12, 2)]
        [InlineData(20, "", 0, 5, 20, 5)]
        [InlineData(20, "", 0, 10, 20, 10)]
        [InlineData(20, "", 2, 10, 20, 0)]
        [InlineData(30, "", 2, 10, 30, 10)]
        [InlineData(25, "Pickle Rick", 0, 10, 25, 10)]
        [InlineData(25, "Pickle Rick 2", 0, 20, 7, 7)]
        [InlineData(25, "Pickle Rick 10", 0, 10, 1, 1)]
        [InlineData(25, "Pickle Rick 200", 0, 10, 0, 0)]
        [InlineData(25, "Morty as You seen on your PC", 0, 10, 25, 10)]
        [InlineData(25, "Morty as You seen on your PC 2", 0, 20, 7, 7)]
        [InlineData(25, "Morty as You seen on your PC 10", 0, 10, 1, 1)]
        [InlineData(25, "Morty as You seen on your PC 200", 0, 10, 0, 0)]
        [InlineData(25, "Company", 0, 10, 25, 10)]
        [InlineData(25, "Company 2", 0, 20, 7, 7)]
        [InlineData(25, "Company 10", 0, 10, 1, 1)]
        [InlineData(25, "Company 200", 0, 10, 0, 0)]
        public void GetProducts(int savedItems, string filter, int pageIndex, int pageSize, int totalItems, int renderedItems)
        {

            List<Product> modelsToSave = new List<Product>();
            IItemsList<Product> productsList = null;

            for (int i = 1; i <= savedItems; i++)
            {

                Product model = this.CreateDefaultModel();

                model.Name = model.Name + $" { i.ToString() }";
                model.Description = model.Description + $" { i.ToString() }";
                model.AgeRestriction = model.AgeRestriction + i;
                model.Company = model.Company + $" { i.ToString() }";
                model.Price = model.Price + i;

                modelsToSave.Add(model);

            }

            using (UnitOfWork uow = new UnitOfWork(UnoSquareContextFactory.Create(nameof(CreateUpdatesSavedEntityId))))
            {

                modelsToSave.ForEach(x => { uow.Products.Add(x); uow.Complete(); });
                productsList = uow.Products.GetProducts(filter, pageIndex, pageSize);

                Assert.Equal(totalItems, productsList.TotalItemsCount);
                Assert.Equal(renderedItems, productsList.RenderedItemsCount);
                Assert.Equal(renderedItems, productsList.Items.Count());

            }

        }

    }

}
