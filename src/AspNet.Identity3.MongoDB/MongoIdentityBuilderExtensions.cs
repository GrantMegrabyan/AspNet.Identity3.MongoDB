using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AspNet.Identity3.MongoDB
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