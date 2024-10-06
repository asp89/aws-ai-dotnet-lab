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
    /// A function that takes a string and detects dominant language.
    /// </summary>
    public async Task<DetectDominantLanguageResponse?> DetectDominantLanguageAsync(string input)
    {
        try
        {
            _logger.LogInformation("Detecting dominant language for input text.");
            DetectDominantLanguageRequest detectDominantLanguageRequest = new()
            {
                Text = input
            };

            DetectDominantLanguageResponse detectDominantLanguageResponse = await _client.DetectDominantLanguageAsync(detectDominantLanguageRequest);
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
    /// A function that takes a string and language code to detect entities.
    /// </summary>
    public async Task<DetectEntitiesResponse?> DetectEntitiesAsync(string input, string languageCode)
    {
        try
        {
            _logger.LogInformation("Detecting dominant language for input text.");
            DetectEntitiesRequest detectEntitiesRequest = new()
            {
                Text = input,
                LanguageCode = string.IsNullOrEmpty(languageCode) ? "en" : languageCode.ToLowerInvariant()
            };

            DetectEntitiesResponse detectEntitiesResponse = await _client.DetectEntitiesAsync(detectEntitiesRequest);
            _logger.LogInformation("Language detection completed.");

            return detectEntitiesResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while detecting entities.");
            return null;
        }
    }

    /// <summary>
    /// A function that takes a string and language code to detect key phrases.
    /// </summary>
    public async Task<DetectKeyPhrasesResponse?> DetectKeyPhrasesAsync(string input, string languageCode)
    {
        try
        {
            _logger.LogInformation("Detecting key phrases for input text.");
            DetectKeyPhrasesRequest detectKeyPhrasesRequest = new DetectKeyPhrasesRequest()
            {
                Text = input,
                LanguageCode = string.IsNullOrEmpty(languageCode) ? "en" : languageCode.ToLowerInvariant()
            };

            DetectKeyPhrasesResponse detectKeyPhrasesResponse = await _client.DetectKeyPhrasesAsync(detectKeyPhrasesRequest);
            _logger.LogInformation("Key Phrases detection completed.");

            return detectKeyPhrasesResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while detecting key phrases.");
            return null;
        }
    }

    /// <summary>
    /// A function that takes a string and language code to detect PII.
    /// </summary>
    public async Task<DetectPiiEntitiesResponse?> DetectPiiAsync(string input, string languageCode)
    {
        try
        {
            _logger.LogInformation("Detecting PII for input text.");
            DetectPiiEntitiesRequest detectPiiEntitiesRequest = new()
            {
                Text = input,
                LanguageCode = string.IsNullOrEmpty(languageCode) ? "en" : languageCode.ToLowerInvariant()
            };
            DetectPiiEntitiesResponse response = await _client.DetectPiiEntitiesAsync(detectPiiEntitiesRequest);
            _logger.LogInformation("PII detection completed.");
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while detecting PII.");
            return null;
        }
    }

    /// <summary>
    /// A function that takes string and language to detect sentiment.
    /// </summary>
    public async Task<DetectSentimentResponse?> DetectSentimentAsync(string input, string languageCode)
    {
        try
        {
            _logger.LogInformation("Detecting sentiments for input text");
            DetectSentimentRequest detectSentimentRequest = new()
            {
                Text = input,
                LanguageCode = string.IsNullOrEmpty(languageCode) ? "en" : languageCode.ToLowerInvariant()
            };

            DetectSentimentResponse detectSentimentResponse = await _client.DetectSentimentAsync(detectSentimentRequest);
            _logger.LogInformation("Sentiment detection completed.");

            return detectSentimentResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while detecting sentiments.");
            return null;
        }
    }

    /// <summary>
    /// A function that takes string and language to detect syntax.
    /// </summary>
    public async Task<DetectSyntaxResponse?> DetectSyntaxAsync(string input, string languageCode)
    {
        try
        {
            _logger.LogInformation("Detecting syntax for input text");
            DetectSyntaxRequest detectSyntaxRequest = new()
            {
                Text = input,
                LanguageCode = string.IsNullOrEmpty(languageCode) ? "en" : languageCode.ToLowerInvariant()
            };

            DetectSyntaxResponse detectSyntaxResponse = await _client.DetectSyntaxAsync(detectSyntaxRequest);
            _logger.LogInformation("Sentiment detection completed.");

            return detectSyntaxResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while detecting sentiments.");
            return null;
        }
    }
}