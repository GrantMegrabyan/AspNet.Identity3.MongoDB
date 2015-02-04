using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace AspNet.Identity3.MongoDB
{
    public class RoleStore<TUser, TRole, TContext> :
        IQueryableRoleStore<TRole>,
        IRoleClaimStore<TRole>
        where TUser : MongoIdentityUser
        where TRole : MongoIdentityRole
        where TContext : MongoIdentityContext<TUser, TRole>
    {
        private readonly TContext _context;

        public RoleStore(TContext context)
        {
            _context = context;
        }

        public IQueryable<TRole> Roles
        {
            get { throw new NotImplementedException("Roles"); }
        }

        public void Dispose()
        {
            // no need to dispose of anything, mongodb handles connection pooling automatically
        }

        public virtual async Task CreateAsync(TRole role, CancellationToken cancellationToken = new CancellationToken())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            await _context.Roles.InsertOneAsync(role, cancellationToken);
        }

        public virtual async Task UpdateAsync(TRole role, CancellationToken cancellationToken = new CancellationToken())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            var queryById = Query<TRole>.EQ(r => r.Id, role.Id);
            await _context.Roles.UpdateOneAsync(queryById, role, cancellationToken: cancellationToken);
        }

        public virtual async Task DeleteAsync(TRole role, CancellationToken cancellationToken = new CancellationToken())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            var queryById = Query<TRole>.EQ(r => r.Id, role.Id);
            await _context.Roles.DeleteOneAsync(queryById, cancellationToken);
        }

        public virtual Task<string> GetRoleIdAsync(TRole role, CancellationToken cancellationToken = new CancellationToken())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            return Task.FromResult(role.Id);
        }

        public virtual Task<string> GetRoleNameAsync(TRole role, CancellationToken cancellationToken = new CancellationToken())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            return Task.FromResult(role.Name);
        }

        public virtual Task SetRoleNameAsync(TRole role, string roleName, CancellationToken cancellationToken = new CancellationToken())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            role.Name = roleName;
            return Task.FromResult(0);
        }

        public virtual async Task<TRole> FindByIdAsync(string roleId, CancellationToken cancellationToken = new CancellationToken())
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _context.Roles.Find(r => r.Id == roleId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<TRole> FindByNameAsync(string roleName, CancellationToken cancellationToken = new CancellationToken())
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _context.Roles.Find(r => r.Name == roleName)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public virtual Task<IList<Claim>> GetClaimsAsync(TRole role, CancellationToken cancellationToken = new CancellationToken())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            return Task.FromResult((IList<Claim>)role.MyClaims.Select(c => c.ToSecurityClaim()).ToList());
        }

        public virtual Task AddClaimAsync(TRole role, Claim claim, CancellationToken cancellationToken = new CancellationToken())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }

            role.AddClaim(claim);

            return Task.FromResult(0);
        }

        public virtual Task RemoveClaimAsync(TRole role, Claim claim, CancellationToken cancellationToken = new CancellationToken())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }

            role.RemoveClaim(claim);

            return Task.FromResult(0);
        }
    }
}