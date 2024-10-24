// <copyright file="GridChildProperties.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DA.UI.Grid;

public class GridChildProperties
{
    public int Row { get; set; }
    public int Column { get; set; }
    public int RowSpan { get; set; } = 1;
    public int ColumnSpan { get; set; } = 1;
    public UIEdgeInsets Margin { get; set; }
    public GridAlignment HorizontalAlignment { get; set; } = GridAlignment.Stretch;
    public GridAlignment VerticalAlignment { get; set; } = GridAlignment.Stretch;

    public GridChildProperties()
    {
        Margin = new UIEdgeInsets(0, 0, 0, 0);
    }
}