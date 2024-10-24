// <copyright file="INavigationTab.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DA.UI.Views.Navigation;

/// <summary>
/// Navigation Tab Interface.
/// </summary>
public interface INavigationTab
{
    /// <summary>
    /// Gets or sets the title of the tab.
    /// </summary>
    string Title { get; set; }

    /// <summary>
    /// Gets the identifier of the tab.
    /// </summary>
    string Identifier { get; }
}