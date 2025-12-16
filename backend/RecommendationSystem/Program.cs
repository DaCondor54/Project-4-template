using MongoDB.Driver;
using RecommendationSystem.Services;
using RecommendationSystem.Data.Mongo;
using Shared;
using MongoDB.Bson;
using MongoDB.Driver.Linq;

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

const string FLEMS_COLLECTION = "flems";

app.MapPost("/flem", async (RecommendationEngine engine) =>
{
    var flem = new  Flem(engine.GenerateRecommendation());
    
    var flems = GetCollection<FlemDocument>(FLEMS_COLLECTION);

    await flems.InsertOneAsync(new FlemDocument(ObjectId.GenerateNewId() , flem.FlemRate));

    return flem;

})
.WithName("AddFlem");

app.MapGet("/averageflem", () =>
{
    var flems = GetCollection<FlemDocument>(FLEMS_COLLECTION);
    
    try
    {
        var averageFlem = flems.AsQueryable().Average(flem => flem.FlemRate);
        return new Flem(averageFlem);
    } catch 
    {
        return new Flem(0);
    }        
}).WithName("GetAverageFlem");

app.MapGet("/flem", async () =>
{
    var flems = GetCollection<FlemDocument>(FLEMS_COLLECTION);

    return await flems.AsQueryable().Select(flem => new Flem(flem.FlemRate)).ToListAsync();
}).WithName("GetFlems");

app.Run();


IMongoCollection<T> GetCollection<T>(string collection)
{
    var client = new MongoClient(builder.Configuration.GetConnectionString("MongoDB"));
    var db = client.GetDatabase(builder.Configuration.GetConnectionString("MONGODB_NAME"));

    return db.GetCollection<T>(collection);
}