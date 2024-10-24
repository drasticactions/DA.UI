using DA.UI.Grid;
using Masonry;

namespace DA.UI.iOSApp;

public class GridViewController : UIViewController
{
    public GridViewController()
    {
        var grid = CreateGrid();
        grid.AddChild(CreateGrid(), 1, 1);

        View.AddSubview(grid);
        grid.TranslatesAutoresizingMaskIntoConstraints = false;
        grid.MakeConstraints(make =>
        {
            make.Top.EqualTo(View.SafeAreaLayoutGuideTop()).Offset(20);
            make.Left.EqualTo(View.SafeAreaLayoutGuideLeft());
            make.Right.EqualTo(View.SafeAreaLayoutGuideRight());
            make.Bottom.EqualTo(View.SafeAreaLayoutGuideBottom());
        });
    }

    private UIGrid CreateGrid()
    {
        var grid = new UIGrid()
        {
            ShowGridLines = true // Helpful for debugging
        };

        // Define columns
        grid.AddColumn(new GridDefinition(100, GridUnit.Pixel));
        grid.AddColumn(new GridDefinition(1, GridUnit.Star));
        grid.AddColumn(new GridDefinition(100, GridUnit.Pixel));

        // Define rows
        grid.AddRow(new GridDefinition(100, GridUnit.Pixel));
        grid.AddRow(new GridDefinition(1, GridUnit.Star));
        grid.AddRow(new GridDefinition(100, GridUnit.Pixel));

        var view1 = new UIView() { BackgroundColor = UIColor.Red };
        grid.AddChild(view1, 0, 0);
        var view2 = new UIView() { BackgroundColor = UIColor.Blue };
        grid.AddChild(view2, 0, 2);
        var view3 = new UIView() { BackgroundColor = UIColor.Green };
        grid.AddChild(view3, 2, 0);
        var view4 = new UIView() { BackgroundColor = UIColor.Yellow };
        grid.AddChild(view4, 2, 2);
        return grid;
    }
}