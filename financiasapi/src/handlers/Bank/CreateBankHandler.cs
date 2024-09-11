
using System.Text.Json;
using financias.src.interfaces;
using financiasapi.src.commands.Bank;
using financiasapi.src.models;
using MediatR;

namespace financiasapi.src.handlers.BankHandler
{
    public class CreateBankHandler : IRequestHandler<CreateBankCommand>
    {
        public IUnitOFWork _unitOfWork { get; set; }
         public ILogger<CreateBankHandler> _logger { get; set; }
        public CreateBankHandler(IUnitOFWork unitOfWork, ILogger<CreateBankHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Handle(CreateBankCommand request, CancellationToken cancellationToken)
        {
             _logger.LogInformation($"Start CreateBankHandler with request {JsonSerializer.Serialize(request)}");
             var user = await _unitOfWork.userRepository.GetById(request.UserId);

           _logger.LogInformation($"User found {JsonSerializer.Serialize(user)}");

            if(user is null){
                throw new ApplicationException("User not found");
            }
              _logger.LogInformation($"Start creating object for insert in database");

              var bank = new Bank(){
                    Id = Guid.NewGuid(),
                    Cnpj = request.Cnpj,
                    Name = request.Name,
                    UserId = request.UserId,
                    CreatedAt = DateTime.Now,
                    Active = true,
              };
             
             _logger.LogInformation($"Object Bank created {JsonSerializer.Serialize(bank)}");
             _logger.LogInformation("Start Add objects");
             _unitOfWork.bankRepository.Add(bank);

              await _unitOfWork.Commit();
            _logger.LogInformation("Object Committed Successful");

        }
    }
}