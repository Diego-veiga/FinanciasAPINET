
using financias.src.commands.BanckAccount;
using financias.src.interfaces;
using financias.src.models;
using financias.src.models.Enums;
using MediatR;

namespace financias.src.handlers
{
    public class CreateBanckAccountHandler : IRequestHandler<CreateBanckAcconutCommand>
    {
         private IUnitOFWork _unitOFWork;

         public CreateBanckAccountHandler(IUnitOFWork unitOFWork)
         {
            _unitOFWork =unitOFWork;
            
         }
        

        public async Task Handle(CreateBanckAcconutCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOFWork.userRepository.GetById(request.UserId);
            if(user is null){
                throw new ApplicationException("User not found");
            }

            var banck = await _unitOFWork.banckRepository.GetById(request.BanckId);
            if(banck is null){
                throw new ApplicationException("Banck not found");
               
            }
           

            var banckAccount = new BanckAccount(Guid.NewGuid(),request.Name,(AccountType)Enum.Parse(typeof(AccountType), request.Type),request.BanckId,true,DateTime.Now, DateTime.Now);
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

             _unitOFWork.banckAccountRepository.Add(banckAccount);
             _unitOFWork.userBancksAccountsRepository.Add(userBanckAccount);
             
             
            await _unitOFWork.Commit();

           
        }
    }
}