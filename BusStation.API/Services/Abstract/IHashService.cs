namespace BusStation.API.Services.Abstract
{
    public interface IHashService
    {
        public string GetHash(string stringToHash, out byte[] salt);
        public bool VerifyHash(string password, string hash, byte[] salt);
    }
}