using API.Database;
using API.Database.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var corsOrigin = "_corsOrigin";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsOrigin,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7246","http://localhost:5136").AllowAnyMethod().AllowAnyHeader();
                      });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MainDbContext>(option =>
                option.UseSqlServer(builder.Configuration.GetConnectionString("MainDatabase")));


builder.Services.AddScoped<CustomerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(corsOrigin);

app.UseAuthorization();

app.MapControllers();

app.Run();
