using AspNet.Identity3.MongoDB;
using System;
using MongoDB.Bson;
using Xunit;

namespace Tests
{
    public class UserBsonRepresentationTests : IDisposable
    {
        private MongoIdentityContext<IdentityUser, IdentityRole> _context;

        public UserBsonRepresentationTests()
        {
            _context = new MongoIdentityContext<IdentityUser, IdentityRole>();
        }

        public void Dispose()
        {

        }

        [Fact]
        public void User_Id_IsRepresentedAsBsonObjectId()
        {
            var user = new IdentityUser();

            var document = user.ToBsonDocument();

            Assert.IsType<BsonObjectId>(document["_id"]);
        }

        [Fact]
        public void UserWithoutLogins_MyLogins_AreIgnored()
        {
            var user = new IdentityUser();

            var document = user.ToBsonDocument();

            Assert.False(document.Contains("MyLogins"));
        }

        [Fact]
        public void UserWithoutRoles_MyRoles_AreIgnored()
        {
            var user = new IdentityUser();

            var document = user.ToBsonDocument();

            Assert.False(document.Contains("MyRoles"));
        }

        [Fact]
        public void UserWithoutClaims_MyClaims_AreIgnored()
        {
            var user = new IdentityUser();

            var document = user.ToBsonDocument();

            Assert.False(document.Contains("MyClaims"));
        }
    }
}
