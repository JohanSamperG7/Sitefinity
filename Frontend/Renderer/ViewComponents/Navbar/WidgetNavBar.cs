using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Renderer.ViewModel;

namespace Renderer.ViewComponents.Navbar
{
    [SitefinityWidget]
    public class WidgetNavBar : ViewComponent
    {
        private readonly NavbarWidgetViewModel _viewModel;

        public WidgetNavBar(NavbarWidgetViewModel navbarWidgetViewModel)
        {
            _viewModel = navbarWidgetViewModel;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var pages = await _viewModel.GetAllPagesAsync();
            return View("Default", pages);
        }
    }

    public class NavbarEntity()
    {
        public string Name { get; set; } = string.Empty;
        public bool ShowLogo { get; set; }
    }
}
