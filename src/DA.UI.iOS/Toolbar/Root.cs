// <copyright file="Root.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Collections;

#if MACCATALYST
using AppKit;

namespace DA.UI.Toolbar;

public class Root : NSToolbarDelegate, IEnumerable<Element>
{
    private List<Element> elements = new();
    private ObjCRuntime.Selector action;

    public Root()
    {
        this.action = new ObjCRuntime.Selector("itemClickAction:");
    }

    public void Add(Element item)
    {
        item.Target = this;
        item.Action = this.action;
        this.elements.Add(item);
    }

    public IEnumerator<Element> GetEnumerator()
    {
        foreach (var s in this.elements)
        {
            yield return s;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    [Export("itemClickAction:")]
    public void ItemClickAction(NSObject sender)
    {
        if (sender is Element element)
        {
            this.InvokeOnMainThread(element.InvokeAction);
        }
    }

    /// <inheritdoc/>
    public override string[] AllowedItemIdentifiers(NSToolbar toolbar)
    {
        return [];
    }

    /// <inheritdoc/>
    public override string[] DefaultItemIdentifiers(NSToolbar toolbar)
    {
        return this.elements.Select(x => x.Identifier).ToArray();
    }

    /// <inheritdoc/>
    public override NSToolbarItem? WillInsertItem(NSToolbar toolbar, string itemIdentifier, bool willBeInserted)
    {
        return this.elements.FirstOrDefault(x => x.ElementIdentifier == itemIdentifier) as NSToolbarItem;
    }
}

public static class RootExtensions
{
    public static RootToolbar Generate(this Root root, string? identifier = default)
    {
        var toolbar = new RootToolbar(root, identifier ?? Guid.NewGuid().ToString());
        return toolbar;
    }
}
#endif