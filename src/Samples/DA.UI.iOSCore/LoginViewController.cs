using DA.UI.Commands;
using DA.UI.Services;
using DA.UI.TableView;
using DA.UI.TableView.Elements;

namespace DA.UI.iOSApp;

public sealed class LoginViewController : UITableViewController
{
    private readonly IAppDispatcher dispatcher;
    private readonly IErrorHandler errorHandler;
    private readonly EntryElement usernameElement;
    private readonly EntryElement passwordElement;
    private IAsyncCommand loginCommand;
    
    public LoginViewController(IAppDispatcher dispatcher, IErrorHandler errorHandler)
    {
        this.errorHandler = errorHandler;
        this.dispatcher = dispatcher;

#if !TVOS
        this.View!.BackgroundColor = UIColor.SystemBackground;
#endif

        this.Title = "Login";
        this.usernameElement = new EntryElement("Username");
        this.usernameElement.EntryChanged += (s, e) => this.RaiseCanExecuteChanged();
        this.usernameElement.TextField.AutocapitalizationType = UITextAutocapitalizationType.None;
        this.usernameElement.TextField.AutocorrectionType = UITextAutocorrectionType.No;
        this.usernameElement.TextField.ClearButtonMode = UITextFieldViewMode.WhileEditing;
        this.passwordElement = new EntryElement("Password", isPassword: true);
        this.passwordElement.EntryChanged += (s, e) => this.RaiseCanExecuteChanged();

        this.loginCommand = new AsyncCommand("Login", async (x, y, z) =>
            {
                z.Report("Starting Login");
                await Task.Delay(5000);
                z.Report("Login Complete");
            },
            this.dispatcher,
            this.errorHandler,
            canExecute: () => !string.IsNullOrEmpty(this.usernameElement.Value) && !string.IsNullOrEmpty(this.passwordElement.Value));
        this.loginCommand.IsBusyChanged += (s, e) =>
        {
            this.InvokeOnMainThread(() =>
            {
                this.usernameElement.IsEnabled = !this.loginCommand.IsBusy;
                this.passwordElement.IsEnabled = !this.loginCommand.IsBusy;
            });
        };

        this.TableView = new Root()
        {
            new Section("Login")
            {
                usernameElement,
                passwordElement,
                AsyncCommandElement.Create(this.loginCommand),
            },
        };

        this.RaiseCanExecuteChanged();
    }

    private void RaiseCanExecuteChanged()
    {
        this.loginCommand.RaiseCanExecuteChanged();
    }
}