using BugsManager.Application.Extensions;
using BugsManager.Infrastructure.Extensions;
using BugsManager.Persistence.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var MyAllowSpecificOrigins = "AllowOrigin";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("https://localhost:7164")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
        });
});

// Add services to the container.
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer();
builder.Services.AddPersistenceLayer(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API V1");

        // Para permitir swagger-client
        c.RoutePrefix = string.Empty;
    });
}


app.UseAuthorization();
app.UseCors(MyAllowSpecificOrigins);
app.MapControllers();


app.UseRequestLocalization();


app.Run();
