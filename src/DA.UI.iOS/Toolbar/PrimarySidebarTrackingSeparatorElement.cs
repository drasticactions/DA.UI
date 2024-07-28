// <copyright file="PrimarySidebarTrackingSeparatorElement.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

#if MACCATALYST
namespace DA.UI.Toolbar;

public sealed class PrimarySidebarTrackingSeparatorElement : Element
{
    private string identifier;

    public PrimarySidebarTrackingSeparatorElement()
        : base("NSToolbarPrimarySidebarTrackingSeparatorItem")
    {
        this.identifier = Guid.NewGuid().ToString();
    }

    public override string ElementIdentifier => this.identifier;

    public override void InvokeAction()
    {
        throw new NotImplementedException();
    }
}
#endif