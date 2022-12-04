using Microsoft.EntityFrameworkCore;
using Scrapper_DataAccess.Repositories.SportVision;
using Scrapper_Domain;
using ScrapperServices.Services.Scrapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
         builder.WithOrigins("http://localhost:4200")
             .AllowAnyMethod()
             .AllowAnyHeader()
             .AllowCredentials());
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IScrapperService, ScrapperService>();
builder.Services.AddTransient<ISportVisionRepository, SportVisionRepository>();

var dbConnectionString = builder.Configuration["ConnectionStrings:DatabaseConnectionString"];

//CONFIG NA LOGGER DA IMA DEBUG I INFORMATION MODE

builder.Services.AddDbContext<ScrapperDbContext>(options => options.UseSqlServer(dbConnectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
