﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNet.Identity3.MongoDB
{
    public class MongoIdentityServices
    {
        public static IEnumerable<ServiceDescriptor> GetDefaultServices(
            Type userType, Type roleType, Type contextType, IConfiguration config = null)
        {
            var userStoreType = typeof(UserStore<,,>).MakeGenericType(userType, roleType, contextType);
            var roleStoreType = typeof(RoleStore<,,>).MakeGenericType(userType, roleType, contextType);

            yield return ServiceDescriptor.Scoped(
                typeof(IUserStore<>).MakeGenericType(userType),
                userStoreType);

            yield return ServiceDescriptor.Scoped(
                typeof(IRoleStore<>).MakeGenericType(roleType),
                roleStoreType);
        }
    }
}