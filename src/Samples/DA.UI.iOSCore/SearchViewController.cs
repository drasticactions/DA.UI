using Bogus;
using DA.UI.TableView;
using DA.UI.TableView.Elements;

namespace DA.UI.iOSApp;

public class SearchViewController : UITableViewController
{
    private Faker<string> fakerParagraphs = new Faker<string>().CustomInstantiator(f => f.Lorem.Paragraphs(1));
    private RootSearchDelegate searchDelegate;
    private UISearchBar searchBar;

    public SearchViewController()
    {
        this.Title = "Search";
        var paragraphElements = new List<Element>();
        for (int i = 0; i < 100; i++)
        {
            paragraphElements.Add(new StringElement(this.fakerParagraphs.Generate()));
        }

        var searchSection = new Section("Search");
        foreach (var element in paragraphElements)
        {
            searchSection.Add(element);
        }

        this.TableView = new Root()
        {
            searchSection,
        };

        this.searchDelegate = new RootSearchDelegate((Root)this.TableView);
        this.searchBar = new UISearchBar();
        this.searchBar.Delegate = this.searchDelegate;
    }

    public override UIView GetViewForHeader(UITableView tableView, IntPtr section)
    {
        return this.searchBar;
    }

    public override nfloat GetHeightForHeader(UITableView tableView, nint section)
    {
        return 44;
    }
}