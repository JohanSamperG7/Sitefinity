namespace Renderer.Models
{
    public class PageModel
    {
        public string Id { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string ViewUrl { get; set; } = default!;
        public string ParentId { get; set; } = default!;
        public bool ShowInNavigation { get; set; }
        public List<PageModel> Children { get; set; } = [];
    }
}
