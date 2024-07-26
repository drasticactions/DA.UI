using System.ComponentModel;
using DA.UI.Commands;

namespace DA.UI.Buttons;

public class AsyncCommandButton : UIButton, IDisposable
{
    private IAsyncCommand value;

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

    public IAsyncCommand AsyncCommand => this.value;

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

    public void Dispose()
    {
        this.value.PropertyChanged -= this.OnCommandPropertyChanged;
        this.value.CanExecuteChanged -= this.OnCommandCanExecuteChanged;
    }
}