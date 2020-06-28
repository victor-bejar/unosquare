using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

//TODO: Review Policies;
// https://stackoverflow.com/questions/54085677/how-to-configure-angular-6-with-net-core-2-to-allow-cors-from-any-host
namespace App.Api.Installer
{

    public class CorsInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {

            string[] localHostOrigins = new string[] { "http://localhost:4200" };

            // CORS middleware must precede any defined endpoints
            services.AddCors
            (
                options =>
                {
                    options.AddPolicy
                    (
                        "DevelopmentCorsPolicy",
                        builder =>
                        {
                            builder.WithOrigins(localHostOrigins).AllowAnyHeader().AllowAnyMethod();
                        }
                    );
                }
            );

        }

    }

}