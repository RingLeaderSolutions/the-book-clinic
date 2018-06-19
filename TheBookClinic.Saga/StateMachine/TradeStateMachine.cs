using System;
using Automatonymous;
using MassTransit;
using Serilog;
using TheBookClinic.Messaging.Events;
using TheBookClinic.Persistence.Model;
using TheBookClinic.StateSaga.Commands;

namespace TheBookClinic.StateSaga.StateMachine
{
    public class TradeStateMachine : MassTransitStateMachine<PersistedTrade>
    {
        public State RetrievingCrmData { get; private set; }

        public State RetrievingPricingData { get; private set; }

        public State Completed { get; private set; }


        public Event<INewTradeReceived> NewTradeReceived { get; private set; }

        public Event<ICrmDataReceivedEvent> CrmDataReceived { get; private set; }

        public Event<IPriceReceivedEvent> PricingDataReceived { get; private set; }


        public TradeStateMachine()
        {
            InstanceState(s => s.CurrentState);


            Event(() => NewTradeReceived,
                x =>
                {
                    x.CorrelateBy(sagaState => sagaState.TradeId, context => context.Message.TradeId);
                    x.InsertOnInitial = true;
                    x.SetSagaFactory(context => new PersistedTrade
                    {
                        CorrelationId = NewId.NextGuid(),
                        TradeId = context.Message.TradeId
                    });
                    x.SelectId(context => NewId.NextGuid());
                });

            Event(() => CrmDataReceived,
                x => x.CorrelateBy(sagaState => sagaState.TradeId, context => context.Message.TradeId));

            Event(() => PricingDataReceived,
                x => x.CorrelateBy(sagaState => sagaState.TradeId, context => context.Message.TradeId));


            Initially(
                When(NewTradeReceived)
                    .Then(
                        context =>
                            Log.Information("[TradeStateMachine] New Trade: Id=[{CorrelationId}] TradeId=[{TradeId}]",
                                context.Instance.CorrelationId,
                                context.Data.TradeId))
                    .Send((state, command) => new Uri(StateSagaConfiguration.CrmDataEndpoint),
                        context => new CrmDataRequestedCommand(context.Data.TradeId))
                    .TransitionTo(RetrievingCrmData));


            During(RetrievingCrmData,
                When(CrmDataReceived)
                    .Then(
                        context =>
                            Log.Information(
                                "[TradeStateMachine] CRM data received for Trade: Id=[{CorrelationId}] TradeId=[{TradeId}]",
                                context.Instance.CorrelationId,
                                context.Data.TradeId))
                    .Send((state, command) => new Uri(StateSagaConfiguration.PricingDataEndpoint),
                        context => new PriceRequestedCommand(context.Data.TradeId))
                    .TransitionTo(RetrievingPricingData));


            During(RetrievingPricingData,
                When(PricingDataReceived)
                    .Then(
                        context =>
                            Log.Information(
                                "[TradeStateMachine] Pricing data received for Trade: Id=[{CorrelationId}] TradeId=[{TradeId}]",
                                context.Instance.CorrelationId,
                                context.Data.TradeId))
                    .TransitionTo(Completed));


            During(Completed,
                When(NewTradeReceived)
                    .Then(
                        context =>
                            Log.Information("[TradeStateMachine] Existing Trade: Id=[{CorrelationId}] TradeId=[{TradeId}]",
                                context.Instance.CorrelationId,
                                context.Data.TradeId))
                    .Send((state, command) => new Uri(StateSagaConfiguration.CrmDataEndpoint),
                        context => new CrmDataRequestedCommand(context.Data.TradeId))
                    .TransitionTo(RetrievingCrmData));
        }
    }
}