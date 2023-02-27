using Amazon.EventBridge;
using Amazon.EventBridge.Model;
using LambdaEventBridgeProducer.Events;
using LambdaEventBridgeProducer.Interfaces;
using System.Text.Json;

namespace LambdaEventBridgeProducer.Providers
{
    public class EventBridgeProvider : IEventBridgeProvider
    {
        private readonly IAmazonEventBridge _amazonEventBridgeClient;

        public EventBridgeProvider(IAmazonEventBridge amazonEventBridgeClient)
        {
            _amazonEventBridgeClient = amazonEventBridgeClient;
        }

        public async Task PublishEvents()
        {
            PutEventsRequestEntry message = new()
            {
                Detail = JsonSerializer.Serialize(GenerateEvent()),
                DetailType = nameof(TestEvent),
                EventBusName = "default",
                Source = "SDK for dotnet"
            };

            PutEventsRequest request = new()
            {
                Entries = new() { message },
            };

            PutEventsResponse response = await _amazonEventBridgeClient.PutEventsAsync(request);
        }

        public TestEvent GenerateEvent() => new TestEvent { Id = 1, Message = "Hello EventBridge" };

    }
}
