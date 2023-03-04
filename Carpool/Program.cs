using Carpool.Database;
using Carpool.Services;
using Carpool.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var TokenValidationParameters = new TokenValidationParameters()
{
    ValidateIssuer = false,
    ValidateAudience = false,
    IssuerSigningKey= new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
    ClockSkew = TimeSpan.FromMinutes(2)// remove delay of token when expire
};
builder.Services.AddAuthentication(options =>
   { 
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; 
    })
    .AddJwtBearer(cfg =>
     {
        cfg.TokenValidationParameters = TokenValidationParameters;
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();


/*
builder.Services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase("CarpoolDb"));*/
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CarpoolApiConnectionString")), ServiceLifetime.Transient);
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBookRideService, BookRideService>();
builder.Services.AddScoped<IHistoryService, HistoryService>();
builder.Services.AddScoped<IOfferRideService, OfferRideService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin();
});


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();