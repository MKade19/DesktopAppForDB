using BusStation.API.Data.Abstract;
using BusStation.API.Exceptions;
using BusStation.API.Services.Abstract;
using BusStation.Common.Models;

namespace BusStation.API.Services
{
    public class AuthService : IAuthService
    {
        private IUserRepository UserRepository { get; }
        private IHashService HashService { get; }
        private ITokenService TokenService { get; }
        
        public AuthService(IUserRepository userRepository, IHashService hashService, ITokenService tokenService)
        {
            UserRepository = userRepository;
            HashService = hashService;
            TokenService = tokenService;
        }

        public async Task<AuthData> Login(User user)
        {
            User userFromDB = await UserRepository.GetUserByUsernameAsync(user.Username);

            if (userFromDB.Id == -1) 
            {
                throw new NotFoundException("Данного пользователя не существует!");
            }
            
            if (!HashService.VerifyHash(user.Password, userFromDB.Password, userFromDB.Salt))
            {
                throw new BadRequestException("Неверный пароль!");
            }

            AuthData authData = new AuthData(TokenService.GetToken(userFromDB), userFromDB.Role);

            return authData;
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public async Task Register(User user)
        {
            user.Password = HashService.GetHash(user.Password, out byte[] salt);
            user.Salt = salt;

            await UserRepository.CreateOne(user);
        }
    }
}
