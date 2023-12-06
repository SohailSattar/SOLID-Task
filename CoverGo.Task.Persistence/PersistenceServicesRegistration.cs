using CoverGo.Task.Application.Contracts.Persistence;
using CoverGo.Task.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CoverGo.Task.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services)
        {
            // Configure in-memory database
            services.AddDbContext<CoverGoDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: "InMemoryDb"));


            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductAmountRepository, ProductAmountRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

            //services.AddScoped<ICompaniesQuery, InMemoryCompaniesRepository>();
            //services.AddScoped<IPlansQuery, InMemoryPlansRepository>();
            //services.AddScoped<ICompaniesWriteRepository, InMemoryCompaniesRepository>();
            //services.AddScoped<IPlansWriteRepository, InMemoryPlansRepository>();
            return services;
        }
    }
}
