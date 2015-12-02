using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System.Collections;

namespace AspNet.Identity3.MongoDB
{
    public class MongoIdentityContext<TUser, TRole>
        where TUser : IdentityUser
        where TRole : IdentityRole
    {
		public IMongoCollection<TUser> Users { get; set; }
		public IMongoCollection<TRole> Roles { get; set; }

		public MongoIdentityContext()
		{
            RegisterConventionToNotSerializeEmptyLists();
            RegisterMappings();
		}

        protected void RegisterMappings()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(IdentityUser)))
            {
                BsonClassMap.RegisterClassMap<IdentityUser>(cm =>
                {
                    cm.AutoMap();
                });
            }
        }

        private static void RegisterConventionToNotSerializeEmptyLists()
        {
            var pack = new ConventionPack();
            pack.AddMemberMapConvention("Do not serialize empty lists", m =>
            {
                if (typeof(ICollection).IsAssignableFrom(m.MemberType))
                {
                    m.SetShouldSerializeMethod(instance =>
                    {
                        var value = (ICollection)m.Getter(instance);
                        return value != null && value.Count > 0;
                    });
                }
            });
            ConventionRegistry.Register("Do not serialize empty lists", pack, t => true);
        }
    }
}