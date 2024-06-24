using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using WebApplication1.Controllers;
using WebApplication1.Mapper;
using WebApplication1.Model;
using WebApplication1.Repository;


var builder = WebApplication.CreateBuilder(args);


 builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

//Log.Logger = new LoggerConfiguration()
//    .ReadFrom.Configuration(builder.Configuration)
//    .WriteTo.Console()
    //.CreateLogger();

builder.Services.AddControllers();
builder.Services.AddDbContext<EmployeeDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("dbcs")));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.AddAuthorization();
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Practice.API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Enter 'Bearer' [space] and your token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header
        },
        new List<string>()
    }
    });
});
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();



//using EFCoreDemo;
//using EFCoreDemo.Data;
//using EFCoreDemo.Repositories;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
//using Swashbuckle.AspNetCore.SwaggerGen;
//using System.Reflection;
//using System.Text;

//var builder = WebApplication.CreateBuilder(args);

//// Access the configuration
//var configuration = builder.Configuration;


//// Configure JWT authentication
//var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = configuration["Jwt:Issuer"],
//            ValidAudience = configuration["Jwt:Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey(key)
//        };

//        options.Events = new JwtBearerEvents
//        {
//            OnAuthenticationFailed = context =>
//            {
//                context.Response.StatusCode = 401;
//                context.Response.ContentType = "application/json";
//                return context.Response.WriteAsync(new
//                {
//                    success = false,
//                    message = "Authentication failed. Please check the token and try again."
//                }.ToString());
//            },
//            OnChallenge = context =>
//            {
//                context.HandleResponse();
//                context.Response.StatusCode = 401;
//                context.Response.ContentType = "application/json";
//                return context.Response.WriteAsync(new
//                {
//                    success = false,
//                    message = "You are not authorized."
//                }.ToString());
//            },
//            OnForbidden = context =>
//            {
//                context.Response.StatusCode = 403;
//                context.Response.ContentType = "application/json";
//                return context.Response.WriteAsync(new
//                {
//                    success = false,
//                    message = "You are not allowed to access this resource."
//                }.ToString());
//            }
//        };
//    });

















//This is For PostMan

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = builder.Configuration["Jwt:Issuer"],
//            ValidAudience = builder.Configuration["Jwt:Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

//        };
//        options.Events = new JwtBearerEvents
//        {
//            OnMessageReceived = context =>
//            {
//                var accessToken = context.Request.Query["access_token"];

//                var path = context.HttpContext.Request.Path;
//                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/api"))
//                {
//                    context.Token = accessToken;
//                }

//                return Task.CompletedTask;
//            }
//        };
//    });