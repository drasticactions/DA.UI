// <copyright file="MainViewController.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.ComponentModel;
using DA.UI.Buttons;
using DA.UI.Commands;
using DA.UI.Events;
using DA.UI.Services;
using DA.UI.TableView;
using DA.UI.TableView.Elements;
using Masonry;

namespace DA.UI.iOSApp;

public sealed class MainViewController : UITableViewController
{
    private readonly IAppDispatcher dispatcher;
    private readonly IErrorHandler errorHandler;

    private IAsyncCommand delayTaskCommand;

    private bool canExecuteDelayTaskCommand = true;

    public MainViewController(IAppDispatcher dispatcher, IErrorHandler errorHandler)
    {
        this.dispatcher = dispatcher;
        this.errorHandler = errorHandler;

#if !TVOS
        this.View!.BackgroundColor = UIColor.SystemBackground;
#endif

        this.Title = "DA.UI";

        this.delayTaskCommand = new AsyncCommand("Delay", async (x, y, z) =>
            {
                z.Report("Starting Task");
                await Task.Delay(5000);
                z.Report("Task Complete");
            },
            this.dispatcher,
            this.errorHandler,
            canExecute: () => this.canExecuteDelayTaskCommand);

        this.TableView = new Root()
        {
            new Section("Welcome")
            {
                new StringElement("Welcome to DA.UI."),
            },
            new Section("Async Command")
            {
                AsyncCommandElement.Create(this.delayTaskCommand),
                ActionElement.Create("Disable/Enable Delay Command", () =>
                {
                    if (!this.delayTaskCommand.IsBusy)
                    {
                        this.canExecuteDelayTaskCommand = !this.canExecuteDelayTaskCommand;
                        this.delayTaskCommand.RaiseCanExecuteChanged();
                    }
                }),
            },
            new Section("View Controllers")
            {
                ActionElement.Create("Login View Controller", () =>
                {
                    var loginViewController = new LoginViewController(this.dispatcher, this.errorHandler);
                    this.NavigationController!.PushViewController(loginViewController, true);
                }),
            },
        };
    }
}