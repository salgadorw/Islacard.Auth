using RWS.Authentication.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using RWS.Authentication.Application.Services;
using RWS.Authentication.Domain.Core.Repositories;
using RWS.Authentication.Database.Repositories;
using RWS.Authentication.Application.Services.MappingProfile;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using RWS.Authentication.Api;
using Serilog;
using RWS.Authentication.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);


var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders().AddSerilog(logger);

builder.Services.AddTransient<HandleExceptionsMiddleware>();
//Database
builder.Services.AddDbContext<DbContextLogin>(op => op.UseInMemoryDatabase("LoginDb"));
//ConfigureDependencies
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

//Add authentication
builder.Services
    .AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; ;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; ;
        })
    .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = builder.Configuration["AuthSettings:Audience"],
                ValidIssuer = builder.Configuration["AuthSettings:Issuer"],
                RequireExpirationTime = true,
                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["AuthSettings:key"])),
                ValidateIssuerSigningKey = true,

            };
        });

//API
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.IncludeXmlComments(System.AppDomain.CurrentDomain.BaseDirectory + "RWS.Authentication.Api.xml", true);


    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Isracard Auh API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
   });

});

builder.Services.AddCors(policyBuilder =>
    policyBuilder.AddDefaultPolicy(policy =>
        policy.WithOrigins("*").AllowAnyHeader().AllowAnyHeader())
);


var app = builder.Build();

app.UseMiddleware<HandleExceptionsMiddleware>();
app.UseCors();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    //Initialise the database
    app.InitialiseDatabase();
}

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
}); 

app.Run();

