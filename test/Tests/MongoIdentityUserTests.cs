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
    }
}