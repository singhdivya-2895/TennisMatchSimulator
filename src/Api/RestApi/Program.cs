using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using RestApi.DTO;
using RestApi.Validator;
using System.Diagnostics.CodeAnalysis;
using TennisMatchService;
using TennisMatchService.Logger;
using FluentValidation;
using FluentValidation.Results;

namespace RestApi
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tennis Match Simulator", Version = "v1" });
            });
            builder.Services.AddSingleton<ITennisMatchService, TennisMatchService.TennisMatchManager>();
            builder.Services.AddScoped<IValidator<TennisMatchDto>, TennisMatchDtoValidator>();
            
            builder.Services.AddLogging(builder =>
            {
                builder.AddSimpleConsole(options =>
                {
                    options.IncludeScopes = false;
                    options.SingleLine = true;
                });
            });
            builder.Services.AddSingleton<IMatchLogger, MatchLogger>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapPost("/api/playMatch", (ITennisMatchService tennisMatchService, [FromBody] TennisMatchDto matchDto, IValidator<TennisMatchDto> validator) =>
            {
                ValidationResult validationResult = validator.Validate(matchDto);
                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }
                tennisMatchService.PlayMatch(matchDto.Players, matchDto.Points);
                return Results.Ok("Match Finished, Please check logs for match summary");
            })
            .WithName("AddTeamToSport")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi(x => new OpenApiOperation(x)
            {
                Summary = "Simulate a tennis match",
                Description = "POST endpoint to simulate a tennis match for player and points history.",
                Tags = new List<OpenApiTag> { new() { Name = "Rest API for match simulation" } }
            });

            app.Run();
        }
    }
}
