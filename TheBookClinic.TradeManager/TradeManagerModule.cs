using Autofac;
using TheBookClinic.TradeManager.Services;

namespace TheBookClinic.TradeManager
{
    public class TradeManagerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TradeService>();
        }
    }
}