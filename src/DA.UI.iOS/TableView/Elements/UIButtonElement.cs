// <copyright file="UIButtonElement.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DA.UI.TableView.Elements;

/// <summary>
/// Represents an abstract base class for elements that contain a UIButton.
/// Inherits from the <see cref="Element"/> class.
/// </summary>
public abstract class UIButtonElement : Element
{
    private readonly UIButton button;

    /// <summary>
    /// Initializes a new instance of the <see cref="UIButtonElement"/> class with the specified UIButton, reuse identifier, and table view cell style.
    /// </summary>
    /// <param name="button">The UIButton to associate with this element.</param>
    /// <param name="reuseIdentifier">The reuse identifier for the table view cell.</param>
    /// <param name="style">The style of the table view cell. Default is <see cref="UITableViewCellStyle.Default"/>.</param>
    protected UIButtonElement(UIButton button, string reuseIdentifier, UITableViewCellStyle style = UITableViewCellStyle.Default)
        : base(reuseIdentifier, style)
    {
        this.button = button;
        this.ContentView.AddSubview(this.button);
        this.button.TranslatesAutoresizingMaskIntoConstraints = false;
        this.button.TopAnchor.ConstraintEqualTo(this.ContentView.TopAnchor).Active = true;
        this.button.BottomAnchor.ConstraintEqualTo(this.ContentView.BottomAnchor).Active = true;
        this.button.LeadingAnchor.ConstraintEqualTo(this.ContentView.LeadingAnchor).Active = true;
        this.button.TrailingAnchor.ConstraintEqualTo(this.ContentView.TrailingAnchor).Active = true;
        this.SelectionStyle = UITableViewCellSelectionStyle.None;
    }

    /// <summary>
    /// Gets the UIButton associated with this element.
    /// </summary>
    protected UIButton Button => this.button;
}