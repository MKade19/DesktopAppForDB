using Microsoft.Data.SqlClient;

namespace BusStation.API.Data
{
    internal class BusStationDatabase
    {
        private static BusStationDatabase? _instance;
        public SqlConnection Connection { get; }
        public static BusStationDatabase Instance 
        { 
            get
            {
                
                if (_instance == null)
                {
                    _instance = new BusStationDatabase();
                }

                return _instance;
            }
        }
        private BusStationDatabase()
        {
            Connection = new SqlConnection("Server=.\\MKMSSQLSERVER;Database=Bus_station;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
