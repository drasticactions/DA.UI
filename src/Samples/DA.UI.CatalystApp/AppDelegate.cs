// <copyright file="AppDelegate.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using AppKit;
using DA.UI.Commands;
using DA.UI.iOSApp;
using DA.UI.Services;
using DA.UI.Toolbar;
using Microsoft.Extensions.DependencyInjection;

namespace DA.UI.CatalystApp;

[Register("AppDelegate")]
public class AppDelegate : UIApplicationDelegate
{
    private IServiceProvider provider;

    public override UIWindow? Window { get; set; }

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        // create a new service provider
        var services = this.ConfigureBaseServices();
        this.provider = services.BuildServiceProvider();
        var appDispatcher = this.provider.GetRequiredService<IAppDispatcher>();
        var errorService = this.provider.GetRequiredService<IErrorHandler>();
        var asyncCommand = new AsyncCommand("Test", async (x, y, z) =>
        {
            await Task.Delay(5000);
        }, appDispatcher, errorService);
        // create a new window instance based on the screen size
        Window = new UIWindow(UIScreen.MainScreen.Bounds);

        // create a UIViewController with a single UILabel
        var vc = new UINavigationController(this.provider.GetRequiredService<MainViewController>());
        var toolbar = new Toolbar.Root()
        {
            new ToolbarElement("Add", UIImage.GetSystemImage("plus")!)
            {
            },
            new AsyncCommandToolbarElement(asyncCommand, UIImage.GetSystemImage("star")!),
        }.Generate();
        Window.RootViewController = vc;
        Window.WindowScene!.Titlebar!.Toolbar = toolbar;
        toolbar.Visible = true;
        Window.WindowScene!.Titlebar!.ToolbarStyle = UITitlebarToolbarStyle.Automatic;

        // make the window visible
        Window.MakeKeyAndVisible();

        return true;
    }
}