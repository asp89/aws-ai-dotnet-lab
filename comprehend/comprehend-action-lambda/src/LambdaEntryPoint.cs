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

        if (string.IsNullOrEmpty(requestBodyModel.Input))
        {
            return "Input text cannot be empty";
        }

        AwsComprehend comprehend = new AwsComprehend();
        string result = string.Empty;

        switch (requestBodyModel.ActionType.ToLower())
        {
            case ActionTypeConstants.DetectDominantLanguage:
                result = JsonSerializer.Serialize(await comprehend.DetectDominantLanguageAsync(requestBodyModel.Input));
                break;

            case ActionTypeConstants.DetectEntities:
                result = JsonSerializer.Serialize(await comprehend.DetectEntitiesAsync(requestBodyModel.Input, requestBodyModel.LanguageCode));
                break;

            case ActionTypeConstants.DetectKeyPhrases:
                result = JsonSerializer.Serialize(await comprehend.DetectKeyPhrasesAsync(requestBodyModel.Input, requestBodyModel.LanguageCode));
                break;

            case ActionTypeConstants.DetectPiiEntities:
                result = JsonSerializer.Serialize(await comprehend.DetectPiiAsync(requestBodyModel.Input, requestBodyModel.LanguageCode));
                break;

            case ActionTypeConstants.DetectSentiment:
                result = JsonSerializer.Serialize(await comprehend.DetectSentimentAsync(requestBodyModel.Input, requestBodyModel.LanguageCode));
                break;

            case ActionTypeConstants.DetectSyntaxAnalysis:
                result = JsonSerializer.Serialize(await comprehend.DetectSyntaxAsync(requestBodyModel.Input, requestBodyModel.LanguageCode));
                break;

            default:
                result = "Invalid action type specified.";
                break;
        }

        _logger.LogInformation("Lambda Function Completed");
        return result;
    }
}
