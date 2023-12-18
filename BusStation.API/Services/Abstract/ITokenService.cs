namespace BusStation.API.Services.Abstract
{
    public interface ITokenService
    {
        public string GetToken(object payload);
        public void ValidateToken(string token);
    }
}
