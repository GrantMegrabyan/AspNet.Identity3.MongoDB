using AspNet.Identity3.MongoDB;
using System;
using MongoDB.Bson;
using Xunit;
using System.Security.Claims;

namespace Tests
{
    public class MongoIdentityUserTests : IDisposable
    {
        private MongoIdentityContext<MongoIdentityUser, MongoIdentityRole> _context;

        public MongoIdentityUserTests()
        {
            _context = new MongoIdentityContext<MongoIdentityUser, MongoIdentityRole>();
        }

        public void Dispose()
        {
            
        }

        [Fact]
        public void CreateUser_Id_IsAssigned()
        {
            var user = new MongoIdentityUser();

            ObjectId userObjectId;
            ObjectId.TryParse(user.Id, out userObjectId);

            Assert.NotNull(userObjectId);
            Assert.NotEqual(userObjectId, ObjectId.Empty);
        }

        [Fact]
        public void CreateUser_MyLogins_NotNull()
        {
            var user = new MongoIdentityUser();

            Assert.NotNull(user.MyLogins);
        }

        [Fact]
        public void CreateUser_MyRoles_NotNull()
        {
            var user = new MongoIdentityUser();

            Assert.NotNull(user.MyRoles);
        }

        [Fact]
        public void CreateUser_MyClaims_NotNull()
        {
            var user = new MongoIdentityUser();

            Assert.NotNull(user.MyClaims);
        }

        [Fact]
        public void UserWithoutClaims_AddClaim_OneClaimAdded()
        {
            var user = new MongoIdentityUser();

            string claimType = "test-claim";
            string claimValue = "test-value";

            var claim = new Claim(claimType, claimValue);

            user.AddClaim(claim);

            Assert.Equal(1, user.MyClaims.Count);
            Assert.Equal(claimType, user.MyClaims[0].Type);
            Assert.Equal(claimValue, user.MyClaims[0].Value);
        }

        [Fact]
        public void UserWithoutClaims_RemoveClaim_DoesNothing()
        {
            var user = new MongoIdentityUser();

            string claimType = "test-claim";
            string claimValue = "test-value";

            var claim = new Claim(claimType, claimValue);

            user.RemoveClaim(claim);

            Assert.Empty(user.MyClaims);
        }

        [Fact]
        public void UserWithOneClaim_RemoveNonExistingClaim_DoesNothing()
        {
            var user = new MongoIdentityUser();

            string claimType = "test-claim";
            string claimValue = "test-value";

            var claim = new Claim(claimType, claimValue);
            user.AddClaim(claim);

            user.RemoveClaim(new Claim("non-existing-type", "non-existing-value"));

            Assert.Equal(1, user.MyClaims.Count);
            Assert.Equal(claimType, user.MyClaims[0].Type);
            Assert.Equal(claimValue, user.MyClaims[0].Value);
        }

        [Fact]
        public void UserWithOneClaim_RemoveClaimWithTheSameTypeButDifferentValue_DoesNothing()
        {
            var user = new MongoIdentityUser();

            string claimType = "test-claim";
            string claimValue = "test-value";

            var claim = new Claim(claimType, claimValue);
            user.AddClaim(claim);

            user.RemoveClaim(new Claim(claimType, "non-existing-value"));

            Assert.Equal(1, user.MyClaims.Count);
            Assert.Equal(claimType, user.MyClaims[0].Type);
            Assert.Equal(claimValue, user.MyClaims[0].Value);
        }

        [Fact]
        public void UserWithOneClaim_RemoveClaimWithTheSameValueButDifferentType_DoesNothing()
        {
            var user = new MongoIdentityUser();

            string claimType = "test-claim";
            string claimValue = "test-value";

            var claim = new Claim(claimType, claimValue);
            user.AddClaim(claim);

            user.RemoveClaim(new Claim("non-existing-type", claimValue));

            Assert.Equal(1, user.MyClaims.Count);
            Assert.Equal(claimType, user.MyClaims[0].Type);
            Assert.Equal(claimValue, user.MyClaims[0].Value);
        }

        [Fact]
        public void UserWithOneClaim_RemoveTheClaim_NoClaimsRemain()
        {
            var user = new MongoIdentityUser();

            string claimType = "test-claim";
            string claimValue = "test-value";

            var claim1 = new Claim(claimType, claimValue);
            var claim2 = new Claim(claimType, claimValue);

            user.AddClaim(claim1);
            user.RemoveClaim(claim2);

            Assert.Empty(user.MyClaims);
        }

        [Fact]
        public void UserWithTwoClaims_RemoveOneOfTheClaims_OtherClaimRemains()
        {
            var user = new MongoIdentityUser();

            string claimType1 = "test-claim-1";
            string claimValue1 = "test-value-1";
            var claim1 = new Claim(claimType1, claimValue1);

            string claimType2 = "test-claim-2";
            string claimValue2 = "test-value-2";
            var claim2 = new Claim(claimType2, claimValue2);

            user.AddClaim(claim1);
            user.AddClaim(claim2);

            user.RemoveClaim(new Claim(claimType1, claimValue1));

            Assert.Equal(1, user.MyClaims.Count);
            Assert.Equal(claimType2, user.MyClaims[0].Type);
            Assert.Equal(claimValue2, user.MyClaims[0].Value);
        }
    }
}