// <copyright file="Element.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DA.UI.TableView;

/// <summary>
/// Implements a UITableViewCell that can be used with DA.Dialog as an Element.
/// </summary>
public abstract class Element : UITableViewCell
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Element"/> class.
    /// Initializes the element with the given caption.
    /// </summary>
    /// <param name="reuseIdentifier">Cell reuse identifier.</param>
    /// <param name="style">The style of UITableViewCell, defaults to Default.</param>
    public Element(string reuseIdentifier, UITableViewCellStyle style = UITableViewCellStyle.Default)
        : base(style, reuseIdentifier)
    {
    }

    /// <summary>
    /// Gets the parent section of this element.
    /// </summary>
    public Section? Parent { get; private set; }

    /// <summary>
    /// Layouts the cell.
    /// </summary>
    public virtual void Layout()
    {
    }

    /// <summary>
    /// If the cell matches.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>Match.</returns>
    public virtual bool Matches(string text) => false;

    /// <summary>
    /// Sets the parent.
    /// </summary>
    /// <param name="section">The section.</param>
    internal void SetParent(Section section)
    {
        this.Parent = section;
    }
}