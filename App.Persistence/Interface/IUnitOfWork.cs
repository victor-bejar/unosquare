using System;
using System.Collections.Generic;

namespace App.Persistence.Interface
{
    
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        int Complete();
   }

}