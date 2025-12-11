using Xunit;
using RecommendationSystem.Services;
using FluentAssertions;

namespace RecommendationSystem.Tests.Services;

public class RecommendationEngineTests
{
    [Fact]
    public void IsRecommendationBetweenZeroAndOne()
    {
        var engine = new RecommendationEngine();
        
        var result = engine.GenerateRecommendation();

        result.Should().BeInRange(0, 1);
    }
}
