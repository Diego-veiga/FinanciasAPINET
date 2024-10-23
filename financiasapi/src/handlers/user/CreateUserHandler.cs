using financias.src.interfaces;
using financiasapi.src.commands.user;
using financiasapi.src.models;
using financiasapi.src.utils;
using MediatR;
using System.Text.Json;

namespace financiasapi.src.handlers.user;

    public class CreateUserHandler : IRequestHandler<CreateUserCommand>
    {
        private IUnitOFWork _unitOFWork;
        public ILogger<CreateUserHandler> _logger { get; set; }

        public CreateUserHandler(IUnitOFWork unitOFWork, ILogger<CreateUserHandler> logger)
        {
            _unitOFWork = unitOFWork;
            _logger = logger;
        }
        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
           _logger.LogInformation($"Start CreateUserHandler with request {JsonSerializer.Serialize(request)}");
           _logger.LogInformation($"Handle invoked  with request {JsonSerializer.Serialize(request)}");
           var emailExist = await _unitOFWork.userRepository.GetByEmail(request.Email);
           
          _logger.LogInformation($"User found {JsonSerializer.Serialize(emailExist)}");
        
          if (emailExist is not null)
          {
              throw new ApplicationException("There is an user with this email");
          }

         _logger.LogInformation($"Start creating object for insert in database");

          var newUser = new User(Guid.NewGuid(), request.Name, request.Email, request.Password, true, DateTime.Now, DateTime.Now);
          
          _logger.LogInformation($"Object User created {JsonSerializer.Serialize(newUser)}");

          byte[] salt = EncryptHelper.GenerateSalt([128, 8]);
           newUser.Salt = salt;
           newUser.Password = EncryptHelper.EncryptPassword(request.Password, salt);
         _logger.LogInformation("Start Add objects");
         _unitOFWork.userRepository.Add(newUser);
           await _unitOFWork.Commit();
         _logger.LogInformation("Object Committed Successful");
        }
    }

