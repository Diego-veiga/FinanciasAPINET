
using System.Text.Json;
using financias.src.commands.BanckAccount;
using financias.src.interfaces;
using financiasapi.src.models;
using financiasapi.src.models.Enums;
using MediatR;

namespace financias.src.handlers
{
    public class CreateBanckAccountHandler : IRequestHandler<CreateBanckAcconutCommand>
    {
         private IUnitOFWork _unitOFWork;
         public ILogger<CreateBanckAccountHandler> _logger { get; set; }

         public CreateBanckAccountHandler(IUnitOFWork unitOFWork,ILogger<CreateBanckAccountHandler> logger)
         {
            _unitOFWork =unitOFWork;
            _logger = logger;
         }
    
        public async Task Handle(CreateBanckAcconutCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start CreateBanckAccountHandler with request {JsonSerializer.Serialize(request)}");
            _logger.LogInformation($"Handle invoked  with request {JsonSerializer.Serialize(request)}");
            var user = await _unitOFWork.userRepository.GetById(request.UserId);

           _logger.LogInformation($"User found {JsonSerializer.Serialize(user)}");

            if(user is null){
                throw new ApplicationException("User not found");
            }

            var banck = await _unitOFWork.banckRepository.GetById(request.BanckId);

             _logger.LogInformation($"Banck found {JsonSerializer.Serialize(banck)}");

            if(banck is null){
                throw new ApplicationException("Banck not found");
               
            }
            
            _logger.LogInformation($"Strart creating object for insert in database");

            var banckAccount = new BanckAccount(Guid.NewGuid(),request.Name,(AccountType)Enum.Parse(typeof(AccountType), request.Type),request.BanckId,true,DateTime.Now, DateTime.Now);
           
           _logger.LogInformation($"Object BanckAccount created {JsonSerializer.Serialize(banckAccount)}");

            var userBanckAccount = new UserBancksAccounts()
            {
                Id=Guid.NewGuid(),
                BanckAccountId = banckAccount.Id,
                UserId=request.UserId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsAdmin=true,
                Active=true
            };
            
             _logger.LogInformation($"Object UserBanckAccount created {JsonSerializer.Serialize(userBanckAccount)}");
             _logger.LogInformation("Start Add objects");

             _unitOFWork.banckAccountRepository.Add(banckAccount);
             _unitOFWork.userBancksAccountsRepository.Add(userBanckAccount);
             
             
            await _unitOFWork.Commit();
            _logger.LogInformation("Object Commited Succefull");
           
        }
    }
}