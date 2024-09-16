// <copyright file="DetailStringElement.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DA.UI.TableView;

namespace DA.UI;

/// <summary>
/// Represents an element that displays a caption and a detail string in a table view cell.
/// Inherits from <see cref="BaseElement{T}"/> with a string type parameter.
/// </summary>
public class DetailStringElement : BaseElement<string>
{
    private readonly string caption;

    /// <summary>
    /// Initializes a new instance of the <see cref="DetailStringElement"/> class with the specified caption and value.
    /// </summary>
    /// <param name="caption">The caption to display in the cell.</param>
    /// <param name="value">The detail text to display in the cell.</param>
    public DetailStringElement(string caption, string value)
        : base(value, "DetailStringElement")
    {
        this.caption = caption;
    }

    /// <summary>
    /// Gets the caption to display in the cell.
    /// </summary>
    public string Caption => this.caption;

    /// <summary>
    /// Configures the cell's content based on the value.
    /// </summary>
    /// <remarks>
    /// This method sets up the cell's content configuration to display the caption and detail text.
    /// It configures the text properties to support word wrapping and allows for an unlimited number of lines.
    /// </remarks>
    public override void Layout()
    {
        this.SelectionStyle = UITableViewCellSelectionStyle.None;
        var contentConfiguration = UIListContentConfiguration.ValueCellConfiguration;
        contentConfiguration.Text = this.Caption;
        contentConfiguration.TextProperties.NumberOfLines = 0;
        contentConfiguration.TextProperties.LineBreakMode = UILineBreakMode.WordWrap;
        contentConfiguration.SecondaryText = this.Value;
        contentConfiguration.SecondaryTextProperties.NumberOfLines = 0;
        contentConfiguration.SecondaryTextProperties.LineBreakMode = UILineBreakMode.WordWrap;
        this.ContentConfiguration = contentConfiguration;
    }
}
