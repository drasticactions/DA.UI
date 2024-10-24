// <copyright file="NSGrid.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DA.UI.Grid;

public class NSGrid : NSView
{
    private List<GridDefinition> columnDefinitions = new List<GridDefinition>();
    private List<GridDefinition> rowDefinitions = new List<GridDefinition>();
    private Dictionary<NSView, GridChildProperties> childProperties = new Dictionary<NSView, GridChildProperties>();
    private bool showGridLines;

    public bool ShowGridLines
    {
        get => this.showGridLines;
        set
        {
            this.showGridLines = value;
            this.NeedsDisplay = true;
        }
    }

    public NSGrid()
    {
        this.WantsLayer = true;
    }

    public void AddColumn(GridDefinition definition)
    {
        this.columnDefinitions.Add(definition);
        this.NeedsLayout = true;
    }

    public void AddRow(GridDefinition definition)
    {
        this.rowDefinitions.Add(definition);
        this.NeedsLayout = true;
    }

    public void AddChild(NSView view, int row, int column, int rowSpan = 1, int columnSpan = 1,
        GridAlignment horizontalAlignment = GridAlignment.Stretch,
        GridAlignment verticalAlignment = GridAlignment.Stretch,
        NSEdgeInsets? margin = null)
    {
        if (row + rowSpan > this.rowDefinitions.Count || column + columnSpan > this.columnDefinitions.Count)
        {
            throw new ArgumentException("Row or column index (including span) out of range");
        }

        this.AddSubview(view);
        this.childProperties[view] = new GridChildProperties
        {
            Row = row,
            Column = column,
            RowSpan = rowSpan,
            ColumnSpan = columnSpan,
            Margin = margin ?? new NSEdgeInsets(0, 0, 0, 0),
            HorizontalAlignment = horizontalAlignment,
            VerticalAlignment = verticalAlignment,
        };
        this.NeedsLayout = true;
    }

    public override void DrawRect(CGRect dirtyRect)
    {
        base.DrawRect(dirtyRect);

        if (!this.ShowGridLines)
        {
            return;
        }

        using (var context = NSGraphicsContext.CurrentContext.CGContext)
        {
            context.SetStrokeColor(NSColor.Gray.CGColor);
            context.SetLineWidth(1);

            var columnWidths = this.CalculateGridDimensions(this.columnDefinitions, this.Bounds.Width);
            var rowHeights = this.CalculateGridDimensions(this.rowDefinitions, this.Bounds.Height);

            // Draw vertical lines
            float x = 0;
            foreach (var width in columnWidths)
            {
                var path = new CGPath();
                path.MoveToPoint(x, 0);
                path.AddLineToPoint(x, this.Bounds.Height);
                context.AddPath(path);
                context.StrokePath();
                x += width;
            }

            // Draw horizontal lines
            float y = 0;
            foreach (var height in rowHeights)
            {
                var path = new CGPath();
                path.MoveToPoint(0, y);
                path.AddLineToPoint(this.Bounds.Width, y);
                context.AddPath(path);
                context.StrokePath();
                y += height;
            }
        }
    }

    public override void Layout()
    {
        base.Layout();

        var bounds = this.Bounds;
        var columnWidths = this.CalculateGridDimensions(this.columnDefinitions, bounds.Width);
        var rowHeights = this.CalculateGridDimensions(this.rowDefinitions, bounds.Height);

        foreach (var child in this.Subviews)
        {
            if (this.childProperties.TryGetValue(child, out var props))
            {
                // Calculate cell bounds
                nfloat cellWidth = columnWidths.Skip(props.Column).Take(props.ColumnSpan).Sum();
                nfloat cellHeight = rowHeights.Skip(props.Row).Take(props.RowSpan).Sum();
                nfloat cellX = columnWidths.Take(props.Column).Sum();
                nfloat cellY = rowHeights.Take(props.Row).Sum();

                // Apply margins
                cellX += props.Margin.Left;
                cellY += props.Margin.Bottom;
                cellWidth -= props.Margin.Left + props.Margin.Right;
                cellHeight -= props.Margin.Top + props.Margin.Bottom;

                // Get desired size of the view
                var desiredSize = child.FittingSize;
                var finalFrame = default(CGRect);

                // Apply horizontal alignment
                switch (props.HorizontalAlignment)
                {
                    case GridAlignment.Start:
                        finalFrame.Width = (nfloat)Math.Min(desiredSize.Width, cellWidth);
                        finalFrame.X = cellX;
                        break;
                    case GridAlignment.Center:
                        finalFrame.Width = (nfloat)Math.Min(desiredSize.Width, cellWidth);
                        finalFrame.X = cellX + ((cellWidth - finalFrame.Width) / 2);
                        break;
                    case GridAlignment.End:
                        finalFrame.Width = (nfloat)Math.Min(desiredSize.Width, cellWidth);
                        finalFrame.X = cellX + cellWidth - finalFrame.Width;
                        break;
                    case GridAlignment.Stretch:
                        finalFrame.Width = cellWidth;
                        finalFrame.X = cellX;
                        break;
                }

                // Apply vertical alignment
                switch (props.VerticalAlignment)
                {
                    case GridAlignment.Start:
                        finalFrame.Height = (nfloat)Math.Min(desiredSize.Height, cellHeight);
                        finalFrame.Y = bounds.Height - cellY - cellHeight;
                        break;
                    case GridAlignment.Center:
                        finalFrame.Height = (nfloat)Math.Min(desiredSize.Height, cellHeight);
                        finalFrame.Y = bounds.Height - cellY - cellHeight + ((cellHeight - finalFrame.Height) / 2);
                        break;
                    case GridAlignment.End:
                        finalFrame.Height = (nfloat)Math.Min(desiredSize.Height, cellHeight);
                        finalFrame.Y = bounds.Height - cellY - finalFrame.Height;
                        break;
                    case GridAlignment.Stretch:
                        finalFrame.Height = cellHeight;
                        finalFrame.Y = bounds.Height - cellY - cellHeight;
                        break;
                }

                child.Frame = finalFrame;
            }
        }
    }

    private float[] CalculateGridDimensions(List<GridDefinition> definitions, nfloat totalSpace)
    {
        var results = new float[definitions.Count];
        var remainingSpace = (float)totalSpace;
        var unallocatedIndices = new List<int>();
        var totalStars = definitions.Where(d => d.Unit == GridUnit.Star).Sum(d => d.Size);

        // First pass: Handle fixed sizes and calculate auto sizes
        for (int i = 0; i < definitions.Count; i++)
        {
            var def = definitions[i];
            if (def.Unit == GridUnit.Pixel)
            {
                results[i] = Math.Min(def.MaxSize, Math.Max(def.MinSize, def.Size));
                remainingSpace -= results[i];
            }
            else if (def.Unit == GridUnit.Auto)
            {
                // Calculate auto size based on content
                float autoSize = this.CalculateAutoSize(i, def.Unit == GridUnit.Auto);
                autoSize = Math.Min(def.MaxSize, Math.Max(def.MinSize, autoSize));
                results[i] = autoSize;
                remainingSpace -= autoSize;
            }
            else
            {
                unallocatedIndices.Add(i);
            }
        }

        // Second pass: Handle star sizes
        if (totalStars > 0 && remainingSpace > 0)
        {
            var starUnit = remainingSpace / totalStars;
            foreach (var i in unallocatedIndices)
            {
                var def = definitions[i];
                if (def.Unit == GridUnit.Star)
                {
                    var starSize = starUnit * def.Size;
                    results[i] = Math.Min(def.MaxSize, Math.Max(def.MinSize, starSize));
                }
            }
        }

        return results;
    }

    private float CalculateAutoSize(int index, bool isRow)
    {
        float maxSize = 0;

        foreach (var kvp in this.childProperties)
        {
            var view = kvp.Key;
            var props = kvp.Value;

            if (isRow && props.Row <= index && index < props.Row + props.RowSpan)
            {
                maxSize = Math.Max(maxSize, (float)view.FittingSize.Height +
                                            (float)(props.Margin.Top + props.Margin.Bottom));
            }
            else if (!isRow && props.Column <= index && index < props.Column + props.ColumnSpan)
            {
                maxSize = Math.Max(maxSize, (float)view.FittingSize.Width +
                                            (float)(props.Margin.Left + props.Margin.Right));
            }
        }

        return maxSize;
    }
}