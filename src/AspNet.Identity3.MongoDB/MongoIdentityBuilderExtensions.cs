using AspNet.Identity3.MongoDB;
using Microsoft.AspNet.Identity;
using Microsoft.Framework.DependencyInjection.Extensions;

namespace Microsoft.Framework.DependencyInjection
{
    public static class MongoIdentityBuilderExtensions
    {
        public static IdentityBuilder AddMongoStores<TContext, TUser, TRole>(this IdentityBuilder builder)
            where TUser : IdentityUser
            where TRole : IdentityRole
            where TContext : MongoIdentityContext<TUser, TRole>
        {
            builder.Services.Add(MongoIdentityServices.GetDefaultServices(builder.UserType, builder.RoleType, typeof(TContext)));
            return builder;
        }
    }
}