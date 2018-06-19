using System;
using System.Threading.Tasks;
using System.Web.Http;
using MassTransit;
using TheBookClinic.Messaging.Events;
using TheBookClinic.Messaging.MassTransit;
using TheBookClinic.TradeManager.Events;

namespace TheBookClinic.TradeManager.Controllers
{
    [RoutePrefix("api/trade")]
    public class TradeController : ApiController
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public TradeController(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        [Route]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok();
        }

        [Route]
        [HttpPost]
        public async Task<IHttpActionResult> NewTradeReceived([FromBody]NewTradeReceived newTradeReceived)
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(RabbitMqConnectionInformation.TradeSagaEndpoint));
            await endpoint.Send<INewTradeReceived>(new NewTradeReceived(newTradeReceived.TradeId));

            return Ok();
        }
    }
}