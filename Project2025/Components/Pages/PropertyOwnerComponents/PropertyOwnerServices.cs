using Microsoft.EntityFrameworkCore;
using Project2025.Data;
using Project2025.Data.Entity;



namespace Project2025.Components.Pages.PropertyOwnerComponents
{
    public class PropertyOwnerService : IPropertyOwnerService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public PropertyOwnerService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {

            _dbContextFactory = dbContextFactory;

        }

        public async Task DeleteAsync(PropertyOwner propertyOwner)
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            var existingPropertyOwner = _dbContext.PropertyOwners.Find(propertyOwner.Id);
            if (existingPropertyOwner != null)
            {
                _dbContext.PropertyOwners.Remove(existingPropertyOwner);
                await _dbContext.SaveChangesAsync();
            }
        }

        public Task<PropertyOwner?> GetPropertyOwnerByownerId(Guid ownerId)
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            return _dbContext.PropertyOwners.FirstOrDefaultAsync(a => a.Id == ownerId);

        }

        public Task<List<PropertyOwner>> GetPropertyOwners()
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            return _dbContext.PropertyOwners.ToListAsync();

        }

        public async Task<PropertyOwner> Upsert(PropertyOwner propertyOwner)
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            var existingOwner = await _dbContext.PropertyOwners.FirstOrDefaultAsync(a => a.Id == propertyOwner.Id);
            if (existingOwner != null)
            {
                existingOwner.ownerName = propertyOwner.ownerName;
                existingOwner.email = propertyOwner.email;
                existingOwner.phone = propertyOwner.phone;

                _dbContext.PropertyOwners.Update(existingOwner);
            }
            else
            {
                await _dbContext.PropertyOwners.AddAsync(propertyOwner);
            }
            await _dbContext.SaveChangesAsync();
            return propertyOwner;
        }



    }
}