// <copyright file="Element.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

#if MACCATALYST
using AppKit;

namespace DA.UI.Toolbar;

public abstract class Element : NSToolbarItem
{
    protected Element()
        : base(Guid.NewGuid().ToString())
    {
    }

    protected Element(string identifier)
        : base(identifier)
    {
    }

    public virtual string ElementIdentifier => this.Identifier;

    public abstract void InvokeAction();
}
#endif