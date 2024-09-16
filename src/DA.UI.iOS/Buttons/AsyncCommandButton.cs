// <copyright file="AsyncCommandButton.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.ComponentModel;
using DA.UI.Commands;

namespace DA.UI.Buttons;

/// <summary>
/// Async Command Button.
/// </summary>
public class AsyncCommandButton : UIButton, IDisposable
{
    private IAsyncCommand value;

    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncCommandButton"/> class.
    /// </summary>
    /// <param name="value">The command.</param>
    public AsyncCommandButton(IAsyncCommand value)
    {
        ArgumentNullException.ThrowIfNull(value);
        this.value = value;
        this.SetTitle(value.Title, UIControlState.Normal);
        this.TouchUpInside += async (s, e) =>
        {
            if (this.value.CanExecute(null))
            {
                await this.value.ExecuteAsync();
            }
        };
        this.value.PropertyChanged += this.OnCommandPropertyChanged;
        this.value.CanExecuteChanged += this.OnCommandCanExecuteChanged;
        this.Configuration = this.SetButtonConfiguration(false);
    }

    /// <summary>
    /// Gets the async command.
    /// </summary>
    public IAsyncCommand AsyncCommand => this.value;

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        this.value.PropertyChanged -= this.OnCommandPropertyChanged;
        this.value.CanExecuteChanged -= this.OnCommandCanExecuteChanged;
    }

    /// <summary>
    /// Sets the button configuration.
    /// </summary>
    /// <param name="isRunning">If the button is running.</param>
    /// <returns>UIButtonConfiguration.</returns>
    protected virtual UIButtonConfiguration? SetButtonConfiguration(bool isRunning)
    {
        return this.Configuration;
    }

    private void OnCommandCanExecuteChanged(object? sender, EventArgs e)
    {
        this.Enabled = this.value.CanExecute(null);
    }

    private void OnCommandPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IAsyncCommand.Title))
        {
            this.SetTitle(this.value.Title, UIControlState.Normal);
        }

        if (e.PropertyName == nameof(IAsyncCommand.IsBusy))
        {
            this.Configuration = this.SetButtonConfiguration(this.value.IsBusy);
        }
    }
}