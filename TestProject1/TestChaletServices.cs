using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Project2025.Components.Pages.ChaletComponents;
using Project2025.Data;
using Project2025.Data.Entity;

namespace TestProject1
{
    public class TestChaletServices
    {
        private static DbContextOptions<ApplicationDbContext> GetDbContextOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        //    اختبار حذف الشالية
        [Fact]
        public async Task DeleteAsync_ShouldRemovesChalet_WhenChaletExists()
        {
            var options = GetDbContextOptions();
            var factory = new PooledDbContextFactory<ApplicationDbContext>(options);

            var chalet = new Chalet
            {
                ChaletId = Guid.NewGuid(),
                location = "قنفوده",
                area = 500,
                price = 60000,
                OwnerId = Guid.NewGuid()
            };

            using (var dbContext = new ApplicationDbContext(options))
            {
                dbContext.Chalets.Add(chalet);
                await dbContext.SaveChangesAsync();
            }

            var service = new ChaletServices(factory);

            await service.DeleteAsync(chalet);

            using (var dbContext = new ApplicationDbContext(options))
            {
                var deletedChalet = await dbContext.Chalets.FindAsync(chalet.ChaletId);
                Assert.Null(deletedChalet);
            }
        }

        //    اختبار الحصول على الشاليه من خلال معرف الشاليه
        [Fact]
        public async Task GetChaletById_ShouldReturnChalet_WhenChaletExists()
        {
            var options = GetDbContextOptions();
            var factory = new PooledDbContextFactory<ApplicationDbContext>(options);

            // أضف البيانات  
            using (var dbContext = new ApplicationDbContext(options))
            {
                dbContext.Chalets.Add(new Chalet
                {
                    ChaletId = Guid.NewGuid(),
                    location = "قريونس",
                    area = 200,
                    price = 90000,
                    OwnerId = Guid.NewGuid()
                });
                await dbContext.SaveChangesAsync();
            }

            var service = new ChaletServices(factory);
            var result = await service.GetChalets();

            Assert.NotNull(result);
            Assert.Single(result);

        }



        //    اختبار الحصول على جميع الشاليهات
        [Fact]
        public async Task GetChalets_ShouldReturnAllChalets_WhenChaletsExist()
        {
            var options = GetDbContextOptions();
            var factory = new PooledDbContextFactory<ApplicationDbContext>(options);

            var chalet1 = new Chalet
            {
                ChaletId = Guid.NewGuid(),
                location = "قريونس",
                area = 3000,
                price = 400000,
                OwnerId = Guid.NewGuid()
            };
            var chalet2 = new Chalet
            {
                ChaletId = Guid.NewGuid(),
                location = "صابري",
                area = 1000,
                price = 200000,
                OwnerId = Guid.NewGuid()
            };

            using (var dbContext = new ApplicationDbContext(options))
            {
                dbContext.Chalets.AddRange(chalet1, chalet2);
                await dbContext.SaveChangesAsync();
            }

            var service = new ChaletServices(factory);

            var result = await service.GetChalets();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        //    اختبار الحصول على جميع الشاليهات من خلال معرف صاحب الشالية
        [Fact]
        public async Task GetChaletsByOwnerId_ShouldReturnChalets_WhenOwnerHasChalets()
        {
            var options = GetDbContextOptions();
            var factory = new PooledDbContextFactory<ApplicationDbContext>(options);

            var ownerId = Guid.NewGuid();
            var chalet1 = new Chalet
            {
                ChaletId = Guid.NewGuid(),
                location = "صابري",
                area = 600,
                price = 70000,
                OwnerId = ownerId
            };
            var chalet2 = new Chalet
            {
                ChaletId = Guid.NewGuid(),
                location = "قنفودة",
                area = 200,
                price = 50000,
                OwnerId = ownerId
            };

            using (var dbContext = new ApplicationDbContext(options))
            {
                dbContext.Chalets.AddRange(chalet1, chalet2);
                await dbContext.SaveChangesAsync();
            }

            var service = new ChaletServices(factory);

            var result = await service.GetChaletsByOwnerId(ownerId);

            Assert.NotNull(result);
        }

        //    اختبار اضافة شالية جديده
        [Fact]
        public async Task AddChalet_ShouldAddChalet_WhenDoesnotExist()
        {
            var options = GetDbContextOptions();
            var factory = new PooledDbContextFactory<ApplicationDbContext>(options);

            var chalet = new Chalet
            {
                ChaletId = Guid.NewGuid(),
                location = "الصابري",
                area = 200,
                price = 30000,
                OwnerId = Guid.NewGuid()
            };

            var service = new ChaletServices(factory);
            await service.Upsert(chalet);

            using (var dbContext = new ApplicationDbContext(options))
            {
                var addedChalet = await dbContext.Chalets.FindAsync(chalet.ChaletId);
                Assert.NotNull(addedChalet);
                Assert.Equal(chalet.location, addedChalet.location);
                Assert.Equal(chalet.area, addedChalet.area);
                Assert.Equal(chalet.price, addedChalet.price);
                Assert.Equal(chalet.OwnerId, addedChalet.OwnerId);
            }
        }

        //    اختبار تعديل بيانات شالية
        [Fact]
        public async Task UpdateChalet_ShouldUpdateChalet_WhenChaletExists()
        {
            var options = GetDbContextOptions();
            var factory = new PooledDbContextFactory<ApplicationDbContext>(options);

            var existingChalet = new Chalet
            {
                ChaletId = Guid.NewGuid(),
                location = "سيدي خليفة",
                area = 100,
                price = 40000,
                OwnerId = Guid.NewGuid()
            };

            using (var dbContext = new ApplicationDbContext(options))
            {
                dbContext.Chalets.Add(existingChalet);
                await dbContext.SaveChangesAsync();
            }

            var service = new ChaletServices(factory);

            var chalet = new Chalet
            {
                ChaletId = existingChalet.ChaletId,
                location = "توكرة",
                area = 600,
                price = 70000,
                OwnerId = existingChalet.OwnerId
            };

            await service.Upsert(chalet);

            using (var dbContext = new ApplicationDbContext(options))
            {
                var updatedChalet = await dbContext.Chalets.FindAsync(chalet.ChaletId);
                Assert.NotNull(updatedChalet);
                Assert.Equal(chalet.location, updatedChalet.location);
                Assert.Equal(chalet.area, updatedChalet.area);
                Assert.Equal(chalet.price, updatedChalet.price);
                Assert.Equal(chalet.OwnerId, updatedChalet.OwnerId);
            }
        }
    }
}
