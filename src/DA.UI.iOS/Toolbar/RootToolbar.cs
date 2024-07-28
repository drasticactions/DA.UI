// <copyright file="RootToolbar.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

#if MACCATALYST
using AppKit;

namespace DA.UI.Toolbar;

public sealed class RootToolbar : NSToolbar
{
    private Root root;

    public RootToolbar(Root root, string? identifier = default)
        : base(identifier ?? Guid.NewGuid().ToString())
    {
        this.root = root;
        this.Delegate = root;
    }

    public Root Root => this.root;
}
#endif