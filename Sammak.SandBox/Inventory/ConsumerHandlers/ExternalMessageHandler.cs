using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sammak.SandBox.Common;
using Sammak.SandBox.Inventory.QueueMessaging.Consumer;
using Sammak.SandBox.Models;

namespace Sammak.SandBox.Inventory.ConsumerHandlers
{
    public class ExternalMessageHandler : QueueHandler
    {
        private readonly ILogger _logger;
        private readonly IExternalMessageEngine _externalMessageEngine;

        public override string QueueName => Constants.QueueNames.ExternalMessage;

        public ExternalMessageHandler(ILogger logger, IExternalMessageEngine externalMessageEngine) : base(logger)
        {
            _logger = logger;
            _externalMessageEngine = externalMessageEngine;
        }

        public override void Handle(string body)
        {
            //using (new DebugLog(_logger, GetType().Name, nameof(Handle), new { body }))
            //{
                var payload = JsonConvert.DeserializeObject<ExternalMessagePayload>(body);

                _externalMessageEngine.Process(payload);
            //}
        }

    }
}
