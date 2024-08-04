using System.Text.Json;
using financias.src.commands.BankAccount;
using financias.src.interfaces;
using financiasapi.src.models.Enums;
using MediatR;

namespace financias.src.handlers
{
    public class UpdateBankAccountHandler : IRequestHandler<UpdateBankAccountCommand>
    {
         private IUnitOFWork _unitOFWork;
         private ILogger<UpdateBankAccountHandler> _logger { get; set; }

         public UpdateBankAccountHandler(IUnitOFWork unitOFWork, ILogger<UpdateBankAccountHandler> logger)
         {
            _unitOFWork =unitOFWork;
            _logger = logger;
         }
        

        public async Task Handle(UpdateBankAccountCommand request, CancellationToken cancellationToken)
        {
             _logger.LogInformation($"Start UpdateBanckAccountHandler with request {JsonSerializer.Serialize(request)}");
            var backAccount = await _unitOFWork.bankAccountRepository.GetById(request.Id);
            if(backAccount is null){
                throw new ApplicationException("BackAccount not found");
            }
            _logger.LogInformation($"BackAccount found {JsonSerializer.Serialize(backAccount)}");

            var banck = await _unitOFWork.bankRepository.GetById(request.BanckId);
            _logger.LogInformation($"Back found {JsonSerializer.Serialize(banck)}");
            if(banck is null){
                throw new ApplicationException("Banck not found");
            }
            _logger.LogInformation($"Start creating object for update in database");

            backAccount.Name = request.Name;
            backAccount.Type =(AccountType)Enum.Parse(typeof(AccountType), request.Type);
            backAccount.BankId = request.BanckId;

            _logger.LogInformation($"Object BanckAccount created {JsonSerializer.Serialize(backAccount)}");
            _logger.LogInformation("Start Update objects");

             _unitOFWork.bankAccountRepository.Update(backAccount);
                         
            await _unitOFWork.Commit();
            _logger.LogInformation("Object Committed Successful");

           
        }
    }
}