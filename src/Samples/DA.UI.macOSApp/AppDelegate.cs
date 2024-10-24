using DA.UI.Grid;

namespace DA.UI.macOSApp;

[Register("AppDelegate")]
public class AppDelegate : NSApplicationDelegate
{
    public override void DidFinishLaunching(NSNotification notification)
    {
        var window = new NSWindow(new CGRect(0, 0, 800, 600), NSWindowStyle.Titled | NSWindowStyle.Resizable | NSWindowStyle.Closable | NSWindowStyle.Miniaturizable, NSBackingStore.Buffered, false);
        window.Title = "Hello World";
        
        var grid = CreateGrid();
        grid.AddChild(CreateGrid(), 1, 1);
        window.ContentView = grid;
        
        window.Center();
        window.MakeKeyAndOrderFront(window);
    }

    private NSGrid CreateGrid()
    {
        var grid = new NSGrid()
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
        var view1 = new NSView() { };
        view1.WantsLayer = true;
        view1.Layer.BackgroundColor = NSColor.Red.CGColor;
        grid.AddChild(view1, 0, 0);
        var view2 = new NSView() { };
        view2.WantsLayer = true;
        view2.Layer.BackgroundColor = NSColor.Blue.CGColor;
        grid.AddChild(view2, 0, 2);
        var view3 = new NSView() { };
        view3.WantsLayer = true;
        view3.Layer.BackgroundColor = NSColor.Green.CGColor;
        grid.AddChild(view3, 2, 0);
        var view4 = new NSView() { };
        view4.WantsLayer = true;
        view4.Layer.BackgroundColor = NSColor.Yellow.CGColor;
        grid.AddChild(view4, 2, 2);
        return grid;
    }

    public override void WillTerminate(NSNotification notification)
    {
        // Insert code here to tear down your application
    }
}