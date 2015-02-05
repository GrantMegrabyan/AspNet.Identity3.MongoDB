# AspNet.Identity3.MongoDB
ASP.NET 5 uses new version of [Identity system](https://github.com/aspnet/Identity) which has only Entity Framework provider.
Goal of this project is to create provider which uses MongoDB as storage.

## CoreCLR support

I use official [MongoDB C# Driver](https://github.com/mongodb/mongo-csharp-driver) which does not have CoreCLR support yet. As soon as they will start working with CoreCLR I'll update this project.

## Installation

Add dependency to you project.json:

	{
        ...
	    "dependencies": {
		    ...,
		    "AspNet.Identity3.MongoDB": "0.0.1"
	    },
        ...
    }

## Usage

Here I'm going to show how to use AspNet.Identity3.MongoDB in ASP.NET 5 MVC sample project.

### User and Role Model

First we need models for users and roles.
Create ApplicationUser derived from MongoIdentityUser: 

    public class ApplicationUser : MongoIdentityUser
    {

    }

For roles I'll use MongoIdentityRole class.

### DbContext

Derive ApplicationDbContext class from MongoIdentityContext:

    public class ApplicationDbContext : MongoIdentityContext<ApplicationUser, MongoIdentityRole>
    {
        public ApplicationDbContext()
            : base()
        {
            string connectionString = "";
            string databaseName = "";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            this.Users = database.GetCollection<ApplicationUser>("users");
            this.Roles = database.GetCollection<MongoIdentityRole>("roles");
        }
    }

### Setting up

In Startup.cs:

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ApplicationDbContext>();

        services.AddIdentity<ApplicationUser, MongoIdentityRole>(Configuration)
            .AddMongoStores<ApplicationDbContext, ApplicationUser, MongoIdentityRole>();

        ...
    }

