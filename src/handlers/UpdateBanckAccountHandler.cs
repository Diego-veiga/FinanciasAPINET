
using financias.src.commands.BanckAccount;
using financias.src.interfaces;
using financias.src.models;
using financias.src.models.Enums;
using MediatR;

namespace financias.src.handlers
{
    public class UpdateBanckAccountHandler : IRequestHandler<UpdateBanckAcconutCommand>
    {
         private IUnitOFWork _unitOFWork;

         public UpdateBanckAccountHandler(IUnitOFWork unitOFWork)
         {
            _unitOFWork =unitOFWork;
            
         }
        

        public async Task Handle(UpdateBanckAcconutCommand request, CancellationToken cancellationToken)
        {
            var backAccount = await _unitOFWork.banckAccountRepository.GetById(request.Id);
            if(backAccount is null){
                throw new ApplicationException("BackAccount not found");
            }
            

            var banck = await _unitOFWork.banckRepository.GetById(request.BanckId);
            if(banck is null){
                throw new ApplicationException("Banck not found");
            }
            
            backAccount.Name = request.Name;
            backAccount.Type =(AccountType)Enum.Parse(typeof(AccountType), request.Type);
            backAccount.BanckId = request.BanckId;
        

             _unitOFWork.banckAccountRepository.Update(backAccount);
                         
             
            await _unitOFWork.Commit();

           
        }
    }
}