// <copyright file="Root.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DA.UI.TableView;

/// <summary>
/// UITableView that can be used with DA.Dialog as a root element.
/// </summary>
public class Root : UITableView
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Root"/> class.
    /// </summary>
    /// <param name="dataSource">The data source for the table view, providing the content. If null, a default <see cref="RootDataSource"/> is created.</param>
    /// <param name="style">The style of the table view. Defaults to <see cref="UITableViewStyle.Plain"/>.</param>
    public Root(
        RootDataSource? dataSource = null,
        UITableViewStyle style = UITableViewStyle.Plain)
        : base(CGRect.Null, style)
    {
        this.RootDataSource = dataSource ?? new RootDataSource();
        this.DataSource = (UITableViewDataSource)this.RootDataSource!;
    }

    /// <inheritdoc/>
    public Element? Parent => null;

    public RootDataSource RootDataSource { get; }

    public IReadOnlyList<Section> Sections => this.RootDataSource.Sections;

    /// <summary>
    /// Adds a new section to the table view.
    /// </summary>
    /// <param name="section">The section to add. Must not be null.</param>
    /// <remarks>
    /// This method adds a given section to the Sections list. If the section is of type
    /// <see cref="UITableViewCellElementSection"/>, it also sets the parent of the section to this table view.
    /// After adding the section, it updates the table view to include the new section by inserting it
    /// with no animation.
    /// </remarks>
    public void Add(Section section)
    {
        ArgumentNullException.ThrowIfNull(section);
        this.RootDataSource.Sections.Add(section);
        if (section is Section sectionCell)
        {
            sectionCell.SetParent(this);
        }

        this.InsertSections(this.MakeIndexSet(this.Sections.Count - 1, 1), UITableViewRowAnimation.None);
    }

    /// <summary>
    /// Removes a specified section from the table view.
    /// </summary>
    /// <param name="s">The section to remove.</param>
    /// <remarks>
    /// This method first checks if the provided section is null, throwing an <see cref="ArgumentNullException"/> if it is.
    /// It then attempts to find the index of the given section in the Sections list. If the section is found,
    /// it is removed using the <see cref="RemoveAt"/> method with a fade animation. If the section is not found,
    /// the method exits without making any changes.
    /// </remarks>
    public void Remove(Section s)
    {
        ArgumentNullException.ThrowIfNull(s);

        int idx = this.RootDataSource.Sections.IndexOf(s);
        if (idx == -1)
        {
            return;
        }

        this.RemoveAt(idx, UITableViewRowAnimation.Fade);
    }

    /// <summary>
    /// Removes a section at a specified location.
    /// </summary>
    public void RemoveAt(int idx)
    {
        this.RemoveAt(idx, UITableViewRowAnimation.Fade);
    }

    /// <summary>
    /// Clears all sections from the table view and reloads the data.
    /// </summary>
    /// <remarks>
    /// This method removes all sections from the table view by clearing the Sections list.
    /// After clearing, it calls ReloadData to refresh the table view, ensuring that the UI
    /// accurately reflects the current state of the Sections list, which is now empty.
    /// </remarks>
    public void Clear()
    {
        this.RootDataSource.Sections.Clear();
        this.ReloadData();
    }

    /// <summary>
    /// Removes a section at a specified location using the specified animation.
    /// </summary>
    /// <param name="idx">
    /// A <see cref="int"/>.
    /// </param>
    /// <param name="anim">
    /// A <see cref="UITableViewRowAnimation"/>.
    /// </param>
    public void RemoveAt(int idx, UITableViewRowAnimation anim)
    {
        if (idx < 0 || idx >= this.Sections.Count)
        {
            return;
        }

        this.RootDataSource.Sections.RemoveAt(idx);

        this.DeleteSections(NSIndexSet.FromIndex(idx), anim);
    }

    /// <summary>
    /// Finds the index of a given <see cref="Section"/> within the Sections list.
    /// </summary>
    /// <param name="target">The <see cref="Section"/> to find the index of.</param>
    /// <returns>The zero-based index of the target section if found; otherwise, -1.</returns>
    /// <remarks>
    /// This method iterates through the Sections list, comparing each section with the target.
    /// If the target is found, its index is returned. If the target is not found by the end of the list,
    /// the method returns -1, indicating the target is not present in the list.
    /// </remarks>
    internal int IndexOf(Section target)
    {
        int idx = 0;
        foreach (var s in this.Sections)
        {
            if (s == target)
            {
                return idx;
            }

            idx++;
        }

        return -1;
    }

    private NSIndexSet MakeIndexSet(int start, int count)
    {
        NSRange range;
        range.Location = start;
        range.Length = count;
        return NSIndexSet.FromNSRange(range);
    }
}