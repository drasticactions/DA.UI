// <copyright file="INavigationTabGroup.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DA.UI.Views.Navigation;

/// <summary>
/// Navigation Tab Group Interface.
/// </summary>
public interface INavigationTabGroup : INavigationTab
{
    /// <summary>
    /// Gets or sets the children of the tab.
    /// </summary>
    public INavigationTab Children { get; set; }
}