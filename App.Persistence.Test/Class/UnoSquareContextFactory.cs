using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using App.Persistence.Class;

namespace App.Persistence.Test.Api
{

    public class UnoSquareContextFactory
    {

        public static UnoSquareContext Create(string databaseName)
        {

            ServiceProvider serviceProvider =
                new ServiceCollection().
                    AddEntityFrameworkInMemoryDatabase().
                    BuildServiceProvider();

            DbContextOptions<UnoSquareContext> options =
                new DbContextOptionsBuilder<UnoSquareContext>().
                UseInMemoryDatabase(databaseName).
                UseInternalServiceProvider(serviceProvider).
                Options;

            UnoSquareContext context = new UnoSquareContext(options);

            return context;

        }

    }

}