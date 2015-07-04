using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;

namespace AspNet.Identity3.MongoDB
{
    public class MongoIdentityServices
    {
        public static IServiceCollection GetDefaultServices(
            Type userType, Type roleType, Type contextType, IConfiguration config = null)
        {
            Type userStoreType = typeof(UserStore<,,>).MakeGenericType(userType, roleType, contextType);
            Type roleStoreType = typeof(RoleStore<,,>).MakeGenericType(userType, roleType, contextType);

            var services = new ServiceCollection();
            services.AddScoped(
                typeof(IUserStore<>).MakeGenericType(userType),
                userStoreType);
            services.AddScoped(
                typeof(IRoleStore<>).MakeGenericType(roleType),
                roleStoreType);
            return services;
        }
    }
}