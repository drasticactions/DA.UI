// <copyright file="UIButtonElement.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DA.UI.TableView.Elements;

public abstract class UIButtonElement : Element
{
    private readonly UIButton button;

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

    protected UIButton Button => this.button;
}