// <copyright file="Section.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Collections;

namespace DA.UI.TableView;

/// <summary>
/// Implements a UITableViewCell that can be used with DA.Dialog as a section.
/// </summary>
public class Section : IEnumerable<Element>, IEnumerable
{
    private List<Element> elements;

    /// <summary>
    /// Initializes a new instance of the <see cref="Section"/> class.
    /// </summary>
    /// <param name="caption">The caption.</param>
    public Section(string? caption = default)
    {
        this.Caption = caption;
        this.elements = new List<Element>();
    }

    /// <inheritdoc/>
    public string? Caption { get; }

    /// <inheritdoc/>
    public IReadOnlyList<Element> Elements => this.elements;

    public Root? Root { get; private set; }

    /// <summary>
    /// Gets the number of elements in this section.
    /// </summary>
    public int Count => this.Elements.Count;

    /// <inheritdoc/>
    public Element? this[int idx] => this.Elements?[idx];

    IEnumerator<Element> IEnumerable<Element>.GetEnumerator()
    {
        foreach (var s in this.Elements)
        {
            yield return s;
        }
    }

    /// <inheritdoc/>
    public IEnumerator GetEnumerator()
    {
        return (this.Elements ?? []).GetEnumerator();
    }

    /// <summary>
    /// Adds a new child Element to the Section.
    /// </summary>
    /// <param name="element">
    /// An element to add to the section.
    /// </param>
    public void Add(Element element, bool reloadTable = true) => this.Add(element, UITableViewRowAnimation.None, reloadTable);

    /// <summary>
    /// Adds a new child Element to the Section with the specified animation style.
    /// </summary>
    /// <param name="element">An element to add to the section.</param>
    /// <param name="style">The animation style to use.</param>
    public void Add(Element element, UITableViewRowAnimation style, bool reloadTable = true)
    {
        this.elements.Add(element);
        element.SetParent(this);

        if (this.Root != null && reloadTable)
        {
            this.InsertVisual(this.Elements.Count - 1, style, 1);
        }
    }

    /// <summary>
    /// Inserts a series of elements into the Section using the specified animation.
    /// </summary>
    /// <param name="idx">
    /// The index where the elements are inserted.
    /// </param>
    /// <param name="anim">
    /// The animation to use.
    /// </param>
    /// <param name="newElements">
    /// A series of elements.
    /// </param>
    /// <returns>Inserted number.</returns>
    public int Insert(int idx, UITableViewRowAnimation anim = UITableViewRowAnimation.None, bool reloadTable = true, params Element[] newElements)
    {
        ArgumentNullException.ThrowIfNull(newElements);

        int pos = idx;
        int count = 0;
        foreach (var e in newElements)
        {
            this.elements.Insert(pos++, e);
            e.SetParent(this);

            count++;
        }

        if (this.Root is not null && reloadTable)
        {
            if (anim == UITableViewRowAnimation.None)
            {
                this.Root.ReloadData();
            }
            else
            {
                this.InsertVisual(idx, anim, pos - idx);
            }
        }

        return count;
    }

    /// <summary>
    /// Inserts a single element into the Section using the specified animation.
    /// </summary>
    /// <param name="index">The starting index.</param>
    /// <param name="newElements">List of elements.</param>
    /// <returns></returns>
    public int Insert(int index, bool reloadTable = true, params Element[] newElements)
    {
        return this.Insert(index, UITableViewRowAnimation.None, reloadTable, newElements);
    }

    /// <summary>
    /// Removes an element at a specified index.
    /// </summary>
    /// <param name="e">The element to be removed.</param>
    public void Remove(Element e, bool reloadTable = true)
    {
        ArgumentNullException.ThrowIfNull(e);

        for (int i = this.Elements.Count; i > 0;)
        {
            i--;
            if (this.Elements[i] == e)
            {
                this.RemoveRange(i, 1, reloadTable);
                return;
            }
        }
    }

    /// <summary>
    /// Removes an element at a specified index.
    /// </summary>
    /// <param name="idx">The index of the element.</param>
    public void Remove(int idx, bool reloadTable = true)
    {
        this.RemoveRange(idx, 1, reloadTable);
    }

    /// <summary>
    /// Removes a range of elements from the Section.
    /// </summary>
    /// <param name="start">
    /// Starting position.
    /// </param>
    /// <param name="count">
    /// Number of elements to remove from the section.
    /// </param>
    public void RemoveRange(int start, int count, bool reloadTable = true)
    {
        this.RemoveRange(start, count, UITableViewRowAnimation.None, reloadTable);
    }

    /// <summary>
    /// Remove a range of elements from the section with the given animation.
    /// </summary>
    /// <param name="start">
    /// Starting position.
    /// </param>
    /// <param name="count">
    /// Number of elements to remove form the section.
    /// </param>
    /// <param name="anim">
    /// The animation to use while removing the elements.
    /// </param>
    public void RemoveRange(int start, int count, UITableViewRowAnimation anim, bool reloadTable = true)
    {
        if (start < 0 || start >= this.Elements.Count)
        {
            return;
        }

        if (count == 0)
        {
            return;
        }

        var root = this.Root;

        if (start + count > this.Elements.Count)
        {
            count = this.Elements.Count - start;
        }

        this.elements.RemoveRange(start, count);

        if (root == null)
        {
            return;
        }

        int sidx = root.IndexOf(this);
        var paths = new NSIndexPath[count];
        for (int i = 0; i < count; i++)
        {
            paths[i] = NSIndexPath.FromRowSection(start + i, sidx);
        }

        if (reloadTable)
        {
            root.DeleteRows(paths, anim);
        }
    }

    /// <summary>
    /// Clears all the elements in the Section.
    /// </summary>
    public void Clear(bool reloadTable = true)
    {
        this.elements.Clear();
        if (this.Root != null && reloadTable)
        {
            this.Root.ReloadData();
        }
    }

    private void InsertVisual(int idx, UITableViewRowAnimation anim, int count)
    {
        var root = this.Root as Root;

        if (root == null)
        {
            return;
        }

        int sidx = root.IndexOf(this);
        var paths = new NSIndexPath[count];
        for (int i = 0; i < count; i++)
        {
            paths[i] = NSIndexPath.FromRowSection(idx + i, sidx);
        }

        root.InsertRows(paths, anim);
    }

    internal void SetParent(Root? element) => this.Root = element;
}