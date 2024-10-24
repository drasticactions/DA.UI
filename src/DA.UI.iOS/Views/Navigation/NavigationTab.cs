using ObjCRuntime;

namespace DA.UI.Views.Navigation;

public sealed class NavigationTab : UITab, INavigationTab
{
    public NavigationTab(string title, UIImage? image, string identifier, Func<UITab, UIViewController>? viewControllerProvider)
        : base(title, image, identifier, viewControllerProvider)
    {
    }
}