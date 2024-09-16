// <copyright file="ToolbarElement.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

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