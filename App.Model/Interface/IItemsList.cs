
using System.Collections.Generic;

namespace App.Model.Interface
{

    public interface IItemsList<TIEntity> where TIEntity : class
    {
        int TotalItemsCount { get; set; }
        int RenderedItemsCount { get; set; }
        IEnumerable<TIEntity> Items { get; set; }
    }

}