// <copyright file="ActionElement.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DA.UI.TableView.Elements;

/// <summary>
/// Action Element.
/// </summary>
public sealed class ActionElement : UIButtonElement
{
    private Action action;

    /// <summary>
    /// Initializes a new instance of the <see cref="ActionElement"/> class.
    /// </summary>
    /// <param name="button">The UIButton.</param>
    /// <param name="value">The Action.</param>
    private ActionElement(UIButton button, Action value)
        : base(button, "ActionElement", UITableViewCellStyle.Default)
    {
        ArgumentNullException.ThrowIfNull(value);
        this.action = value;
        this.Button.TouchUpInside += (s, e) => this.action();
    }

    /// <inheritdoc/>
    public override void Layout()
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="ActionElement"/> class with the specified UIButton and Action.
    /// </summary>
    /// <param name="button">The UIButton to associate with the ActionElement.</param>
    /// <param name="action">The Action to be executed when the button is pressed.</param>
    /// <returns>A new instance of the <see cref="ActionElement"/> class.</returns>
    public static ActionElement Create(UIButton button, Action action)
        => new ActionElement(button, action);

    /// <summary>
    /// Creates a new instance of the <see cref="ActionElement"/> class with a UIButton created from the specified title and Action.
    /// </summary>
    /// <param name="title">The title to set on the UIButton.</param>
    /// <param name="value">The Action to be executed when the button is pressed.</param>
    /// <returns>A new instance of the <see cref="ActionElement"/> class.</returns>
    public static ActionElement Create(string title, Action value)
    {
        ArgumentNullException.ThrowIfNull(value);
        var button = new UIButton(UIButtonType.System);
        button.SetTitle(title, UIControlState.Normal);
        return new ActionElement(button, value);
    }
}