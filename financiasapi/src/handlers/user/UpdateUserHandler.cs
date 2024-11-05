using financias.src.interfaces;
using financiasapi.src.commands.user;
using financiasapi.src.dtos;
using financiasapi.src.models;
using financiasapi.src.utils;
using MediatR;
using System.Text.Json;

namespace financiasapi.src.handlers.user;

    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand>
    {
        private IUnitOFWork _unitOFWork;
        public ILogger<UpdateUserHandler> _logger { get; set; }

        public UpdateUserHandler(IUnitOFWork unitOFWork, ILogger<UpdateUserHandler> logger)
        {
            _unitOFWork = unitOFWork;
            _logger = logger;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start UpdateUserHandler with request {JsonSerializer.Serialize(request)}");
            var userExist = await _unitOFWork.userRepository.GetById(request.Id);
            _logger.LogInformation($"User with id {JsonSerializer.Serialize(userExist)}");

            if (userExist is null)
            {
                throw new ApplicationException("User not found ");
            }

            var existUserWitThisEmail = await _unitOFWork.userRepository.GetByEmail(request.Email);
            _logger.LogInformation($"User with email {JsonSerializer.Serialize(existUserWitThisEmail)}");

            if (existUserWitThisEmail is not null && existUserWitThisEmail.Id != request.Id)
            {
                throw new ApplicationException("There is another user with this Email.");
            }

           _logger.LogInformation($"Start creating object for update in database");

            byte[] salt = EncryptHelper.GenerateSalt([128, 8]);
            var password = EncryptHelper.EncryptPassword(request.Password, salt);
            userExist.Name = request.Name;
            userExist.Email = request.Email;
            userExist.Password = password;
            userExist.UpdatedAt = DateTime.Now;

            _logger.LogInformation($"Object User created {JsonSerializer.Serialize(userExist)}");
            _logger.LogInformation("Start Add objects");

            _unitOFWork.userRepository.Update(userExist);
            await _unitOFWork.Commit();

            _logger.LogInformation("Object Committed Successful"); 
    }
}

