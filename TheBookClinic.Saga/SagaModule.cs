using Autofac;

namespace TheBookClinic.StateSaga
{
    public class SagaModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SagaStateService>();
        }
    }
}