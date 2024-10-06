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

    /// <summary>
    /// A simple function that takes a string and detects dominant language.
    /// </summary>
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

    /// <summary>
    /// A simple function that takes a string and language code to detect entities.
    /// </summary>
    public async Task<DetectEntitiesResponse?> DetectEntitiesAsync(string input, string languageCode)
    {
        try
        {
            _logger.LogInformation("Detecting dominant language for input text.");
            var request = new DetectEntitiesRequest()
            {
                Text = input,
                LanguageCode = string.IsNullOrEmpty(languageCode) ? "en" : languageCode.ToLowerInvariant()
            };
            DetectEntitiesResponse response = await _client.DetectEntitiesAsync(request);
            _logger.LogInformation("Language detection completed.");
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while detecting entities.");
            return null;
        }
    }

    /// <summary>
    /// A simple function that takes a string and language code to detect key phrases.
    /// </summary>
    public async Task<DetectKeyPhrasesResponse?> DetectKeyPhrasesAsync(string input, string languageCode)
    {
        try
        {
            _logger.LogInformation("Detecting key phrases for input text.");
            var request = new DetectKeyPhrasesRequest()
            {
                Text = input,
                LanguageCode = string.IsNullOrEmpty(languageCode) ? "en" : languageCode.ToLowerInvariant()
            };
            DetectKeyPhrasesResponse response = await _client.DetectKeyPhrasesAsync(request);
            _logger.LogInformation("Key Phrases detection completed.");
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while detecting key phrases.");
            return null;
        }
    }

    /// <summary>
    /// A simple function that takes a string and language code to detect PII.
    /// </summary>
    public async Task<DetectPiiEntitiesResponse?> DetectPiiAsync(string input, string languageCode)
    {
        try
        {
            _logger.LogInformation("Detecting PII for input text.");
            var request = new DetectPiiEntitiesRequest()
            {
                Text = input,
                LanguageCode = string.IsNullOrEmpty(languageCode) ? "en" : languageCode.ToLowerInvariant()
            };
            DetectPiiEntitiesResponse response = await _client.DetectPiiEntitiesAsync(request);
            _logger.LogInformation("PII detection completed.");
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while detecting PII.");
            return null;
        }
    }
}