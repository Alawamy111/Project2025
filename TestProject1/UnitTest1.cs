using Microsoft.EntityFrameworkCore;
using Moq;
using Project2025.Components.Pages.PropertyOwnerComponents;
using Project2025.Data;
using Project2025.Data.Entity;

namespace TestProject1
{
    public class OwnerServices_GetTests 
    {
        private IDbContextFactory<ApplicationDbContext> GetDbContextFactory(DbContextOptions<ApplicationDbContext> options)
        {
            var mockFactory = new Mock<IDbContextFactory<ApplicationDbContext>>();
            mockFactory.Setup(f => f.CreateDbContext()).Returns(value: new ApplicationDbContext(options));
            return mockFactory.Object;
        }

        private static Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContext> GetDbContextOptions() => new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())

                .Options;



        //    اختبار حذف صاحب الشالية
        [Fact]
        public async Task DeleteAsync_ShouldRemovesOwner_WhenOwnerExists()
        {

            var options = GetDbContextOptions();
            var factory = GetDbContextFactory(options);
            using var dbContext = new ApplicationDbContext(options);

            var propertyOwner = new PropertyOwner
            {
                Id = Guid.NewGuid(),
                ownerName = "mohamed",
                email = "mm@",
                phone = 765499999
            };

            // نحفظ صاحب الشالية في قاعدة البيانات
            using (var m = factory.CreateDbContext())
            {
                m.PropertyOwners.Add(propertyOwner);
                await m.SaveChangesAsync();
            }
            options = GetDbContextOptions();
            factory = GetDbContextFactory(options);
            var service = new PropertyOwnerService(factory);

            // Act
            await service.DeleteAsync(propertyOwner);


            // Assert
            options = GetDbContextOptions();
            factory = GetDbContextFactory(options);
            using var Context = factory.CreateDbContext();

            var deleted = await Context.PropertyOwners.FindAsync(propertyOwner.Id);
            Assert.Null(deleted);
        }


        //  idاختبار الحصول على صاحب الشالية من خلال 
        [Fact]
        public async Task GetOwnerByownerId_ShouldReturnsOwner_WhenOwnerExist()
        {
            var options = GetDbContextOptions();
            var factory = GetDbContextFactory(options);

            // Arrange  
            var Id = Guid.NewGuid();

            var owner = new PropertyOwner
            {
                Id = Id,
                ownerName = " mohamed",
                email = "mm aa@",
                phone = 7654321
            };

            using var dbContext = new ApplicationDbContext(options);
            dbContext.PropertyOwners.Add(owner); // اضافة صاحب شالية إلى قاعدة البيانات  
            await dbContext.SaveChangesAsync();

            var service = new PropertyOwnerService(factory);

            // Act  
            var result = await service.GetPropertyOwnerByownerId(Id);

            // Assert  
            var addedOwner = await dbContext.PropertyOwners.FindAsync(result.Id);
            Assert.NotNull(addedOwner);
            Assert.Equal(Id, result.Id);
        }

        //اختبار الحصول على جميع اصحاب الشاليهات 
        [Fact]
        public async Task GetOwners_ShouldReturnsAllOwners_WhenOwnerExists()
        {
            // Arrange
            var options = GetDbContextOptions();
            var factory = GetDbContextFactory(options);
            using var dbContext = new ApplicationDbContext(options);

            dbContext.PropertyOwners.AddRange(

                new PropertyOwner { Id = Guid.NewGuid(), ownerName = "Owner 1", email = "mm@", phone = 987654 },
                new PropertyOwner { Id = Guid.NewGuid(), ownerName = "Owner 2", email = "aa@", phone = 65675 }
            );
            await dbContext.SaveChangesAsync();
            var service = new PropertyOwnerService(factory);

            // Act
            var result = await service.GetPropertyOwners();
            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }


        //اختبار تعديل صاحب عقار موجود بالفعل
        [Fact]
        public async Task UpsertOwner__ShouldUpdateExistingOwner_WhenOwnerExist()
        {
            var options = GetDbContextOptions();
            var factory = GetDbContextFactory(options);

            // Arrange
            var existingOwner = new PropertyOwner
            {
                Id = Guid.NewGuid(),
                ownerName = "tt Owner",
                email = "ttrt@",
                phone = 7654321
            };

            var cf = factory.CreateDbContext();
            cf.PropertyOwners.Add(existingOwner);
            await cf.SaveChangesAsync();

            // تعديل بيانات صاحب شالية
            var owner = new PropertyOwner
            {
                Id = existingOwner.Id,
                ownerName = " mohamed",
                email = "mm aa@",
                phone = 65757
            };

            var service = new PropertyOwnerService(factory);

            // Act
            var result = await service.Upsert(owner);

            // Assert
            var dbContext = factory.CreateDbContext();
            var updatedOwner = await dbContext.PropertyOwners.FindAsync(result.Id);
            Assert.NotNull(updatedOwner);
            Assert.Equal(owner.ownerName, updatedOwner.ownerName);
            Assert.Equal(owner.email, updatedOwner.email);
            Assert.Equal(owner.phone, updatedOwner.phone);


        }

        //                          اختبار اضافه صاحب شالية جديد

        [Fact]
        public async Task UpsertOwner__ShouldAddNewOwner_WhenDoesnotExist()
        {
            var options = GetDbContextOptions();
            var factory = GetDbContextFactory(options);
            // Arrange
            var owner = new PropertyOwner
            {
                Id = Guid.NewGuid(),
                ownerName = "علي",
                email = "ali@",
                phone = 0009999
            };

            using var dbContext = new ApplicationDbContext(options);
            var service = new PropertyOwnerService(factory);
            // Act
            var result = await service.Upsert(owner);
            // Assert
            var addedOwner = await dbContext.PropertyOwners.FindAsync(result.Id);
            Assert.NotNull(addedOwner);
            Assert.Equal(owner.Id, result.Id);



        }





    }
}

    



