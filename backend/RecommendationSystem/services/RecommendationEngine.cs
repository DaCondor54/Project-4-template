namespace RecommendationSystem.Services;

public class RecommendationEngine
{
    public double GenerateRecommendation() => Math.Round(new Random().NextDouble(), 2);
}