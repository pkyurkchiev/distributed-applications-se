using MC.Data.Entities;
using MC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MC.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected DbSet<T> DbSet { get; set; }

        protected DbContext Context { get; set; }

        public Repository(DbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context), "An instance of DbContext is required to use this repository.");
            this.DbSet = this.Context.Set<T>();
        }

        public virtual void ActivateDeactivate(T entity)
        {
            entity.IsActivated = !entity.IsActivated;
            this.Update(entity);
        }

        public virtual void ActivateDeactivate(int id)
        {
            var entity = this.DbSet.Find(id);
            if (entity != null)
            {
                ActivateDeactivate(entity);
            }
        }

        public void Delete(T entity)
        {
            EntityEntry<T> entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.DbSet.Attach(entity);
                this.DbSet.Remove(entity);
            }
        }

        public void Delete(int id)
        {
            var entity = this.GetByIdAsync(id).Result;
            if (entity != null)
            {
                this.Delete(entity);
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(bool isActive = true) => await SoftDeleteQueryFilter(this.DbSet.AsQueryable(), isActive).ToListAsync();

        public async Task<T> GetByIdAsync(int id, bool isActive = true) => await this.DbSet.FindAsync(id);

        public void Insert(T entity)
        {
            entity.CreatedBy = 1;
            entity.CreatedOn = DateTime.UtcNow;
            entity.IsActivated = true;

            EntityEntry<T> entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else 
            {
                this.DbSet.AddAsync(entity);
            }
        }

        public void Update(T entity, string excludeProperties = "")
        {
            entity.UpdatedBy = 1;
            entity.UpdatedOn = DateTime.UtcNow;

            EntityEntry<T> entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;

            entry.Property("CreatedBy").IsModified = false;
            entry.Property("CreatedOn").IsModified = false;

            foreach (var excludeProperty in excludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                entry.Property(excludeProperty).IsModified = false;
            }

        }

        public virtual void Save(T entity)
        {
            if (entity.Id == 0)
            {
                this.Insert(entity);
            }
            else
            {
                this.Update(entity);
            }
        }

        protected static IQueryable<T> SoftDeleteQueryFilter(IQueryable<T> query, bool? isActive)
        {
            if (isActive.HasValue)
            {
                query = query.Where(x => x.IsActivated == isActive.Value);
            }
            return query;
        }
    }
}
