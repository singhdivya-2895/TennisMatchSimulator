
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using RestApi.DTO;
using TennisMatchService;
using TennisMatchService.Logger;

namespace RestApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tennis Match Simulator", Version = "v1" });
            });
            builder.Services.AddSingleton<ITennisMatchService, TennisMatchService.TennisMatchService>();
            builder.Services.AddLogging(builder =>
            {
                builder.AddSimpleConsole(options =>
                {
                    options.IncludeScopes = false;
                    options.SingleLine = true;
                });
            });
            builder.Services.AddSingleton<MatchLogger>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapPost("/playMatch", ([FromBody] TennisMatchDto matchDto, ITennisMatchService tennisMatchService) =>
            {
                tennisMatchService.PlayMatch(matchDto.Players, matchDto.Points);
                return "Match Finished, Please check logs for match summary";
            })
            .WithName("AddTeamToSport")
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi(x => new OpenApiOperation(x)
            {
                Summary = "Play a tennis match",
                Description = "POST endpoint to simulate a tennis match for player and points history.",
                Tags = new List<OpenApiTag> { new() { Name = "Rest API for match simulation" } }
            });

            app.Run();
        }
    }
}
