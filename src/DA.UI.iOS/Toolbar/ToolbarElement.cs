#if MACCATALYST
namespace DA.UI.Toolbar;

public sealed class ToolbarElement : Element
{
    private Action action;
    public ToolbarElement(string label, UIImage image, Action? action = default)
        : base(Guid.NewGuid().ToString())
    {
        this.Label = label;
        this.UIImage = image;
        this.action = action ?? (() => { });
    }

    public override void InvokeAction() => this.action();
}
#endif