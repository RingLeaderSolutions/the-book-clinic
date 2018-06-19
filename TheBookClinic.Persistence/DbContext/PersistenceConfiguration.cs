using System.Configuration;

namespace TheBookClinic.Persistence.DbContext
{
    public class PersistenceConfiguration
    {
        public static string ConnectionString { get; } = ConfigurationManager.AppSettings["sqlConnectionString"];
    }
}