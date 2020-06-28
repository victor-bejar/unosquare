using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace App.Api.Installer
{

    public class SwaggerInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {

            services.AddSwaggerGen
            (
                x =>
                    x.SwaggerDoc
                    (
                        "v1",
                        new OpenApiInfo()
                        {
                            Title = "UnoSquare API",
                            Version = "v1"
                        }
                    )
            );

        }

    }

}

