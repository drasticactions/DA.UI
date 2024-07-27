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

    /// <inheritdoc/>
    public Section? Parent { get; private set; }

    public virtual void Layout()
    {
    }

    public virtual bool Matches(string text) => false;

    internal void SetParent(Section section)
    {
        this.Parent = section;
    }
}