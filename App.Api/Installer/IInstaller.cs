using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Api.Installer
{

    public interface IInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }

}

