// <copyright file="RootDataSource.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DA.UI.TableView;

/// <summary>
/// UITableView Element Data Source.
/// </summary>
public class RootDataSource : UITableViewDataSource
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RootDataSource"/> class.
    /// </summary>
    public RootDataSource()
    {
        this.Sections = new List<Section>();
    }

    /// <summary>
    /// Gets the list of sections.
    /// </summary>
    internal List<Section> Sections { get; }

    /// <inheritdoc/>
    public override nint NumberOfSections(UITableView tableView)
    {
        return this.Sections.Count;
    }

    /// <inheritdoc/>
    public override IntPtr RowsInSection(UITableView tableView, IntPtr section)
    {
        if (!this.Sections.Any())
        {
            return 0;
        }

        var s = this.Sections[(int)section];
        var count = s.Elements?.Count ?? 0;

        return count;
    }

    /// <inheritdoc/>
    public override string TitleForHeader(UITableView tableView, IntPtr section)
    {
        if (this.Sections.Any())
        {
            return this.Sections[(int)section].Caption ?? string.Empty;
        }

        return string.Empty;
    }

    /// <inheritdoc/>
    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
    {
        try
        {
            var section = this.Sections[(int)indexPath.Section];
            var element = section.Elements?[(int)indexPath.Row];
            if (element is Element cell)
            {
                cell.Layout();
                return cell;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        throw new Exception("Could not get cell.");
    }
}