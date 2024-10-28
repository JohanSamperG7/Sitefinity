using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Resources;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.RestSdk;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Client;
using Progress.Sitefinity.RestSdk.Dto;
using Renderer.ViewModel.Card;
using static Progress.Sitefinity.AspNetCore.Constants;

namespace ViewComponents.Card
{
    [SitefinityWidget]
    public class CardViewComponent : ViewComponent
    {
        private readonly IRestClient _restClient;
        private readonly IStyleClassesProvider _styleClassesProvider;

        public CardViewComponent(
            IRestClient restClient,
            IStyleClassesProvider styleClassesProvider
        )
        {
            _restClient = restClient;
            _styleClassesProvider = styleClassesProvider;
        }

        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<CardEntity> context)
        {
            CardViewModel model = new();

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            await GetImagesAsync(context, model);
            BuildSectionMargins(context, model);

            model.CardTitle = context.Entity.CardTitle;
            model.CardText = context.Entity.CardText;

            return this.View(context.Entity.ViewName, model);
        }

        private async Task GetImagesAsync(IViewComponentContext<CardEntity> context, CardViewModel model)
        {
            if (context.Entity.Image.HasSelectedItems())
            {
                CollectionResponse<ImageDto> imageResult = await _restClient
                    .GetItems<ImageDto>(new()
                    {
                        Type = RestClientContentTypes.Images
                    });

                if (imageResult.Items.Count > 0)
                {
                    model.Image = imageResult.Items[0];
                }
            }
        }

        private void BuildSectionMargins(IViewComponentContext<CardEntity> context, CardViewModel model)
        {
            switch (context.Entity.TextAlignment)
            {
                case TextAlignment.Center:
                    model.TextAlign = "center";
                    break;
                case TextAlignment.Right:
                    model.TextAlign = "right";
                    break;
                case TextAlignment.Left:
                    model.TextAlign = "left";
                    break;
                default:
                    model.TextAlign = "start";
                    break;
            }

            var margin = _styleClassesProvider.GetMarginsClasses(context.Entity.MarginStyle);
            if (!string.IsNullOrEmpty(margin))
            {
                model.SectionCss = margin;
            }
        }
    }

    public class LabelDto
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Language { get; set; }
    }

    public class CardEntity
    {
        private const string cardSectionName = "Card";

        [ContentSection(cardSectionName)]
        [Content(Type = KnownContentTypes.Images, AllowMultipleItemsSelection = false)]
        [Required]
        public MixedContentContext Image { get; set; }

        [ContentSection(cardSectionName)]
        [DisplayName("Titulo")]
        [Required]
        public string CardTitle { get; set; }

        [ContentSection(cardSectionName)]
        [DisplayName("Texto")]
        [DataType(customDataType: KnownFieldTypes.TextArea)]
        [Required]
        public string CardText { get; set; }

        [ContentSection(cardSectionName)]
        [DisplayName("Dirección del Texto")]
        [DefaultValue(TextAlignment.Left)]
        [Required]
        public TextAlignment TextAlignment { get; set; }

        [ContentSection(cardSectionName)]
        [DisplayName("Margén")]
        [TableView("Section")]
        public MarginStyle MarginStyle { get; set; }

        [ContentSection("Template")]
        [DisplayName("Plantilla")]
        [DefaultValue("Default")]
        [ViewSelector]
        public string ViewName { get; set; }
    }

    public enum TextAlignment
    {
        Center,
        Right,
        Left
    }

    public enum BoostrapColor
    {
        Primary,
        Secondary,
        Success,
        Info,
        Light,
        Dark,
        Link,
        Warning,
        Danger
    }
}
