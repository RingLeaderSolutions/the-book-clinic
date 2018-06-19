using Autofac;

namespace TheBookClinic.PricingService
{
    internal class PricingServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PricingService>();
        }
    }
}