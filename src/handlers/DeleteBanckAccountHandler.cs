
using System.Text.Json;
using financias.src.commands.BanckAccount;
using financias.src.interfaces;
using financias.src.models;
using financias.src.models.Enums;
using MediatR;

namespace financias.src.handlers
{
    public class DeleteBanckAccountHandler : IRequestHandler<DeleteBanckAcconutCommand>
    {
         private IUnitOFWork _unitOFWork;
        public ILogger<DeleteBanckAccountHandler> _logger { get; set; }

         public DeleteBanckAccountHandler(IUnitOFWork unitOFWork,ILogger<DeleteBanckAccountHandler> logger)
         {
            _unitOFWork =unitOFWork;
            _logger = logger;
         }

        public async Task Handle(DeleteBanckAcconutCommand request, CancellationToken cancellationToken)
        {
           _logger.LogInformation($"Start UpdateBanckAccountHandler with request {JsonSerializer.Serialize(request)}");
            var backAccount = await _unitOFWork.banckAccountRepository.GetById(request.Id);
            
            if(backAccount is null){
                throw new ApplicationException("BackAccount not found");
            }
            _logger.LogInformation($"BackAccount found {JsonSerializer.Serialize(backAccount)}");

            _logger.LogInformation($"Strart creating object for delete in database");

            backAccount.Active=false;
            backAccount.UpdatedAt = DateTime.Now;

            _logger.LogInformation($"Object BanckAccount created {JsonSerializer.Serialize(backAccount)}");
            _logger.LogInformation("Start Delete objects");

            _unitOFWork.banckAccountRepository.Delete(backAccount);

            await _unitOFWork.Commit();
            _logger.LogInformation("Object Commited Succefull");
           
        }
    }
}