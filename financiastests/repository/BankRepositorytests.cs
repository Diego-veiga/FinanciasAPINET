using System.Text.Json;
using financias.src.Repository;
using financiasapi.src.models;
using financiastests.helper;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace financiastests.repository
{
    public class BankRepositoryTests
    {
        private readonly ITestOutputHelper _output;

        public BankRepositoryTests(ITestOutputHelper output)
        {
            _output = output;
        }
        [Fact]
        public async void GetByUserId_ExistingObjects_ReturnBank()
        {
            
            var context = new RepositoryTestsHelper().GetInMemoryAppDbContext("TestesBank");
            var  userId = Guid.NewGuid();
            var  bankId = Guid.NewGuid();
            
            var user =  new User(userId,"testeGetByUserId_ExistingObjects","esteGetByUserId_ExistingObjects@teste.com","123",true,DateTime.Now, DateTime.Now);
            user.Salt = [1];
            var bank =  new Bank(){ 
                                Id = bankId,
                                Name = "bankTeste",
                                UserId = userId, 
                                Active = true,
                                Cnpj = "123456789", 
                                CreatedAt = DateTime.Now, 
                                UpdatedAt = DateTime.Now
                            };
            
            context.Users.Add(user);
            context.Banks.Add(bank);
            context.SaveChanges();

            
               
            var bankRepository = new BankRepository(context);

            var result = await context.Banks.Where(u => u.UserId == user.Id ).ToListAsync();
            var result1 = await bankRepository.GetById(bankId);
            var result2 = await bankRepository.Get().ToListAsync();
            var result3 = await bankRepository.GetByUserId(userId);
          
            Assert.Equal(result.Count, 1);
            Assert.Equal(result[0].Name, "bankTeste");
            Assert.Equal(result[0].Cnpj, "123456789");
            Assert.Equal(result[0].UserId, userId);
        }

         [Fact]
        public async void GetByUserId_NonExistentObject_ReturnBank()
        {
            var context = new RepositoryTestsHelper().GetInMemoryAppDbContext("TestesBank");
            var  userId = Guid.NewGuid();
            var  bankId = Guid.NewGuid();
            
            var user =  new User(userId,"teste","teste@teste.com","123",true,DateTime.Now, DateTime.Now);
            user.Salt = [1];
            var bank =  new Bank(){ Id = bankId, Name = "bankTeste",UserId = userId, Active = true, Cnpj = "123456789", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now};
            
 
            context.Users.Add(user);
            await context.SaveChangesAsync();
           
            context.Banks.Add(bank);
            await  context.SaveChangesAsync();
    
            var bankAccountRepository = new BankRepository(context);

            var result = await bankAccountRepository.GetByUserId(Guid.NewGuid());

             Assert.Equal(result.Count, 0);
          
        }
    }
}