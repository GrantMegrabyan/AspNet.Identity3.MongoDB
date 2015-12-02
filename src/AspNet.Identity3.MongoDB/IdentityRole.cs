using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using MongoDB.Bson;

namespace AspNet.Identity3.MongoDB
{
    /// <summary>
    /// Represents a Role entity
    /// </summary>
    public class IdentityRole
    {
        public IdentityRole()
        {
            Id = ObjectId.GenerateNewId();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="roleName"></param>
        public IdentityRole(string roleName) 
            : this()
        {
            Name = roleName;
        }

        /// <summary>
        /// Role Id
        /// </summary>
        public ObjectId Id { get; set; }

        /// <summary>
        /// Role name
        /// </summary>
        public string Name { get; set; }
        public string NormalizedName { get; set; }

        /// <summary>
        /// A random value that should change whenever a role is persisted to the store
        /// </summary>
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Collection of claims in the role
        /// </summary>
        public List<IdentityClaim> Claims { get; } = new List<IdentityClaim>();

        public virtual void AddClaim(Claim claim)
        {
            Claims.Add(new IdentityClaim(claim));
        }

        public virtual void RemoveClaim(Claim claim)
        {
            Claims.RemoveAll(c => c.ClaimType == claim.Type &&
                                  c.ClaimValue == claim.Value);
        }

        public virtual void ReplaceClaim(Claim claim, Claim newClaim)
        {
            foreach (var userClaim in Claims.Where(userClaim => userClaim.ClaimType == claim.Type &&
                                                                userClaim.ClaimValue == claim.Value))
            {
                userClaim.ClaimType = newClaim.Type;
                userClaim.ClaimValue = newClaim.Value;
            }
        }
    }
}