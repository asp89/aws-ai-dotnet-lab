using Xunit;
using Amazon.Lambda.TestUtilities;
using Models;
using Amazon.Comprehend.Model;

namespace comprehend_action_lambda.Tests;

public class FunctionTest
{
    [Fact]
    public async void TestDominantLanguage()
    {
        var function = new LambdaEntryPoint();
        var context = new TestLambdaContext();
        ComprehendInput inputModel = new()
        {
            Input = "It is raining today in Seattle.",
            ActionType = "detectdominantlanguage"
        };
        var response = await function.FunctionHandler(inputModel, context);

        Assert.NotEmpty(response);
        Assert.NotNull(response);
    }
}
