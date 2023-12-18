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

        public async Task<Token> Login(User user)
        {
            User userFromDB = await UserRepository.GetUserByUsername(user.Username);

            if (userFromDB.Id == -1) 
            {
                throw new NotFoundException("Incorrect username!");
            }
            
            if (!HashService.VerifyHash(user.Password, userFromDB.Password, userFromDB.Salt))
            {
                throw new BadRequestException("Incorrect password!");
            }

            Token token = new Token(TokenService.GetToken(userFromDB));

            return token;
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
