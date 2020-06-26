using System;

using Xunit;

using App.Model.Class;

namespace App.Model.Test.Class
{

    public class ProductTest
    {

        [Fact]
        public void StoresProductId()
        {
            int productId = (new Random()).Next(1, int.MaxValue);
            Product product = new Product() { ProductId = productId };
            Assert.Equal(productId, product.ProductId);
        }

        [Fact]
        public void NameInitializatedAsEmpty()
        {
            string emptyName = string.Empty;
            Product product = new Product();
            Assert.Equal(emptyName, product.Name);
        }

        [Theory]
        [InlineData("Rick & Morty")]
        [InlineData("Capitán")]
        [InlineData("Pandy")]
        [InlineData("1")]
        [InlineData("12345678901234567890123456789012345678901234567890")]
        public void StoresValidName(string name)
        {
            Product product = new Product() { Name = name };
            Assert.Equal(name, product.Name);
        }

        [Fact]
        public void EmptyNameTrhowsArgumentException()
        {
            string name = string.Empty;
            Action setEmptyName = new Action(() => new Product() { Name = name });
            Assert.Throws<ArgumentException>(setEmptyName);
        }

        [Fact]
        public void DescriptionInitializatedAsEmpty()
        {
            string emptyDescription = string.Empty;
            Product product = new Product();
            Assert.Equal(emptyDescription, product.Description);
        }

        [Theory]
        [InlineData("Rick & Morty")]
        [InlineData("Capitán")]
        [InlineData("Pandy")]
        [InlineData("")]
        [InlineData("1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
        public void StoresValidDescription(string description)
        {
            Product product = new Product() { Description = description };
            Assert.Equal(description, product.Description);
        }

        [Fact]
        public void AgeRestrictionInitializatedAs100()
        {
            int ageRestriction = 100;
            Product product = new Product();
            Assert.Equal(ageRestriction, product.AgeRestriction);
        }

        [Fact]
        public void AgeRestrictionStoresNull()
        {
            int? ageRestriction = null;
            Product product = new Product() { AgeRestriction = ageRestriction };
            Assert.Equal(ageRestriction, product.AgeRestriction);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(25)]
        [InlineData(50)]
        [InlineData(75)]
        [InlineData(100)]
        public void StoresValidAgeRestriction(int ageRestriction)
        {
            Product product = new Product() { AgeRestriction = ageRestriction };
            Assert.Equal(ageRestriction, product.AgeRestriction);
        }

        [Theory]
        [InlineData(-100)]
        [InlineData(-1)]
        [InlineData(101)]
        [InlineData(1000)]
        public void InvalidRestrictionTrhowsArgumentException(int ageRestriction)
        {
            Action setInvalidAgeRestriction = new Action(() => new Product() { AgeRestriction = ageRestriction });
            Assert.Throws<ArgumentException>(setInvalidAgeRestriction);
        }

        [Fact]
        public void CompanyInitializatedAsEmpty()
        {
            string emptyCompany = string.Empty;
            Product product = new Product();
            Assert.Equal(emptyCompany, product.Company);
        }

        [Theory]
        [InlineData("Rick & Morty")]
        [InlineData("Capitán")]
        [InlineData("Pandy")]
        [InlineData("12345678901234567890123456789012345678901234567890")]
        public void StoresValidCompany(string company)
        {
            Product product = new Product() { Company = company };
            Assert.Equal(company, product.Company);
        }

        [Fact]
        public void EmptyCompanyTrhowsArgumentException()
        {
            string company = string.Empty;
            Action setEmptyCompany = new Action(() => new Product() { Company = company });
            Assert.Throws<ArgumentException>(setEmptyCompany);
        }

        [Theory]
        [InlineData("123456789012345678901234567890123456789012345678901")]
        [InlineData("1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
        public void InvalidCompanyLengthTrhowsArgumentException(string company)
        {
            Action setInvalidCompanyLength = new Action(() => new Product() { Company = company });
            Assert.Throws<ArgumentException>(setInvalidCompanyLength);
        }

    }

}