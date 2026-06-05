using AtelierTest.Services;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using AtelierTest.Middleware;

var builder = WebApplication.CreateBuilder(args);

// SERVICES

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

builder.Services.AddSingleton<PlayerService>();

builder.Services.AddEndpointsApiExplorer();

// Swagger Examples
builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Tennis API",
        Version = "v1",
        Description = "API permettant de gérer des joueurs de tennis et statistiques"
    });

    // Active les examples Swagger
    c.ExampleFilters();

    // XML comments
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// MIDDLEWARE

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tennis API v1");
    c.DocumentTitle = "Tennis API";
    c.DefaultModelsExpandDepth(-1);
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();


app.Run();