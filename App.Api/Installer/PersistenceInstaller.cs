using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using App.Persistence.Class;
using App.Api.Contract.V1.Interface;
using App.Api.Contract.V1.Request;
using App.Api.Contract.V1.Service;
using App.Persistence.Interface;
using App.Persistence.Model;

namespace App.Api.Installer
{

    public class PersistenceInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UnoSquareContext>(options => options.UseInMemoryDatabase("UnoSquareDb"));
            services.AddScoped<IEntityService<Product, ProductCreateRequest, ProductUpdateRequest>, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
    
}
