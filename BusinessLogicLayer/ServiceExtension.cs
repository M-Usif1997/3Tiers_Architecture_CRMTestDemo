using BusinessLogicLayer.Contract.IFeatures.ICommon;
using BusinessLogicLayer.Contract.IFeatures.IEmployee;
using BusinessLogicLayer.Features_Imp.Common;
using BusinessLogicLayer.Features_Imp.Employee;
using DataAccessLayer.Repositories.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BusinessLogicLayer
{
    public static class ServiceExtension
    {
        public static void ConfigureBusinessLogicServices(this IServiceCollection services, IConfiguration configuration)
        {
            Assembly.GetExecutingAssembly();
            services.AddScoped(typeof(IBaseService<,,,>), typeof(BaseService<,,,>));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

         
            services.AddScoped<IEmployeeService, EmployeeService>();
           



        }
    }
}
