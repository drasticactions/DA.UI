using DA.UI.Commands;

#if MACCATALYST
namespace DA.UI.Toolbar;

public sealed class AsyncCommandToolbarElement : Element
{
    private IAsyncCommand command;

    public AsyncCommandToolbarElement(IAsyncCommand command, UIImage image)
        : base(Guid.NewGuid().ToString())
    {
        this.Label = command.Title;
        this.UIImage = image;
        this.command = command;
        this.command.IsBusyChanged += this.Command_IsBusyChanged;
        this.command.CanExecuteChanged += this.Command_CanExecuteChanged;
    }

    private void Command_CanExecuteChanged(object? sender, EventArgs e)
    {
        this.InvokeOnMainThread(() =>
        {
            this.Enabled = this.command.CanExecute(null);
        });
    }

    private void Command_IsBusyChanged(object? sender, bool e)
    {
        this.InvokeOnMainThread(() =>
        {
            this.Enabled = !e;
        });
    }

    /// <inheritdoc/>
    public override async void InvokeAction()
    {
        if (this.command.CanExecute(null))
        {
            this.command.Execute(null);
        }
    }
}
#endif