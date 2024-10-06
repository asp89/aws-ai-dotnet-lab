using Amazon.Comprehend;
using Amazon.Comprehend.Model;
using Microsoft.Extensions.Logging;
using Models;
namespace comprehendActionLambda;

public class AwsComprehend
{
    private readonly AmazonComprehendClient _client;
    private readonly ILogger<AwsComprehend> _logger;

    public AwsComprehend()
    {
        _client = new AmazonComprehendClient();

        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        _logger = loggerFactory.CreateLogger<AwsComprehend>();
    }

    public async Task<DetectDominantLanguageResponse?> DetectDominantLanguageAsync(string input)
    {
        try
        {
            _logger.LogInformation("Detecting dominant language for input text.");

            var request = new DetectDominantLanguageRequest()
            {
                Text = input
            };
            DetectDominantLanguageResponse detectDominantLanguageResponse = await _client.DetectDominantLanguageAsync(request);
            _logger.LogInformation("Language detection completed.");
            return detectDominantLanguageResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while detecting dominant language.");
            return null;
        }
    }

    public async Task<DetectEntitiesResponse?> DetectEntitiesAsync(string input, string languageCode = "EN")
    {
        DetectEntitiesResponse response = new();
        try
        {
            _logger.LogInformation("Detecting dominant language for input text.");
            var request = new DetectEntitiesRequest()
            {
                Text = input,
                LanguageCode = languageCode.ToLowerInvariant()
            };
            response = await _client.DetectEntitiesAsync(request);
            _logger.LogInformation("Language detection completed.");
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while detecting entities.");
            return null;
        }
    }
}