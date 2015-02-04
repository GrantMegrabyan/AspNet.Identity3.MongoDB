using System;
using System.Security.Claims;

namespace AspNet.Identity3.MongoDB
{
    public class MongoIdentityClaim
    {
        public MongoIdentityClaim()
        {
        }

        public MongoIdentityClaim(Claim claim)
        {
            Type = claim.Type;
            Value = claim.Value;
        }

        public string Type { get; set; }
        public string Value { get; set; }

        public Claim ToSecurityClaim()
        {
            return new Claim(Type, Value);
        }
    }
}