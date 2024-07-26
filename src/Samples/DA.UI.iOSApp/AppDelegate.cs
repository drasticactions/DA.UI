// <copyright file="AppDelegate.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Microsoft.Extensions.DependencyInjection;

namespace DA.UI.iOSApp;

[Register("AppDelegate")]
public class AppDelegate : UIApplicationDelegate
{
	private IServiceProvider provider;

	public override UIWindow? Window
	{
		get;
		set;
	}

	public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
	{
		// create a new service provider
		var services = this.ConfigureBaseServices();
		this.provider = services.BuildServiceProvider();

		// create a new window instance based on the screen size
		Window = new UIWindow(UIScreen.MainScreen.Bounds);

		// create a UIViewController with a single UILabel
		var vc = new UINavigationController(this.provider.GetRequiredService<MainViewController>());
		Window.RootViewController = vc;

		// make the window visible
		Window.MakeKeyAndVisible();

		return true;
	}
}
