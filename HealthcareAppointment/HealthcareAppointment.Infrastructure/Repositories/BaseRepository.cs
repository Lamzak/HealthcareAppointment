using HealthcareAppointment.Application.Interfaces;
using HealthcareAppointment.Domain.Constants;
using HealthcareAppointment.Domain.Entities.Base;
using HealthcareAppointment.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HealthcareAppointment.Infrastructure.Repositories
{
    public class BaseRepository<T>(AppDbContext context, ICurrentUserService currentUserService) : IBaseRepository<T> where T : BaseEntity
    {
        public async Task<T?> GetByIdAsync(Guid id) => await context.Set<T>().FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            ApplyTrackableInformation(entity, isNew: true);

            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            ApplyTrackableInformation(entity, isNew: false);

            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        private void ApplyTrackableInformation(T entity, bool isNew)
        {
            var userId = currentUserService.UserId ?? SystemUsers.Id;
            var now = DateTime.UtcNow;

            if (isNew)
            {
                entity.CreatedAt = now;
                entity.CreatedBy = userId;
            }
            else
            {
                entity.UpdatedAt = now;
                entity.UpdatedBy = userId;
            }
        }
    }
}
