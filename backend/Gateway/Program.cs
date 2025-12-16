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

app.MapGet("/flem", getFlems);
app.MapPost("/flem", postRequest);

app.MapGet("/averageflem", getAverage);


app.Run();

async Task<Flem> getAverage(HttpClient httpClient, ILogger<Program> logger) 
{
    string host = Environment.GetEnvironmentVariable("RECOMMENDATION_SYSTEM_URL") ?? "resys";
    try 
    {
        return (await httpClient.GetFromJsonAsync<Flem>($"http://{host}:4000/averageflem")) ?? new (0);
    } 
    catch(Exception e)
    {
        logger.LogWarning("Error: {error}", e.Message);
        return new (0);
    }
}

async Task<List<Flem>> getFlems(HttpClient httpClient, ILogger<Program> logger)
{
    string host = Environment.GetEnvironmentVariable("RECOMMENDATION_SYSTEM_URL") ?? "resys";
    try 
    {
        return (await httpClient.GetFromJsonAsync<List<Flem>>($"http://{host}:4000/flem")) ?? [];
    } 
    catch(Exception e)
    {
        logger.LogWarning("Error: {error}", e.Message);
        return [];
    }
}

async Task<Flem> postRequest(HttpClient httpClient, ILogger<Program> logger)
{
    string host = Environment.GetEnvironmentVariable("RECOMMENDATION_SYSTEM_URL") ?? "resys";
    try 
    {
        var result = await httpClient.PostAsync($"http://{host}:4000/flem", null);
        result.EnsureSuccessStatusCode();
        return (await  result.Content.ReadFromJsonAsync<Flem>()) ?? new (0);
    } 
    catch(Exception e)
    {
        logger.LogWarning("Error: {error}", e.Message);
        return new (0);
    }
}