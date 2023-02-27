## About

Lambda function written in .NET 6 that publishes events to AWS EventBridge.

## Installation

To test the Lambda function, you can deploy it or run locally with Mock Lambda Test Tool. 

To deploy it, run the following command:

```
cd Lambda-EventBridge-Consumer\LambdaConsumer
dotnet lambda deploy-function
```

To install Mock Lambda Test Tool, please take a look at [this link](https://github.com/aws/aws-lambda-dotnet).
