using Progress.Sitefinity.RestSdk.Dto;

namespace Renderer.ViewModel.Card
{
    public class CardViewModel
    {
        public string CardTitle { get; set; }
        public string CardText { get; set; }
        public ImageDto Image { get; set; }
        public string TextAlign { get; set; }
        public string SectionCss { get; set; }
    }
}
