// <copyright file="DAPicker.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using ObjCRuntime;

namespace DA.UI.Buttons;

/// <summary>
/// Replacement of UIPicker which works on macOS Catalyst.
/// Port of https://gist.github.com/steventroughtonsmith/68a554986da979fb73d4f72ee416bc1e.
/// </summary>
public sealed class DAPicker : UIButton
{
    private int selectedPopUpIndex;

    /// <summary>
    /// Initializes a new instance of the <see cref="DAPicker"/> class.
    /// </summary>
    public DAPicker()
        : base(UIButtonType.System)
    {
        this.ShowsMenuAsPrimaryAction = true;

        if (UIDevice.CurrentDevice.CheckSystemVersion(15, 0))
        {
            this.ChangesSelectionAsPrimaryAction = true;
        }
    }

    /// <summary>
    /// Gets or sets the selected index of the popup.
    /// </summary>
    public int SelectedPopUpIndex
    {
        get => this.selectedPopUpIndex;
        set
        {
            this.selectedPopUpIndex = value;
            this.SendActionForControlEvents(UIControlEvent.ValueChanged);
        }
    }

    /// <inheritdoc/>
    public override UIMenu? Menu
    {
        get => base.Menu;
        set
        {
            if (value?.Children is { Length: > 0 })
            {
                this.SetTitle(value.Children[0].Title, UIControlState.Normal);
            }

            base.Menu = value;
        }
    }

    /// <inheritdoc/>
    public override void WillMoveToSuperview(UIView? newsuper)
    {
        base.WillMoveToSuperview(newsuper);
        this.PreparePopUpButton();
    }

    private void PreparePopUpButton()
    {
        // Check if running on macOS 12 or later
        if (NSProcessInfo.ProcessInfo.OperatingSystemVersion.Major >= 12)
        {
            return;
        }

        // Using ValueForKeyPath to access private properties
        var nsPopupButton = this.ValueForKeyPath(new NSString("_visualElement._button")) as NSObject;

        // Check if the object responds to setPullsDown:
        var setPullsDownSelector = new Selector("setPullsDown:");
        if (!nsPopupButton.RespondsToSelector(setPullsDownSelector))
        {
            return;
        }

        // Set pullsDown to false
        nsPopupButton.SetValueForKey(NSNumber.FromBoolean(false), new NSString("pullsDown"));

        // Get the NSMenu
        var nsMenu = nsPopupButton.ValueForKeyPath(new NSString("menu")) as NSObject;

        // Observe menu action notifications
        NSNotificationCenter.DefaultCenter.AddObserver(
            new NSString("NSMenuDidSendActionNotification"),
            notification =>
            {
                if (nsPopupButton.ValueForKey(new NSString("indexOfSelectedItem")) is NSNumber indexOfSelectedItem)
                {
                    this.InvokeOnMainThread(() => { this.SelectedPopUpIndex = (int)indexOfSelectedItem.Int32Value; });
                }
            },
            nsMenu);
    }
}