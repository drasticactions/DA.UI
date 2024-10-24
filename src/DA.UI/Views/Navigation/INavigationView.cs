// <copyright file="INavigationView.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DA.UI.Views.Navigation;

/// <summary>
/// Navigation View Interface.
/// </summary>
public interface INavigationView
{
    /// <summary>
    /// Gets or sets the title of the view.
    /// </summary>
    string Title { get; set; }

    /// <summary>
    /// Gets or sets the tabs of the view.
    /// </summary>
    public INavigationTabGroup Tabs { get; set; }
}