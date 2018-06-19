using MassTransit.EntityFrameworkIntegration;
using TheBookClinic.Persistence.Model;

namespace TheBookClinic.Persistence.DbContext
{
    public class PersistedTradeMap : SagaClassMapping<PersistedTrade>
    {
        public PersistedTradeMap()
        {
            HasKey(x => x.CorrelationId);

            Property(x => x.TradeId);
            Property(x => x.CurrentState);
        }
    }
}