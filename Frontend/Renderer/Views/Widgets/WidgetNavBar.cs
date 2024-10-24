using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Renderer.ViewModel;

namespace Renderer.Views.Widgets
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
}
