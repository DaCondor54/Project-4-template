using Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();
builder.Services.AddHttpClient(); 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapGet("/flem", async (HttpClient httpClient) =>
{
    string host = Environment.GetEnvironmentVariable("RECOMMENDATION_SYSTEM_URL") ?? "resys";
    var recommendation = await httpClient.GetFromJsonAsync<Recommendation>($"http://{host}:4000/recommendation");
    string flem = recommendation is null ? "" : $" at {recommendation.Score}";
    return new Flem($"Yassir === Flem${flem}");
} ).WithName("GetYassFlem");

app.Run();


record Flem(string Reason);
