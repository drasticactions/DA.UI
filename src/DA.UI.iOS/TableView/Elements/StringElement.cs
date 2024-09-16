// <copyright file="StringElement.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DA.UI.TableView.Elements;

/// <summary>
/// Represents an element that displays a string value in a table view cell.
/// Inherits from <see cref="BaseElement{T}"/> with a string type parameter.
/// </summary>
public class StringElement : BaseElement<string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StringElement"/> class with the specified value.
    /// </summary>
    /// <param name="value">The string value to display in the cell.</param>
    public StringElement(string value)
        : base(value, "StringElement", UITableViewCellStyle.Default)
    {
    }

    /// <summary>
    /// Configures the cell's content based on the value.
    /// </summary>
    /// <remarks>
    /// This method sets up the cell's content configuration to display the string value.
    /// It configures the text properties to support word wrapping and allows for an unlimited number of lines.
    /// </remarks>
    public override void Layout()
    {
        var contentConfiguration = UIListContentConfiguration.CellConfiguration;
        contentConfiguration.Text = this.Value;
        contentConfiguration.TextProperties.NumberOfLines = 0;
        contentConfiguration.TextProperties.LineBreakMode = UILineBreakMode.WordWrap;
        this.ContentConfiguration = contentConfiguration;
    }

    /// <summary>
    /// Determines whether the specified text matches the value of this element.
    /// </summary>
    /// <param name="text">The text to compare with the element's value.</param>
    /// <returns><c>true</c> if the specified text matches the element's value; otherwise, <c>false</c>.</returns>
    public override bool Matches(string text)
    {
        return this.Value.Contains(text, StringComparison.OrdinalIgnoreCase);
    }
}