
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

         public DeleteBanckAccountHandler(IUnitOFWork unitOFWork)
         {
            _unitOFWork =unitOFWork;
            
         }
        

        public async Task Handle(DeleteBanckAcconutCommand request, CancellationToken cancellationToken)
        {
            var backAccount = await _unitOFWork.banckAccountRepository.GetById(request.Id);
            if(backAccount is null){
                throw new ApplicationException("BackAccount not found");
            }
            backAccount.Active=false;
            backAccount.UpdatedAt = DateTime.Now;

             _unitOFWork.banckAccountRepository.Delete(backAccount);
                         
             
            await _unitOFWork.Commit();

           
        }
    }
}