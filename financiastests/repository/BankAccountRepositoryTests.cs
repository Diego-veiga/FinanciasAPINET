using financias.src.Repository;
using financiasapi.src.models;
using financiasapi.src.models.Enums;
using financiastests.helper;

using Xunit;

namespace financiastests.repository
{
    public class BankAccountRepositoryTests
    {

        [Fact]
        public async void GetByUserId_ExistingObjects_ReturnBankAccount()
        {
            var context = new RepositoryTestsHelper().GetInMemoryAppDbContext();
            var  userId = Guid.NewGuid();
            var  bankId = Guid.NewGuid();
            var  bankAccountId = Guid.NewGuid();
            var  userBanksAccountsId = Guid.NewGuid();
            var user =  new User(userId,"teste","teste@teste.com","123",true,DateTime.Now, DateTime.Now);
            user.Salt = [1];
            var bank =  new Bank(){ Id = bankId, Name = "bankTeste",UserId = userId, Active = true, Cnpj = "123456789", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now};
            var bankAccount  =  new BankAccount(bankAccountId,"TesteBankAccount",AccountType.Current,bankId,true,DateTime.Now,DateTime.Now);
            var userBanksAccounts  =  new UserBanksAccounts(){ Id = userBanksAccountsId, BankAccountId = bankAccountId, UserId =userId, Active = true,IsAdmin = true, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now};
 
            context.Users.Add(user);
            await context.SaveChangesAsync();
           
            context.Banks.Add(bank);
            context.SaveChangesAsync().GetAwaiter();
     
            context.BankAccounts.Add(bankAccount);
            await context.SaveChangesAsync();

            context.UserBanksAccounts.Add(userBanksAccounts);
            await context.SaveChangesAsync();
            var bankAccountRepository = new BankAccountRepository(context);

            var result = await bankAccountRepository.GetByUserId(userId);

            Assert.Equal(result.Count, 1);
            Assert.Equal(result[0].Name, "TesteBankAccount");
            Assert.Equal(result[0].Balance, 0);
            Assert.Equal(result[0].BankId, bankId);
            Assert.Equal(result[0].UserBanksAccounts.FirstOrDefault().UserId, userId);
        } 

        [Fact]
        public async void GetByUserId_NonExistentObject_ReturnBankAccount()
        {
            var context = new RepositoryTestsHelper().GetInMemoryAppDbContext();
            var  firstUserId = Guid.NewGuid();
            var  firstBankId = Guid.NewGuid();
            var  firstBankAccountId = Guid.NewGuid();         
            var  firstUserBanksAccountsId = Guid.NewGuid();
            
            var firstUser =  new User(firstUserId,"teste","teste@teste.com","123",true,DateTime.Now, DateTime.Now);
            firstUser.Salt = [1];
            var firstBank =  new Bank(){ Id = firstBankId, Name = "bankTeste",UserId = firstUserId, Active = true, Cnpj = "123456789", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now};       
            var firstBankAccount  =  new BankAccount(firstBankAccountId,"TesteBankAccount",AccountType.Current,firstBankId,true,DateTime.Now,DateTime.Now);
            var firstUserBanksAccounts  =  new UserBanksAccounts(){ Id = firstUserBanksAccountsId, BankAccountId = firstBankAccountId, UserId =firstUserId, Active = true,IsAdmin = true, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now};
 
            context.Users.Add(firstUser);
            await context.SaveChangesAsync();
            context.Banks.Add(firstBank);
            context.BankAccounts.Add(firstBankAccount);
            await context.SaveChangesAsync();
            context.UserBanksAccounts.Add(firstUserBanksAccounts);
            await context.SaveChangesAsync();

            var bankAccountRepository = new BankAccountRepository(context);

            var result = await bankAccountRepository.GetByUserId(firstBankId);

            Assert.Equal(result.Count, 0);
           
        } 
    }
}