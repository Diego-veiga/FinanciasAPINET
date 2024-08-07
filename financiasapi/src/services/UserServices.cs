using System.Security.Cryptography;
using AutoMapper;
using financias.src.interfaces;
using financiasapi.src.dtos;
using financiasapi.src.models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;

namespace financias.src.services
{
    public class UserServices : IUserService
    {
        private IUnitOFWork _unitOFWork;
        private ITokenService _tokenService;
        private ICachingService _cachingService;
        private IMapper _mapper;
        public UserServices(IUnitOFWork unitOFWork, ITokenService tokenService, ICachingService cachingService, IMapper mapper)
        {
            _unitOFWork = unitOFWork;
            _tokenService = tokenService;
            _tokenService = tokenService;
            _cachingService = cachingService;
            _mapper = mapper;
        }
        public async Task Create(CreateUser createUser)
        {
            var emailExist = await _unitOFWork.userRepository.GetByEmail(createUser.Email);
            if (emailExist is not null)
            {
                throw new ApplicationException("there is an user with this email");
            }

            var newUser = new User(Guid.NewGuid(), createUser.Name, createUser.Email, createUser.Password, true, DateTime.Now, DateTime.Now);

            byte[] salt = GenerateSalt([128, 8]);
            newUser.Salt = salt;
            newUser.Password = EncryptPassword(createUser.Password, salt);
            _unitOFWork.userRepository.Add(newUser);
            await _unitOFWork.Commit();
        }

        public async Task Delete(DeleteUser deleteUser)
        {

            var userExist = await _unitOFWork.userRepository.GetById(deleteUser.Id);
            if (userExist is null)
            {
                throw new ApplicationException("User not found ");
            }
            userExist.Active = false;
            _unitOFWork.userRepository.Delete(userExist);
            await _unitOFWork.Commit();
        }

        public async Task<List<UserView>> GetActive()
        {
            List<UserView> userViews = new List<UserView>();
            var users = await _unitOFWork.userRepository.Get().ToListAsync();
            foreach (var user in users)
            {
                if (user.Active == true)
                {
                    userViews.Add(new UserView() { Id = user.Id, Name = user.Name, Email = user.Email, Active = user.Active, CreatedAt = user.CreatedAt, UpdatedAt = user.UpdatedAt });
                }
            }
            return userViews;
        }

        public async Task<UserView> GetById(Guid id)
        {
            var user = await _unitOFWork.userRepository.GetById(id);
            if (user is null)
            {
                return null;
            }
            return new UserView() { Id = user.Id, Name = user.Name, Email = user.Email, Active = user.Active, CreatedAt = user.CreatedAt, UpdatedAt = user.UpdatedAt };
        }
        public async Task<UserView> GetByEmail(string email)
        {
            var user = await _unitOFWork.userRepository.GetByEmail(email);
            if (user is null)
            {
                return null;
            }
            return new UserView() { Id = user.Id, Name = user.Name, Email = user.Email, Active = user.Active, CreatedAt = user.CreatedAt, UpdatedAt = user.UpdatedAt };
        }

        public async Task<string> Login(Login login)
        {
            string token;
            token = await _cachingService.Get("token_" + login.Email);
            if (!string.IsNullOrWhiteSpace(token!))
            {
                return token;
            }
            var user = await _unitOFWork.userRepository.GetByEmail(login.Email);
            if (user is null)
            {
                throw new ApplicationException("Email or passwrod invalid");
            }
            var userView = _mapper.Map<UserView>(user);
            var comparPassword = EncryptPassword(login.Password, user.Salt);
            if (user.Password == comparPassword)
            {
                token = await _tokenService.Generate(userView);
                _cachingService.Save("token_" + login.Email, token);
                return token;
            }
            else
            {
                throw new ApplicationException("Email or passwrod invalid");
            }
        }


        public async Task Update(UpdateUser updateUser)
        {
            var userExist = await _unitOFWork.userRepository.GetById(updateUser.Id);
            if (userExist is not null && userExist.Id != updateUser.Id)
            {
                throw new ApplicationException("User not found ");
            }
            byte[] salt = GenerateSalt([128, 8]);
            var password = EncryptPassword(updateUser.Password, salt);
            userExist.Name = updateUser.Name;
            userExist.Email = updateUser.Email;
            userExist.Password = password;
            userExist.UpdatedAt = DateTime.Now;

            _unitOFWork.userRepository.Update(userExist);
            await _unitOFWork.Commit();

        }

        private Byte[] GenerateSalt(byte[] salt)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private string EncryptPassword(string senha, byte[] salt)
        {
            string senhaCriptografada = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: senha,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return senhaCriptografada;
        }


    }
}