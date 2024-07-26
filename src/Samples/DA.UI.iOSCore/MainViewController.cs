// <copyright file="MainViewController.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.ComponentModel;
using DA.UI.Commands;
using DA.UI.Events;
using DA.UI.Services;
using DA.UI.TableView;

namespace DA.UI.iOSApp;

public sealed class MainViewController : UIViewController
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

        this.View = new Root()
        {
            new Section("Welcome")
            {
                new StringElement("Welcome to DA.UI."),
            },
            new Section("Async Command")
            {
                new AsyncCommandElement(this.delayTaskCommand),
                new ActionElement("Disable/Enable Delay Command", () => {
                    if (!this.delayTaskCommand.IsBusy)
                    {
                        this.canExecuteDelayTaskCommand = !this.canExecuteDelayTaskCommand;
                        this.delayTaskCommand.RaiseCanExecuteChanged();
                    }
                }),
            },
        };
    }

    public class ActionElement : Element
    {
        private UIButton button;

        private Action value;

        public ActionElement(string title, Action value)
            : base("ActionElement", UITableViewCellStyle.Default)
        {
            ArgumentNullException.ThrowIfNull(value);
            this.value = value;
            this.button = new UIButton(UIButtonType.System);
            this.button.SetTitle(title, UIControlState.Normal);
            this.button.TouchUpInside += (s, e) => this.value();
            this.ContentView.AddSubview(this.button);
            this.button.TranslatesAutoresizingMaskIntoConstraints = false;
            this.button.TopAnchor.ConstraintEqualTo(this.ContentView.TopAnchor).Active = true;
            this.button.BottomAnchor.ConstraintEqualTo(this.ContentView.BottomAnchor).Active = true;
            this.button.LeadingAnchor.ConstraintEqualTo(this.ContentView.LeadingAnchor).Active = true;
            this.button.TrailingAnchor.ConstraintEqualTo(this.ContentView.TrailingAnchor).Active = true;
        }

        public override void Layout()
        {
        }
    }

    public class AsyncCommandElement : Element
    {
        private UIButton button;
        private IAsyncCommand value;

        public AsyncCommandElement(IAsyncCommand value)
            : base("AsyncCommandElement", UITableViewCellStyle.Default)
        {
            ArgumentNullException.ThrowIfNull(value);
            this.value = value;
            this.button = new UIButton(UIButtonType.System);
            this.button.SetTitle(value.Title, UIControlState.Normal);
            this.button.TouchUpInside += async (s, e) => {
                if (this.value.CanExecute(null))
                {
                    await this.value.ExecuteAsync();
                }
            };
            this.ContentView.AddSubview(this.button);
            this.button.TranslatesAutoresizingMaskIntoConstraints = false;
            this.button.TopAnchor.ConstraintEqualTo(this.ContentView.TopAnchor).Active = true;
            this.button.BottomAnchor.ConstraintEqualTo(this.ContentView.BottomAnchor).Active = true;
            this.button.LeadingAnchor.ConstraintEqualTo(this.ContentView.LeadingAnchor).Active = true;
            this.button.TrailingAnchor.ConstraintEqualTo(this.ContentView.TrailingAnchor).Active = true;
            this.button.Configuration = this.SetButtonConfiguration(false);
            this.value.PropertyChanged += this.OnCommandPropertyChanged;
            this.value.CanExecuteChanged += this.OnCommandCanExecuteChanged;
        }

        private UIButtonConfiguration SetButtonConfiguration(bool isRunning)
        {
            var config = UIButtonConfiguration.FilledButtonConfiguration;
            config.CornerStyle = UIButtonConfigurationCornerStyle.Fixed;
            config.Background.CornerRadius = 0;
            config.ShowsActivityIndicator = isRunning;
            config.ImagePlacement = NSDirectionalRectEdge.Leading;
            config.ImagePadding = 10;
            return config;
        }

        private void OnCommandCanExecuteChanged(object? sender, EventArgs e)
        {
            this.button.Enabled = this.value.CanExecute(null);
        }

        private void OnCommandPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IAsyncCommand.Title))
            {
                this.button.SetTitle(this.value.Title, UIControlState.Normal);
            }

            if (e.PropertyName == nameof(IAsyncCommand.IsBusy))
            {
                this.button.Configuration = this.SetButtonConfiguration(this.value.IsBusy);
            }
        }

        public override void Layout()
        {
        }
    }
}