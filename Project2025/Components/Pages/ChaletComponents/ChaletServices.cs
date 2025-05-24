using Microsoft.EntityFrameworkCore;
using Project2025.Data.Entity;
using Project2025.Data;

namespace Project2025.Components.Pages.ChaletComponents
{
    public class ChaletServices : IChaletServices
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public ChaletServices(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {

            _dbContextFactory = dbContextFactory;

        }

        public async Task DeleteAsync(Chalet chalet)
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            var existingChalet = _dbContext.Chalets.Find(chalet.ChaletId);
            if (existingChalet != null)
            {
                _dbContext.Chalets.Remove(existingChalet);
                await _dbContext.SaveChangesAsync();
            }
        }

        public Task<Chalet?> GetChaletByChaletId(Guid ChaletId)
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            return _dbContext.Chalets.FirstOrDefaultAsync(p => p.ChaletId == ChaletId);

        }

        public Task<List<Chalet>> GetChalets()
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            return _dbContext.Chalets.ToListAsync();

        }

        public async Task<Chalet> Upsert(Chalet chalet)
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            var existingOwner = await _dbContext.Chalets.FirstOrDefaultAsync(a => a.ChaletId == chalet.ChaletId);
            if (existingOwner != null)
            {
             
                existingOwner.OwnerId = chalet.OwnerId;
                existingOwner.price = chalet.price;
                existingOwner.area = chalet.area;
                existingOwner.location = chalet.location;


                _dbContext.Chalets.Update(existingOwner);
            }
            else
            {
                await _dbContext.Chalets.AddAsync(chalet);
            }
            await _dbContext.SaveChangesAsync();
            return chalet;
        }



    }
}
