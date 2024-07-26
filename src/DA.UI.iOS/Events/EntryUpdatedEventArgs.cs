// <copyright file="EntryUpdatedEventArgs.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DA.UI.Events;

public class EntryUpdatedEventArgs : EventArgs
{
    private string entry;

    public EntryUpdatedEventArgs(string entry)
    {
        this.entry = entry;
    }

    public string Entry => this.entry;
}