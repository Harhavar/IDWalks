using IDWalks.Data;
using IDWalks.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<IndiaWalkDbContext>(option => option.UseSqlServer
(builder.Configuration.GetConnectionString("IndiaWalks")));

builder.Services.AddScoped<IRegionsRepo, RegionsRepo>();

builder.Services.AddScoped<IWalkRepo,WalkRepo>();

builder.Services.AddScoped<IWalkDeficultyRepo, WalkDeficultyRepo>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

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
