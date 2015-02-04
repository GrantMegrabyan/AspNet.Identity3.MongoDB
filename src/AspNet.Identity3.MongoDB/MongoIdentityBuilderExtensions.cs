using AspNet.Identity3.MongoDB;
using Microsoft.AspNet.Identity;

namespace Microsoft.Framework.DependencyInjection
{
    public static class MongoIdentityBuilderExtensions
    {
        public static IdentityBuilder AddMongoStores<TContext, TUser, TRole>(this IdentityBuilder builder)
            where TUser : MongoIdentityUser
            where TRole : MongoIdentityRole
            where TContext : MongoIdentityContext<TUser, TRole>
        {
            builder.Services.Add(MongoIdentityServices.GetDefaultServices(builder.UserType, builder.RoleType, typeof(TContext)));
            return builder;
        }
    }
}