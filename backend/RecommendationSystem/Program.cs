using RecommendationSystem.Services;
using Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddScoped<RecommendationEngine>();
builder.Services.AddCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors(builder => 
    builder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());

app.MapGet("/recommendation", (RecommendationEngine engine) => new  Recommendation(engine.GenerateRecommendation()))
.WithName("Recommendation");

app.Run();
