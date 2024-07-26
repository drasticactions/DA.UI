// <copyright file="StringElement.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DA.UI.TableView;

namespace DA.UI.iOSApp;

public class StringElement : BaseElement<string>
{
    public StringElement(string value)
        : base(value, "StringElement", UITableViewCellStyle.Default)
    {
    }

    public override void Layout()
    {
        var contentConfiguration = UIListContentConfiguration.CellConfiguration;
        contentConfiguration.Text = this.Value;
        contentConfiguration.TextProperties.NumberOfLines = 0;
        contentConfiguration.TextProperties.LineBreakMode = UILineBreakMode.WordWrap;
        this.ContentConfiguration = contentConfiguration;
    }
}