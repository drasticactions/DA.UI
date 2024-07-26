using DA.UI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DA.UI.iOSApp;

public static class AppDelegateExtensions
{
    public static ServiceCollection ConfigureBaseServices(this UIApplicationDelegate app)
    {
        var services = new ServiceCollection();
        services.AddSingleton<IAppDispatcher, AppDispatcher>();
        services.AddSingleton<IErrorHandler, DebugErrorHandler>();
        services.AddSingleton<IAsyncCommandFactory, AsyncCommandFactory>();
        services.AddSingleton<MainViewController>();
        return services;
    }
}
