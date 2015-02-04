using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Serialization;

namespace AspNet.Identity3.MongoDB
{
    public class MongoIdentityUser : IdentityUser
    {
        public MongoIdentityUser()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        public virtual List<IdentityUserLogin<string>> MyLogins { get; private set; } = new List<IdentityUserLogin<string>>();

        public virtual List<string> MyRoles { get; private set; } = new List<string>();

        public virtual List<MongoIdentityClaim> MyClaims { get; private set; } = new List<MongoIdentityClaim>();

        public virtual void AddClaim(Claim claim)
        {
            MyClaims.Add(new MongoIdentityClaim(claim));
        }

        public virtual void RemoveClaim(Claim claim)
        {
            MyClaims.RemoveAll(c =>
                c.Type == claim.Type &&
                c.Value == claim.Value);
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