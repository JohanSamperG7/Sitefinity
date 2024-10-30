using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace Renderer.ViewComponents.Counter
{
    [SitefinityWidget]
    public class CounterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("Counter");
        }
    }
}
