using BusStation.API.Data;
using BusStation.API.Data.Abstract;
using BusStation.API.Middlewares;
using BusStation.API.Services;
using BusStation.API.Services.Abstract;

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

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();


