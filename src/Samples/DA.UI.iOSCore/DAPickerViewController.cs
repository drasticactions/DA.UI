using DA.UI.Buttons;

namespace DA.UI.iOSApp;

public sealed class DAPickerViewController : UIViewController
{
    private DAPicker picker;

    public DAPickerViewController()
    {
        this.picker = new DAPicker();
        this.picker.Menu = UIMenu.Create("Menu", new[]
        {
            UIAction.Create("Action 1", null, null, (action) => { }),
            UIAction.Create("Action 2", null, null, (action) => { }),
            UIAction.Create("Action 3", null, null, (action) => { }),
        });
        this.View!.AddSubview(this.picker);
        this.picker.TranslatesAutoresizingMaskIntoConstraints = false;
        this.picker.CenterXAnchor.ConstraintEqualTo(this.View!.CenterXAnchor).Active = true;
        this.picker.CenterYAnchor.ConstraintEqualTo(this.View!.CenterYAnchor).Active = true;
    }
}