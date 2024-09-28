
using financias.src.interfaces;
using financiasapi.src.commands.Bank;
using MediatR;

namespace financiasapi.src.handlers.BankUpdate
{
    public class UpdateCommandHandler : IRequestHandler<UpdateBankCommand>
    {

         public IUnitOFWork _unitOfWork { get; set; }
         public ILogger<UpdateCommandHandler> _logger { get; set; }
        public UpdateCommandHandler(IUnitOFWork unitOfWork, ILogger<UpdateCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Handle(UpdateBankCommand request, CancellationToken cancellationToken)
        {
           var bank = await _unitOfWork.bankRepository.GetById(request.Id);

            if (bank is null)
            {
                throw new ApplicationException("Bank not found");
            }
            bank.Cnpj = request.Cnpj.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
            bank.Name = request.Name;
            bank.UpdatedAt = DateTime.Now;
            _unitOfWork.bankRepository.Update(bank!);
            await _unitOfWork.Commit();
        }
    }
}