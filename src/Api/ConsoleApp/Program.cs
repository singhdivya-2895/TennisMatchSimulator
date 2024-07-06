using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TennisMatchService;
using TennisMatchService.Logger;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region "Dependency Injection"
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ITennisMatchService, TennisMatchService.TennisMatchService>()
                .AddLogging(builder =>
                {
                    builder.AddSimpleConsole(options =>
                    {
                        options.IncludeScopes = false;
                        options.SingleLine = true;
                    });
                })
                .AddTransient<MatchLogger>()
                .BuildServiceProvider();

            var tennisMatchService = serviceProvider.GetService<ITennisMatchService>();
            #endregion "Dependency Injection"

            string[] players = { "Player 1", "Player 2" };
            int[] points = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 };

            tennisMatchService?.PlayMatch(players, points);
            Console.ReadLine();
        }
    }
}
