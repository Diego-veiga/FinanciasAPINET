
using System.Text.Json;
using financias.src.commands.BankAccount;
using financias.src.interfaces;
using financiasapi.src.models;
using financiasapi.src.models.Enums;
using MediatR;

namespace financias.src.handlers
{
    public class CreateBankAccountHandler : IRequestHandler<CreateBankAccountCommand>
    {
         private IUnitOFWork _unitOFWork;
         public ILogger<CreateBankAccountHandler> _logger { get; set; }

         public CreateBankAccountHandler(IUnitOFWork unitOFWork,ILogger<CreateBankAccountHandler> logger)
         {
            _unitOFWork =unitOFWork;
            _logger = logger;
         }
    
        public async Task Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start CreateBankAccountHandler with request {JsonSerializer.Serialize(request)}");
            _logger.LogInformation($"Handle invoked  with request {JsonSerializer.Serialize(request)}");
            var user = await _unitOFWork.userRepository.GetById(request.UserId);

           _logger.LogInformation($"User found {JsonSerializer.Serialize(user)}");

            if(user is null){
                throw new ApplicationException("User not found");
            }

            var banck = await _unitOFWork.bankRepository.GetById(request.BankId);

             _logger.LogInformation($"Banck found {JsonSerializer.Serialize(banck)}");

            if(banck is null){
                throw new ApplicationException("Banck not found");
               
            }
            
            _logger.LogInformation($"Start creating object for insert in database");

            var bankAccount = new BankAccount(Guid.NewGuid(),request.Name,(AccountType)Enum.Parse(typeof(AccountType), request.Type),request.BankId,true,DateTime.Now, DateTime.Now);
           
           _logger.LogInformation($"Object BankAccount created {JsonSerializer.Serialize(bankAccount)}");

            var userBankAccount = new UserBanksAccounts()
            {
                Id=Guid.NewGuid(),
                BankAccountId = bankAccount.Id,
                UserId=request.UserId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsAdmin=true,
                Active=true
            };
            
             _logger.LogInformation($"Object UserBankAccount created {JsonSerializer.Serialize(userBankAccount)}");
             _logger.LogInformation("Start Add objects");

             _unitOFWork.bankAccountRepository.Add(bankAccount);
             _unitOFWork.userBanksAccountsRepository.Add(userBankAccount);
             
             
            await _unitOFWork.Commit();
            _logger.LogInformation("Object Committed Successful");
           
        }
    }
}