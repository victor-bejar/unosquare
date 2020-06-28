using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Api.Installer
{

    public static class InstallerExtensions
    {
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            
            List<IInstaller> installers =
                typeof(IInstaller).
                    Assembly.
                    ExportedTypes.
                        Where
                        (
                            x =>
                                typeof(IInstaller).IsAssignableFrom(x) &&
                                !x.IsInterface &&
                                !x.IsAbstract
                        ).
                        Select(Activator.CreateInstance).
                        Cast<IInstaller>().
                        ToList();

            installers.ForEach(x => x.InstallServices(services, configuration));

        }

    }

}

