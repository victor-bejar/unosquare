using App.Model.Interface;


namespace App.Api.Contract.V1.Response
{

    public class ProductResponse : IProduct
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? AgeRestriction { get; set; }
        public string Company { get; set; }
        public decimal Price { get; set; }
   }

}