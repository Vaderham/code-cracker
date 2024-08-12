using System.Text.Json;
using CodeCracker.Dictionary;
using CodeCracker.GridTemplates;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/ProcessDictionary", () =>
{
    var jsonString = File.ReadAllText("./dictionary_compact.json");
    var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString)!;
    File.WriteAllText("./dictionary_words_only.json", JsonSerializer.Serialize(dictionary.Select(item => item.Key).Order()));
});

app.MapPost("/CreateTestGrid", () =>
{
    var grid = new SmallGrid();
    var dict = new EnglishDictionary(5);
    grid.PopulatedGrid();
});

app.Run();