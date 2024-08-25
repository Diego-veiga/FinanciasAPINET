using financias.src.Repository.Base;
using financiasapi.src.models;
using financiastests.helper;
using Xunit;

namespace financiastests.repository
{
    public class BaseRepositoryTests
    {
        

        [Fact]
        public void Add_ShouldAddEntityToDatabase()
        {
            var context = new RepositoryTestsHelper().GetInMemoryAppDbContext();
            var repository = new BaseRepository<User>(context);
            // Arrange
            var entity = new User(Guid.NewGuid(),"UserTest","teste@teste.com","123",true,DateTime.Now,DateTime.Now);
            entity.Salt =[1];

            // Act
            repository.Add(entity);
            context.SaveChanges();

            // Assert
            var entityInDb = context.Set<User>().Find(entity.Id);
            Assert.NotNull(entityInDb);
        }

        
        [Fact]
        public void Delete_ShouldPropertyActiveEqualFalse()
        {
                // Arrange
                var context = new RepositoryTestsHelper().GetInMemoryAppDbContext();
                var repository = new BaseRepository<User>(context);
                var entity = new User(Guid.NewGuid(),"UserTest","teste@teste.com","123",true,DateTime.Now,DateTime.Now);
                entity.Salt =[1];
                repository.Add(entity);
                context.SaveChanges();

                // Act
                repository.Delete(entity);
                context.SaveChanges();
                var userDeleted = context.Users.Find(entity.Id);

                // Assert
                Assert.False(userDeleted.Active);
        }
        
        [Fact]
        public void Delete_ShouldMarkEntityAsModified()
        {
                // Arrange
                var context = new RepositoryTestsHelper().GetInMemoryAppDbContext();
                var repository = new BaseRepository<User>(context);
                var entity = new User(Guid.NewGuid(),"UserTest","teste@teste.com","123",true,DateTime.Now,DateTime.Now);
                entity.Salt =[1];
                repository.Add(entity);
                context.SaveChanges();

                // Act
                repository.Delete(entity);
                context.SaveChanges();
                var userDeleted = context.Users.Find(entity.Id);

                // Assert
                Assert.False(userDeleted.Active);
        }
        
        [Fact]
        public async Task GetById_ShouldReturnEntityById()
        {
            // Arrange
            var context = new RepositoryTestsHelper().GetInMemoryAppDbContext();
            var repository = new BaseRepository<User>(context);
            var entity = new User(Guid.NewGuid(),"UserTest","teste@teste.com","123",true,DateTime.Now,DateTime.Now);
            entity.Salt =[1];
            repository.Add(entity);
            context.SaveChanges();
            

            // Act
            var result = await repository.GetById(entity.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(entity.Id, result.Id);
        }

        [Fact]
        public void Update_ShouldMarkEntityAsModified()
        {
            // Arrange
            var context = new RepositoryTestsHelper().GetInMemoryAppDbContext();
            var repository = new BaseRepository<User>(context);
            var entity = new User(Guid.NewGuid(),"UserTest","teste@teste.com","123",true,DateTime.Now,DateTime.Now);
            entity.Salt =[1];
            repository.Add(entity);
            context.SaveChanges();
        

            // Act
            var entityForUpdate = context.Users.Find(entity.Id);
            entityForUpdate.Name = "Updated Entity";
            repository.Update(entityForUpdate);
            context.SaveChanges();

            // Assert
            var entityInDb = context.Set<User>().Find(entity.Id);
            Assert.Equal("Updated Entity", entityInDb.Name);
        }

        [Fact]
        public void Get_ShouldReturnIQueryable()
        {
            // Arrange
            var context = new RepositoryTestsHelper().GetInMemoryAppDbContext();
            var repository = new BaseRepository<User>(context);
            var entity = new User(Guid.NewGuid(),"UserTest","teste@teste.com","123",true,DateTime.Now,DateTime.Now);
            entity.Salt =[1];
            repository.Add(entity);
            context.SaveChanges();
        

            // Act
            var entityInDb = repository.Get();

            // Assert
            
            Assert.Equal(entityInDb.Count(),1);
        }

    }
}