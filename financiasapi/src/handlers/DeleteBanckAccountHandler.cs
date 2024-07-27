
using System.Text.Json;
using financias.src.commands.BankAccount;
using financias.src.interfaces;
using MediatR;

namespace financias.src.handlers
{
    public class DeleteBankAccountHandler : IRequestHandler<DeleteBankAccountCommand>
    {
        private readonly IUnitOFWork _unitOFWork;
        public readonly ILogger<DeleteBankAccountHandler> _logger;

         public DeleteBankAccountHandler(IUnitOFWork unitOFWork, ILogger<DeleteBankAccountHandler> logger)
         {
            _unitOFWork =unitOFWork;
            _logger = logger;
         }

        public async Task Handle(DeleteBankAccountCommand request, CancellationToken cancellationToken)
        {
           _logger.LogInformation($"Start UpdateBankAccountHandler with request {JsonSerializer.Serialize(request)}");
            var backAccount = await _unitOFWork.bankAccountRepository.GetById(request.Id);
            
            if(backAccount is null){
                throw new ApplicationException("BankAccount not found");
            }
            _logger.LogInformation($"BackAccount found {JsonSerializer.Serialize(backAccount)}");

            _logger.LogInformation($"Start creating object for delete in database");

            backAccount.Active=false;
            backAccount.UpdatedAt = DateTime.Now;

            _logger.LogInformation($"Object BankAccount created {JsonSerializer.Serialize(backAccount)}");
            _logger.LogInformation("Start Delete objects");

            _unitOFWork.bankAccountRepository.Delete(backAccount);

            await _unitOFWork.Commit();
            _logger.LogInformation("Object Committed Successful");
           
        }
    }
}