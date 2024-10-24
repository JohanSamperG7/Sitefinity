using AngleSharp.Common;
using Renderer.Models;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Client;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;
using Progress.Sitefinity.Clients.LayoutService.Dto;

namespace Renderer.ViewModel
{
    public class NavbarWidgetViewModel
    {
        private readonly IRestClient _restClient;

        public NavbarWidgetViewModel(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<ICollection<PageModel>> GetAllPagesAsync()
        {
            CollectionResponse<PageModel> pagesResponse = await _restClient
                .GetItems<PageModel>(new GetAllArgs()
                {
                    Type = RestClientContentTypes.Pages
                });

            Dictionary<string, PageModel> pageDict = pagesResponse.Items.ToDictionary(page => page.Id);
            List<PageModel> rootPages = pagesResponse.Items
                .Where(
                    page => string.IsNullOrEmpty(page.ParentId) || 
                    !pagesResponse.Items.Any(pageResponse => pageResponse.Id == page.ParentId)
                ).ToList();

            foreach (var page in pagesResponse.Items.Where(pages => !rootPages.Any(root => root.Id == pages.Id)))
            {
                PageModel parent = rootPages.FirstOrDefault(root => root.Id == page.ParentId)!;
                parent.Children ??= new();
                parent.Children.Add(page);
            }

            return rootPages;
        }
    }

}
