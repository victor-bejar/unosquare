using App.Persistence.Interface;

namespace App.Persistence.Class
{

    public class UnitOfWork : IUnitOfWork
    {

        private readonly UnoSquareContext _context;

        public IProductRepository Products { get; private set; }

        public UnitOfWork(UnoSquareContext context)
        {
            this._context = context;
            this.Products = new ProductRepository(this._context);
        }

        public int Complete()
        {
            return this._context.SaveChanges();
        }

        public void Dispose()
        {
            this._context.Dispose();
        }
        
    }

}