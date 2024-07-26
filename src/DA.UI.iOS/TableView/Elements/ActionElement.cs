// <copyright file="ActionElement.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DA.UI.TableView.Elements;

public sealed class ActionElement : UIButtonElement
{
    private Action action;

    private ActionElement(UIButton button, Action value)
        : base(button, "ActionElement", UITableViewCellStyle.Default)
    {
        ArgumentNullException.ThrowIfNull(value);
        this.action = value;
        this.Button.TouchUpInside += (s, e) => this.action();
    }

    public override void Layout()
    {
    }

    public static ActionElement Create(UIButton button, Action action)
        => new ActionElement(button, action);

    public static ActionElement Create(string title, Action value)
    {
        ArgumentNullException.ThrowIfNull(value);
        var button = new UIButton(UIButtonType.System);
        button.SetTitle(title, UIControlState.Normal);
        return new ActionElement(button, value);
    }
}