// <copyright file="UIGrid.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using CoreAnimation;

namespace DA.UI.Grid;

[Register("iOSGrid")]
public class UIGrid : UIView
{
    private List<GridDefinition> columnDefinitions = new List<GridDefinition>();
    private List<GridDefinition> rowDefinitions = new List<GridDefinition>();
    private Dictionary<UIView, GridChildProperties> childProperties = new Dictionary<UIView, GridChildProperties>();
    private bool showGridLines;
    private CAShapeLayer gridLinesLayer;

    public bool ShowGridLines
    {
        get => showGridLines;
        set
        {
            showGridLines = value;
            if (showGridLines && gridLinesLayer == null)
            {
                gridLinesLayer = new CAShapeLayer();
                Layer.AddSublayer(gridLinesLayer);
            }
            SetNeedsLayout();
        }
    }

    public UIGrid()
    {
        Initialize();
    }

    public UIGrid(CGRect frame) : base(frame)
    {
        Initialize();
    }

    protected UIGrid(IntPtr handle) : base(handle)
    {
        Initialize();
    }

    private void Initialize()
    {
        BackgroundColor = UIColor.Clear;
        AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
    }

    public void AddColumn(GridDefinition definition)
    {
        columnDefinitions.Add(definition);
        SetNeedsLayout();
    }

    public void AddRow(GridDefinition definition)
    {
        rowDefinitions.Add(definition);
        SetNeedsLayout();
    }

    public void AddChild(UIView view, int row, int column, int rowSpan = 1, int columnSpan = 1,
        GridAlignment horizontalAlignment = GridAlignment.Stretch,
        GridAlignment verticalAlignment = GridAlignment.Stretch,
        UIEdgeInsets? margin = null)
    {
        if (row + rowSpan > rowDefinitions.Count || column + columnSpan > columnDefinitions.Count)
            throw new ArgumentException("Row or column index (including span) out of range");

        AddSubview(view);
        childProperties[view] = new GridChildProperties
        {
            Row = row,
            Column = column,
            RowSpan = rowSpan,
            ColumnSpan = columnSpan,
            Margin = margin ?? new UIEdgeInsets(0, 0, 0, 0),
            HorizontalAlignment = horizontalAlignment,
            VerticalAlignment = verticalAlignment
        };
        SetNeedsLayout();
    }

    public override void LayoutSubviews()
    {
        base.LayoutSubviews();

        var bounds = Bounds;
        var columnWidths = CalculateGridDimensions(columnDefinitions, bounds.Width);
        var rowHeights = CalculateGridDimensions(rowDefinitions, bounds.Height);

        foreach (var child in Subviews)
        {
            if (childProperties.TryGetValue(child, out var props))
            {
                // Calculate cell bounds
                nfloat cellWidth = columnWidths.Skip(props.Column).Take(props.ColumnSpan).Sum();
                nfloat cellHeight = rowHeights.Skip(props.Row).Take(props.RowSpan).Sum();
                nfloat cellX = columnWidths.Take(props.Column).Sum();
                nfloat cellY = rowHeights.Take(props.Row).Sum();

                // Apply margins
                cellX += props.Margin.Left;
                cellY += props.Margin.Top;
                cellWidth -= (props.Margin.Left + props.Margin.Right);
                cellHeight -= (props.Margin.Top + props.Margin.Bottom);

                // Get desired size of the view
                var sizeThatFits = child.SizeThatFits(new CGSize(cellWidth, cellHeight));
                var finalFrame = new CGRect();

                // Apply horizontal alignment
                switch (props.HorizontalAlignment)
                {
                    case GridAlignment.Start:
                        finalFrame.Width = (nfloat)Math.Min(sizeThatFits.Width, cellWidth);
                        finalFrame.X = cellX;
                        break;
                    case GridAlignment.Center:
                        finalFrame.Width = (nfloat)Math.Min(sizeThatFits.Width, cellWidth);
                        finalFrame.X = cellX + (cellWidth - finalFrame.Width) / 2;
                        break;
                    case GridAlignment.End:
                        finalFrame.Width = (nfloat)Math.Min(sizeThatFits.Width, cellWidth);
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
                        finalFrame.Height = (nfloat)Math.Min(sizeThatFits.Height, cellHeight);
                        finalFrame.Y = cellY;
                        break;
                    case GridAlignment.Center:
                        finalFrame.Height = (nfloat)Math.Min(sizeThatFits.Height, cellHeight);
                        finalFrame.Y = cellY + (cellHeight - finalFrame.Height) / 2;
                        break;
                    case GridAlignment.End:
                        finalFrame.Height = (nfloat)Math.Min(sizeThatFits.Height, cellHeight);
                        finalFrame.Y = cellY + cellHeight - finalFrame.Height;
                        break;
                    case GridAlignment.Stretch:
                        finalFrame.Height = cellHeight;
                        finalFrame.Y = cellY;
                        break;
                }

                child.Frame = finalFrame;
            }
        }

        if (ShowGridLines)
        {
            DrawGridLines(columnWidths, rowHeights);
        }
    }

    private void DrawGridLines(float[] columnWidths, float[] rowHeights)
    {
        var path = new UIBezierPath();

        // Draw vertical lines
        float x = 0;
        foreach (var width in columnWidths)
        {
            path.MoveTo(new CGPoint(x, 0));
            path.AddLineTo(new CGPoint(x, Bounds.Height));
            x += width;
        }
        // Draw final vertical line
        path.MoveTo(new CGPoint(Bounds.Width, 0));
        path.AddLineTo(new CGPoint(Bounds.Width, Bounds.Height));

        // Draw horizontal lines
        float y = 0;
        foreach (var height in rowHeights)
        {
            path.MoveTo(new CGPoint(0, y));
            path.AddLineTo(new CGPoint(Bounds.Width, y));
            y += height;
        }
        // Draw final horizontal line
        path.MoveTo(new CGPoint(0, Bounds.Height));
        path.AddLineTo(new CGPoint(Bounds.Width, Bounds.Height));

        gridLinesLayer.Frame = Bounds;
        gridLinesLayer.Path = path.CGPath;
        gridLinesLayer.StrokeColor = UIColor.LightGray.CGColor;
        gridLinesLayer.LineWidth = 1;
        gridLinesLayer.FillColor = null;
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
                float autoSize = CalculateAutoSize(i, def.Unit == GridUnit.Auto);
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

        foreach (var kvp in childProperties)
        {
            var view = kvp.Key;
            var props = kvp.Value;

            if (isRow && props.Row <= index && index < props.Row + props.RowSpan)
            {
                var size = view.SizeThatFits(CGSize.Empty);
                maxSize = Math.Max(maxSize, (float)size.Height +
                                            (float)(props.Margin.Top + props.Margin.Bottom));
            }
            else if (!isRow && props.Column <= index && index < props.Column + props.ColumnSpan)
            {
                var size = view.SizeThatFits(CGSize.Empty);
                maxSize = Math.Max(maxSize, (float)size.Width +
                                            (float)(props.Margin.Left + props.Margin.Right));
            }
        }

        return maxSize;
    }
}