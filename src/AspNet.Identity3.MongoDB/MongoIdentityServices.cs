using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;

namespace AspNet.Identity3.MongoDB
{
    public class MongoIdentityServices
    {
        public static IEnumerable<ServiceDescriptor> GetDefaultServices(
            Type userType, Type roleType, Type contextType, IConfiguration config = null)
        {
            Type userStoreType = typeof(UserStore<,,>).MakeGenericType(userType, roleType, contextType);
            Type roleStoreType = typeof(RoleStore<,,>).MakeGenericType(userType, roleType, contextType);


            yield return ServiceDescriptor.Scoped(
                typeof(IUserStore<>).MakeGenericType(userType),
                userStoreType);

            yield return ServiceDescriptor.Scoped(
                typeof(IRoleStore<>).MakeGenericType(roleType),
                roleStoreType);
        }
    }
}