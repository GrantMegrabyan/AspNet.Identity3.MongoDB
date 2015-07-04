using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNet.Identity3.MongoDB
{
    public class IdentityClaim
    {
        public IdentityClaim()
        {
        }

        public IdentityClaim(Claim claim)
        {
            ClaimType = claim.Type;
            ClaimValue = claim.Value;
        }

        /// <summary>
        /// Claim type
        /// </summary>
        public virtual string ClaimType { get; set; }

        /// <summary>
        /// Claim value
        /// </summary>
        public virtual string ClaimValue { get; set; }

        public Claim ToSecurityClaim()
        {
            return new Claim(ClaimType, ClaimValue);
        }
    }
}
