// <copyright file="GridDefinition.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DA.UI.Grid;

public class GridDefinition
{
    public float Size { get; set; }

    public GridUnit Unit { get; set; }

    public float MinSize { get; set; }

    public float MaxSize { get; set; }

    public GridDefinition(float size, GridUnit unit = GridUnit.Pixel, float minSize = 0, float maxSize = float.MaxValue)
    {
        this.Size = size;
        this.Unit = unit;
        this.MinSize = minSize;
        this.MaxSize = maxSize;
    }
}