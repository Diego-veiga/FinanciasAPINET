using System.Text.Json;
using financias.src.interfaces;
using financiasapi.src.commands.Bank;
using MediatR;

namespace financiasapi.src.handlers.BankDelete
{
    public class DeleteBankHandler: IRequestHandler<DeleteBankCommand>
    {
        private readonly IUnitOFWork _unitOFWork;
        public readonly ILogger<DeleteBankHandler> _logger;

         public DeleteBankHandler(IUnitOFWork unitOFWork, ILogger<DeleteBankHandler> logger)
         {
            _unitOFWork =unitOFWork;
            _logger = logger;
         }

        public async Task Handle(DeleteBankCommand request, CancellationToken cancellationToken)
        {
           _logger.LogInformation($"Start DeleteBankHandler with request {JsonSerializer.Serialize(request)}");

           var bank = await _unitOFWork.bankRepository.GetById(request.Id);
           
            _logger.LogInformation($"Result GetById {JsonSerializer.Serialize(bank)}");
            
            if (bank is null)
            {
                throw new ApplicationException("bank not found ");
            }

            if (bank.UserId == null)
            {
                throw new ApplicationException("default bank can't deleted ");

            }

            _logger.LogInformation($"Start creating object for delete in database");
           
            bank.Active = false;
            bank.UpdatedAt = DateTime.Now;

            _logger.LogInformation($"Object BankAccount created {JsonSerializer.Serialize(bank)}");

            _unitOFWork.bankRepository.Delete(bank!);
            await _unitOFWork.Commit();
            _logger.LogInformation("Object Committed Successful"); 
        }
    }
}