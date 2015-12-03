using AspNet.Identity3.MongoDB;
using System;
using MongoDB.Bson;
using Xunit;
using System.Security.Claims;

namespace Tests
{
    public class IdentityUserTests : IDisposable
    {
        private MongoIdentityContext<IdentityUser, IdentityRole> _context;

        public IdentityUserTests()
        {
            _context = new MongoIdentityContext<IdentityUser, IdentityRole>();
        }

        public void Dispose()
        {
            
        }

        [Fact]
        public void CreateUser_Id_IsAssigned()
        {
            var user = new IdentityUser();

            ObjectId userObjectId = user.Id;

            Assert.NotNull(userObjectId);
            Assert.NotEqual(userObjectId, ObjectId.Empty);
        }

        [Fact]
        public void CreateUser_Logins_NotNull()
        {
            var user = new IdentityUser();

            Assert.NotNull(user.Logins);
        }

        [Fact]
        public void CreateUser_Roles_NotNull()
        {
            var user = new IdentityUser();

            Assert.NotNull(user.Roles);
        }

        [Fact]
        public void CreateUser_Claims_NotNull()
        {
            var user = new IdentityUser();

            Assert.NotNull(user.Claims);
        }

        [Fact]
        public void UserWithoutClaims_AddClaim_OneClaimAdded()
        {
            var user = new IdentityUser();

            string claimType = "test-claim";
            string claimValue = "test-value";

            var claim = new Claim(claimType, claimValue);

            user.AddClaim(claim);

            Assert.Equal(1, user.Claims.Count);
            Assert.Equal(claimType, user.Claims[0].ClaimType);
            Assert.Equal(claimValue, user.Claims[0].ClaimValue);
        }

        [Fact]
        public void UserWithoutClaims_RemoveClaim_DoesNothing()
        {
            var user = new IdentityUser();

            string claimType = "test-claim";
            string claimValue = "test-value";

            var claim = new Claim(claimType, claimValue);

            user.RemoveClaim(claim);

            Assert.Empty(user.Claims);
        }

        [Fact]
        public void UserWithOneClaim_RemoveNonExistingClaim_DoesNothing()
        {
            var user = new IdentityUser();

            string claimType = "test-claim";
            string claimValue = "test-value";

            var claim = new Claim(claimType, claimValue);
            user.AddClaim(claim);

            user.RemoveClaim(new Claim("non-existing-type", "non-existing-value"));

            Assert.Equal(1, user.Claims.Count);
            Assert.Equal(claimType, user.Claims[0].ClaimType);
            Assert.Equal(claimValue, user.Claims[0].ClaimValue);
        }

        [Fact]
        public void UserWithOneClaim_RemoveClaimWithTheSameTypeButDifferentValue_DoesNothing()
        {
            var user = new IdentityUser();

            string claimType = "test-claim";
            string claimValue = "test-value";

            var claim = new Claim(claimType, claimValue);
            user.AddClaim(claim);

            user.RemoveClaim(new Claim(claimType, "non-existing-value"));

            Assert.Equal(1, user.Claims.Count);
            Assert.Equal(claimType, user.Claims[0].ClaimType);
            Assert.Equal(claimValue, user.Claims[0].ClaimValue);
        }

        [Fact]
        public void UserWithOneClaim_RemoveClaimWithTheSameValueButDifferentType_DoesNothing()
        {
            var user = new IdentityUser();

            string claimType = "test-claim";
            string claimValue = "test-value";

            var claim = new Claim(claimType, claimValue);
            user.AddClaim(claim);

            user.RemoveClaim(new Claim("non-existing-type", claimValue));

            Assert.Equal(1, user.Claims.Count);
            Assert.Equal(claimType, user.Claims[0].ClaimType);
            Assert.Equal(claimValue, user.Claims[0].ClaimValue);
        }

        [Fact]
        public void UserWithOneClaim_RemoveTheClaim_NoClaimsRemain()
        {
            var user = new IdentityUser();

            string claimType = "test-claim";
            string claimValue = "test-value";

            var claim1 = new Claim(claimType, claimValue);
            var claim2 = new Claim(claimType, claimValue);

            user.AddClaim(claim1);
            user.RemoveClaim(claim2);

            Assert.Empty(user.Claims);
        }

        [Fact]
        public void UserWithTwoClaims_RemoveOneOfTheClaims_OtherClaimRemains()
        {
            var user = new IdentityUser();

            string claimType1 = "test-claim-1";
            string claimValue1 = "test-value-1";
            var claim1 = new Claim(claimType1, claimValue1);

            string claimType2 = "test-claim-2";
            string claimValue2 = "test-value-2";
            var claim2 = new Claim(claimType2, claimValue2);

            user.AddClaim(claim1);
            user.AddClaim(claim2);

            user.RemoveClaim(new Claim(claimType1, claimValue1));

            Assert.Equal(1, user.Claims.Count);
            Assert.Equal(claimType2, user.Claims[0].ClaimType);
            Assert.Equal(claimValue2, user.Claims[0].ClaimValue);
        }
    }
}