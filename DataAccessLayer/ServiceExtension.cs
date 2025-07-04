
using DataAccessLayer.Repositories.Contract.ICommon;
using DataAccessLayer.Repositories.Implementation;
using DataAccessLayer.Repositories.Implementation.Common;
using DataAccessLayer.Services.CRM.Contract;
using DataAccessLayer.Services.CRM.Implementation;
using DataAccessLayer.Services.TokenProvider.Contract;
using DataAccessLayer.Services.TokenProvider.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace DataAccessLayer
{
    public static class ServiceExtension
    {
        public static void ConfigureDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnectionCRM");

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ITokenProvider, TokenProvider>();
            services.AddScoped(typeof(ICrmResponseParser<>), typeof(CrmResponseParser<>));
            services.AddHttpClient();
            services.Configure<CRMConfiguration>(configuration.GetSection("Data"));


            services.AddScoped<ICrmHttpClient, CrmHttpClient>();
            services.AddScoped<ICrmServiceContextProvider>(sp => new CrmServiceContextProvider(connection));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }

    }
}



