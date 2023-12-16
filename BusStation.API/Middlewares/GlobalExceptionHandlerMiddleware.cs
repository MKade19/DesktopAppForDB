using BusStation.API.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BusStation.API.Middlewares
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
				await next(context);
			}
            catch (UprocessibleEntityException e)
            {
                var traceId = Guid.NewGuid();
                var StatusCode = StatusCodes.Status422UnprocessableEntity;

                context.Response.StatusCode = StatusCode;

                var problemDetails = new ProblemDetails
                {
                    Type = "http://api/bus-station/",
                    Title = "Uprocessible Entity Error",
                    Status = StatusCode,
                    Instance = context.Request.Path,
                    Detail = $"Internal server error occured, traceId: {traceId}, message: {e.Message}",
                };

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
            catch (NotFoundException e)
            {
                var traceId = Guid.NewGuid();
                var StatusCode = StatusCodes.Status404NotFound;

                context.Response.StatusCode = StatusCode;

                var problemDetails = new ProblemDetails
                {
                    Type = "http://api/bus-station/",
                    Title = "Not found Error",
                    Status = StatusCode,
                    Instance = context.Request.Path,
                    Detail = $"Internal server error occured, traceId: {traceId}, message: {e.Message}",
                };

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
            catch (UnauthorizedException e)
            {
                var traceId = Guid.NewGuid();
                var StatusCode = StatusCodes.Status401Unauthorized;

                context.Response.StatusCode = StatusCode;

                var problemDetails = new ProblemDetails
                {
                    Type = "http://api/bus-station/",
                    Title = "Unauthorized Error",
                    Status = StatusCode,
                    Instance = context.Request.Path,
                    Detail = $"Internal server error occured, traceId: {traceId}, message: {e.Message}",
                };

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
            catch (BadRequestException e)
            {
                var traceId = Guid.NewGuid();
                var StatusCode = StatusCodes.Status400BadRequest;

                context.Response.StatusCode = StatusCode;

                var problemDetails = new ProblemDetails
                {
                    Type = "http://api/bus-station/",
                    Title = "Bad request Error",
                    Status = StatusCode,
                    Instance = context.Request.Path,
                    Detail = $"Internal server error occured, traceId: {traceId}, message: {e.Message}",
                };

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
            catch (Exception e)
			{
                var traceId = Guid.NewGuid();
                var StatusCode = StatusCodes.Status500InternalServerError;

                context.Response.StatusCode = StatusCode;

                var problemDetails = new ProblemDetails
                {
                    Type = "http://api/bus-station/",
                    Title = "Internal Server Error",
                    Status = StatusCode,
                    Instance = context.Request.Path,
                    Detail = $"Internal server error occured, traceId: {traceId}, message: {e.Message}",
                };

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
