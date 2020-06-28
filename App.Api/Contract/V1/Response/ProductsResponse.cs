using System.Collections.Generic;
using App.Model.Interface;


namespace App.Api.Contract.V1.Response
{

    public class ProductsResponse : IItemsList<IProduct>
    {
        public int TotalItemsCount { get; set; }
        public int RenderedItemsCount { get; set; }
        public IEnumerable<IProduct> Items { get; set; }
    }

}