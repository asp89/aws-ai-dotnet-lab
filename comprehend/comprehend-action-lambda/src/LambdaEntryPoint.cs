using System.Text.Json;
using Amazon.Lambda.Core;
using comprehendActionLambda;
using Microsoft.Extensions.Logging;
using Models;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace comprehend_action_lambda;

public class LambdaEntryPoint
{
    private readonly ILogger<LambdaEntryPoint> _logger;
    public LambdaEntryPoint()
    {
        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        _logger = loggerFactory.CreateLogger<LambdaEntryPoint>();
    }


    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="requestBodyModel">The string value for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    public async Task<string> FunctionHandler(ComprehendInput requestBodyModel, ILambdaContext context)
    {
        _logger.LogInformation("Lambda Function Started");
        var comprehend = new AwsComprehend();
        switch (requestBodyModel.ActionType.ToLower())
        {
            case ActionTypeConstants.DetectDominantLanguage:
                return JsonSerializer.Serialize(await comprehend.DetectDominantLanguageAsync(requestBodyModel.Input));

            case ActionTypeConstants.DetectEntities:
                return JsonSerializer.Serialize(await comprehend.DetectEntitiesAsync(requestBodyModel.Input, requestBodyModel.LanguageCode));

            case ActionTypeConstants.DetectKeyPhrases:
                return JsonSerializer.Serialize(await comprehend.DetectKeyPhrasesAsync(requestBodyModel.Input, requestBodyModel.LanguageCode));

            case ActionTypeConstants.DetectPiiEntities:
                return JsonSerializer.Serialize(await comprehend.DetectPiiAsync(requestBodyModel.Input, requestBodyModel.LanguageCode));

            case ActionTypeConstants.DetectSentiment:
                return JsonSerializer.Serialize(await comprehend.DetectSentimentAsync(requestBodyModel.Input, requestBodyModel.LanguageCode));

            case ActionTypeConstants.DetectSyntaxAnalysis:
                return JsonSerializer.Serialize(await comprehend.DetectSyntaxAsync(requestBodyModel.Input, requestBodyModel.LanguageCode));

            default:
                return "Invalid action type specified.";
        }
    }
}
