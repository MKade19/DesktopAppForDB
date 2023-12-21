using BusStation.API.Data;
using BusStation.API.Data.Abstract;
using BusStation.API.Middlewares;
using BusStation.API.Services;
using BusStation.API.Services.Abstract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BusStation.API;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();
builder.Services.AddTransient<IBusProducerRepository, BusProducerRepository>();
builder.Services.AddTransient<IBusModelRepository, BusModelRepository>();
builder.Services.AddTransient<IBusRepository, BusRepository>();
builder.Services.AddTransient<IBusRouteRepository, BusRouteRepository>();
builder.Services.AddTransient<IPositionRepository, PositionRepository>();
builder.Services.AddTransient<IWorkerRepository, WorkerRepository>();
builder.Services.AddTransient<IMedicalInspetionRepository, MedicalInspectionRepository>();
builder.Services.AddTransient<ITechnicalInspetionRepository, TechnicalInspectionRepository>();
builder.Services.AddTransient<IRepairmentRepository, RepairmentRepository>();
builder.Services.AddTransient<IVoyageRepository, VoyageRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IBusProducerService, BusProducerService>();
builder.Services.AddTransient<IBusModelService, BusModelService>();
builder.Services.AddTransient<IBusService, BusService>();
builder.Services.AddTransient<IBusRouteService, BusRouteService>();
builder.Services.AddTransient<IPositionService, PositionService>();
builder.Services.AddTransient<IWorkerService, WorkerService>();
builder.Services.AddTransient<IMedicalInspectionService, MedicalInspectionService>();
builder.Services.AddTransient<ITechnicalInspectionService, TechnicalInspectionService>();
builder.Services.AddTransient<IRepairmentService, RepairmentService>();
builder.Services.AddTransient<IVoyageService, VoyageService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IHashService, HashService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


