using MongoDB.Bson;
using Shared;

namespace RecommendationSystem.Data.Mongo;

public record FlemDocument (
    ObjectId Id,
    double FlemRate
) : Flem(FlemRate);