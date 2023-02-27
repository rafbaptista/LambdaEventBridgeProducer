using Amazon.EventBridge;
using Amazon.Lambda.Core;
using LambdaEventBridgeProducer.Interfaces;
using LambdaEventBridgeProducer.Providers;
using Microsoft.Extensions.DependencyInjection;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LambdaEventBridgeProducer;

public class Function
{
    private IServiceProvider _serviceProvider;

    public Function()
    {
        ConfigureDependencyInjection();
    }

    public async Task FunctionHandler(ILambdaContext context)
    {
        context.Logger.LogInformation("Starting EventBridge Lambda Producer");

        var eventBridgeProvider = GetEventBridgeProvider();

        try
        {
            await eventBridgeProvider.PublishEvents();
            context.Logger.LogInformation("Events successfuly published to AWS EventBridge");
        }
        catch (Exception ex)
        {
            context.Logger.LogError(ex.Message);
        }

    }

    public void ConfigureDependencyInjection()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddAWSService<IAmazonEventBridge>();
        serviceCollection.AddTransient<IEventBridgeProvider, EventBridgeProvider>();

        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    private IEventBridgeProvider GetEventBridgeProvider() => _serviceProvider.GetRequiredService<IEventBridgeProvider>();

}
