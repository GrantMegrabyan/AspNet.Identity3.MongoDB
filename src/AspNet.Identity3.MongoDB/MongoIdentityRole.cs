using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AspNet.Identity3.MongoDB
{
    public class MongoIdentityRole : IdentityRole
    {
        public MongoIdentityRole()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        [BsonRepresentation(BsonType.ObjectId)]
        public override string Id { get; set; }

        [BsonIgnoreIfNull]
        public virtual List<MongoIdentityClaim> MyClaims { get; private set; } = new List<MongoIdentityClaim>();

        public virtual void AddClaim(Claim claim)
        {
            MyClaims.Add(new MongoIdentityClaim(claim));
        }

        public virtual void RemoveClaim(Claim claim)
        {
            var claimsToRemove = MyClaims
                .Where(c => c.Type == claim.Type)
                .Where(c => c.Value == claim.Value);

            MyClaims = MyClaims.Except(claimsToRemove).ToList();
        }

        public virtual void ReplaceClaim(Claim claim, Claim newClaim)
        {
            foreach (var userClaim in MyClaims)
            {
                if (userClaim.Type == claim.Type &&
                    userClaim.Value == claim.Value)
                {
                    userClaim.Type = newClaim.Type;
                    userClaim.Value = newClaim.Value;
                }
            }
        }
    }
}