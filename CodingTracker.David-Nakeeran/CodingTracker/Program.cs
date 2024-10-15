using Microsoft.Extensions.DependencyInjection;
using CodingTracker.Database;
using CodingTracker.Utilities;
using CodingTracker.Views;
using CodingTracker.Controller;
using CodingTracker.Coordinators;

namespace CodingTracker;

class Program
{
    static void Main(string[] args)
    {
        // Create service collection
        var services = new ServiceCollection();

        // Register services
        services.AddSingleton<Conversion>();
        services.AddSingleton<TimeCalculator>();
        services.AddSingleton<Validation>();
        services.AddSingleton<InputHandler>();
        services.AddSingleton<DatabaseManager>();
        services.AddSingleton<MenuHandler>();
        services.AddSingleton<CodingTrackerController>();
        services.AddSingleton<CodingSessionTracker>();
        services.AddSingleton<AppCoordinator>();

        // Build service provider
        var serviceProvider = services.BuildServiceProvider();

        // Resolve AppCoordinator and start app
        var appCoordinator = serviceProvider.GetRequiredService<AppCoordinator>();
        appCoordinator.Start();

    }
}

