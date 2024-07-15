using System.Text.Json;
using financias.src.commands.BanckAccount;
using financias.src.interfaces;
using financiasapi.src.models.Enums;
using MediatR;

namespace financias.src.handlers
{
    public class UpdateBanckAccountHandler : IRequestHandler<UpdateBanckAcconutCommand>
    {
         private IUnitOFWork _unitOFWork;
         private ILogger<UpdateBanckAccountHandler> _logger { get; set; }

         public UpdateBanckAccountHandler(IUnitOFWork unitOFWork, ILogger<UpdateBanckAccountHandler> logger)
         {
            _unitOFWork =unitOFWork;
            _logger = logger;
         }
        

        public async Task Handle(UpdateBanckAcconutCommand request, CancellationToken cancellationToken)
        {
             _logger.LogInformation($"Start UpdateBanckAccountHandler with request {JsonSerializer.Serialize(request)}");
            var backAccount = await _unitOFWork.banckAccountRepository.GetById(request.Id);
            if(backAccount is null){
                throw new ApplicationException("BackAccount not found");
            }
            _logger.LogInformation($"BackAccount found {JsonSerializer.Serialize(backAccount)}");

            var banck = await _unitOFWork.banckRepository.GetById(request.BanckId);
            _logger.LogInformation($"Back found {JsonSerializer.Serialize(banck)}");
            if(banck is null){
                throw new ApplicationException("Banck not found");
            }
            _logger.LogInformation($"Strart creating object for update in database");

            backAccount.Name = request.Name;
            backAccount.Type =(AccountType)Enum.Parse(typeof(AccountType), request.Type);
            backAccount.BanckId = request.BanckId;

            _logger.LogInformation($"Object BanckAccount created {JsonSerializer.Serialize(backAccount)}");
            _logger.LogInformation("Start Update objects");

             _unitOFWork.banckAccountRepository.Update(backAccount);
                         
            await _unitOFWork.Commit();
            _logger.LogInformation("Object Commited Succefull");

           
        }
    }
}