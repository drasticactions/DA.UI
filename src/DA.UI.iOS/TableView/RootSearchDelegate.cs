// <copyright file="RootSearchDelegate.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DA.UI.TableView;

public class RootSearchDelegate : UISearchBarDelegate
{
    private Root root;
    private Section[]? originalSections;
    private Element[][]? originalElements;

    public RootSearchDelegate(Root root)
    {
        this.root = root;
    }

    /// <inheritdoc/>
    public override void OnEditingStarted(UISearchBar searchBar)
    {
#if !TVOS
        searchBar.ShowsCancelButton = true;
        searchBar.BecomeFirstResponder();
#endif
        this.originalSections = this.root.Sections.ToArray();
        this.originalElements = new Element [this.originalSections.Length][];
        for (int i = 0; i < this.originalSections.Length; i++)
        {
            this.originalElements[i] = this.originalSections[i].Elements.ToArray();
        }
    }

    /// <inheritdoc/>
    public override void TextChanged(UISearchBar searchBar, string searchText)
    {
        this.Search(searchBar.Text ?? string.Empty);
    }

#if !TVOS
    /// <inheritdoc/>
    public override void CancelButtonClicked(UISearchBar searchBar)
    {
        searchBar.ShowsCancelButton = false;
        this.FinishSearch();
        searchBar.ResignFirstResponder();
    }
#endif

    /// <inheritdoc/>
    public override void SearchButtonClicked(UISearchBar searchBar)
    {
    }

    private void Search(string searchText)
    {
        if (this.originalSections == null)
        {
            return;
        }

        if (this.originalElements == null)
        {
            return;
        }

        var newSections = new List<Section>();

        for (int sidx = 0; sidx < this.originalSections.Length; sidx++)
        {
            Section? newSection = null;
            var section = this.originalSections[sidx];
            Element[] elements = this.originalElements[sidx];

            foreach (var t in elements)
            {
                if (t.Matches(searchText))
                {
                    if (newSection == null)
                    {
                        newSection = new Section(section.Caption)
                        {
                        };
                        newSections.Add(newSection);
                    }

                    newSection.Add(t);
                }
            }
        }

        this.root.Clear(false);
        foreach (var s in newSections)
        {
            this.root.Add(s, reloadTable: false);
        }

        this.root.ReloadData();
    }

    private void FinishSearch()
    {
        if (this.originalSections == null)
        {
            return;
        }

        this.root.Clear();
        foreach (var s in this.originalSections)
        {
            this.root.Add(s);
        }

        this.originalSections = null;
        this.originalElements = null;
    }
}