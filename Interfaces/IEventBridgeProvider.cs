namespace LambdaEventBridgeProducer.Interfaces
{
    public interface IEventBridgeProvider
    {
        Task PublishEvents();
    }
}
