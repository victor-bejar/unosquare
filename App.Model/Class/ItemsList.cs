using System.Collections.Generic;

using App.Model.Interface;

namespace App.Model.Class
{

    public class ItemsList<TIEntity> : IItemsList<TIEntity> where TIEntity : class
    {
        public int TotalItemsCount { get; set; }
        public int RenderedItemsCount { get; set; }
        public IEnumerable<TIEntity> Items { get; set; }
    }

}
