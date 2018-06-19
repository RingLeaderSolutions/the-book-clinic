using System.Configuration;

namespace TheBookClinic.StateSaga.StateMachine
{
    public class StateSagaConfiguration
    {
        public static string CrmDataEndpoint { get; } = ConfigurationManager.AppSettings["rabbitmq_crmDataEndpoint"];

        public static string PricingDataEndpoint { get; } = ConfigurationManager.AppSettings["rabbitmq_pricingDataEndpoint"];
    }
}