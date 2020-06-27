using System.Collections.Generic;
using System.Linq;

using Xunit;

using App.Persistence.Class;
using App.Persistence.Model;


namespace App.Persistence.Test.Api
{

    public class UnitOfWorkTest
    {

        private Product CreateDefaultModel()
        {

            const string productName = "Picke Rick";
            const string productDescription = "Rick & Morty's Pickle Rick";
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
        public void CreateSavesToDatabase()
        {

            Product defaultModel = this.CreateDefaultModel();
            Product modelToSave = this.CreateDefaultModel();
            Product savedModel = null;

            using (UnitOfWork uow = new UnitOfWork(UnoSquareContextFactory.Create(nameof(CreateSavesToDatabase))))
            {


                uow.Products.Add(modelToSave);
                uow.Complete();

                // get entity from db
                savedModel = uow.Products.Get(modelToSave.ProductId);
                Assert.NotNull(savedModel);

                Assert.Equal(defaultModel.Name, savedModel.Name);
                Assert.Equal(defaultModel.Description, savedModel.Description);
                Assert.Equal(defaultModel.AgeRestriction, savedModel.AgeRestriction);
                Assert.Equal(defaultModel.Company, savedModel.Company);
                Assert.Equal(defaultModel.Price, savedModel.Price);

            }

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

        [Fact]
        public void CreateSavesSingleRecordOnDatabase()
        {

            int expectedSavedModelsCount = 1;
            int savedModelsCount = 0;

            Product modelToSave = this.CreateDefaultModel();

            using (UnitOfWork uow = new UnitOfWork(UnoSquareContextFactory.Create(nameof(CreateSavesSingleRecordOnDatabase))))
            {

                uow.Products.Add(modelToSave);
                uow.Complete();

                savedModelsCount = uow.Products.GetAll().Count();
                Assert.Equal(expectedSavedModelsCount, savedModelsCount);

            }

        }

        [Fact]
        public void GetFetchsAnExistingEntity()
        {

            Product modelToSave = this.CreateDefaultModel();
            Product savedModel = null;

            using (UnitOfWork uow = new UnitOfWork(UnoSquareContextFactory.Create(nameof(GetFetchsAnExistingEntity))))
            {

                uow.Products.Add(modelToSave);
                uow.Complete();

                savedModel = uow.Products.Get(modelToSave.ProductId);
                Assert.NotNull(savedModel);

                Assert.Equal(modelToSave.Name, savedModel.Name);
                Assert.Equal(modelToSave.Description, savedModel.Description);
                Assert.Equal(modelToSave.AgeRestriction, savedModel.AgeRestriction);
                Assert.Equal(modelToSave.Company, savedModel.Company);
                Assert.Equal(modelToSave.Price, savedModel.Price);

            }

        }

        [Fact]
        public void GetAllFetchsAnExistingEntities()
        {

            int modelsToSave = 10;
            int savedModels = 0;

            List<Product> models = new List<Product>();

            for (int i = 1; i <= modelsToSave; i++)
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

                models.ForEach
                (
                    x =>
                    {
                        uow.Products.Add(x);
                        uow.Complete();
                    }
                );

                savedModels = uow.Products.GetAll().Count();
                Assert.Equal(modelsToSave, savedModels);

            }

        }

        [Fact]
        public void UpdatesSavesAllowedFields()
        {

            Product originalModel = this.CreateDefaultModel();
            Product savedModel = this.CreateDefaultModel();
            string updatedPostfix = "-Modified";
            int updatedIcrement = 1;

            using (UnitOfWork uow = new UnitOfWork(UnoSquareContextFactory.Create(nameof(UpdatesSavesAllowedFields))))
            {

                uow.Products.Add(savedModel);
                uow.Complete();

                savedModel.Name = originalModel.Name + updatedPostfix;
                savedModel.Description = originalModel.Description + updatedPostfix;
                savedModel.AgeRestriction = originalModel.AgeRestriction + updatedIcrement;
                savedModel.Company = originalModel.Company + updatedPostfix;
                savedModel.Price = originalModel.Price + updatedIcrement;
                uow.Complete();

                savedModel = uow.Products.Get(savedModel.ProductId);

                Assert.Equal(originalModel.Name + updatedPostfix, savedModel.Name);
                Assert.Equal(originalModel.Description + updatedPostfix, savedModel.Description);
                Assert.Equal(originalModel.AgeRestriction + updatedIcrement, savedModel.AgeRestriction);
                Assert.Equal(originalModel.Company + updatedPostfix, savedModel.Company);
                Assert.Equal(originalModel.Price + updatedIcrement, savedModel.Price);

            }

        }

        [Fact]
        public void RemoveDeletesEntityFromDatabase()
        {

            Product model = this.CreateDefaultModel();
            int beforeRemoveCount = 0;
            int afterDeleteCount = 0;

            using (UnitOfWork uow = new UnitOfWork(UnoSquareContextFactory.Create(nameof(RemoveDeletesEntityFromDatabase))))
            {

                uow.Products.Add(model);
                uow.Complete();

                beforeRemoveCount = uow.Products.GetAll().Count();

                uow.Products.Remove(model);
                uow.Complete();

                afterDeleteCount = uow.Products.GetAll().Count();

                Assert.Equal(beforeRemoveCount - 1, afterDeleteCount);

            }

        }

    }

}