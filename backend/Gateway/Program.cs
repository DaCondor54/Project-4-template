using Shared;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.UseHttpClientMetrics();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();
builder.Services.AddHttpClient(); 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMetricServer();
app.UseHttpMetrics();

app.UseHttpsRedirection();
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapGet("/flem", async (HttpClient httpClient, ILogger<Program> logger) =>
{
    string host = Environment.GetEnvironmentVariable("RECOMMENDATION_SYSTEM_URL") ?? "resys";
    try 
    {
        var recommendation = await httpClient.GetFromJsonAsync<Recommendation>($"http://{host}:4000/recommendation");
        string flem = recommendation is null ? "" : $" at {recommendation.Score}";

        logger.LogInformation("Yassir's flem is at {score}", recommendation?.Score);

        return new Flem($"Yassir === Flem${flem}");
    } 
    catch(Exception e)
    {
        logger.LogWarning("Error: {error}", e.Message);
        return new Flem("Yassir === Flem ???");
    }

} ).WithName("GetYassFlem");

app.Run();


record Flem(string Reason);
