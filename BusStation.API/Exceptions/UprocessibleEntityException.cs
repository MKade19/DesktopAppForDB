namespace BusStation.API.Exceptions
{
    public class UprocessibleEntityException : Exception
    {
        public UprocessibleEntityException(string message) : base(message) { }
    }
}
