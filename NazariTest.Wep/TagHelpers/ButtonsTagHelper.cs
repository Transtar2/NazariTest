using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NazariTest.Wep.TagHelpers
{
    [HtmlTargetElement("ButtonCloseModal")]
    public class ButtonCloseModalTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            output.Attributes.SetAttribute("class", "btn btn-dark");
            output.Attributes.SetAttribute("type", "button");
            output.Attributes.SetAttribute("data-bs-dismiss", "modal");
            output.Content.SetHtmlContent("<i class=\"ti-close\"></i> بستن");
        }
    }

    [HtmlTargetElement("ButtonSubmit")]
    public class ButtonSubmitTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            output.Attributes.SetAttribute("class", "btn btn-success");
            output.Attributes.SetAttribute("type", "submit");
            output.Content.SetHtmlContent("<i class=\"ti-save-alt\"></i> ذخیره");
        }
    }

    [HtmlTargetElement("ButtonAdd")]
    public class ButtonAddTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            output.Attributes.SetAttribute("class", "btn btn-primary");
            output.Attributes.SetAttribute("type", "button");
            output.Attributes.SetAttribute("data-bs-toggle", "modal");
            output.Attributes.SetAttribute("data-bs-target", "#add-model");
            output.Content.SetContent("جدید");
        }
    }
}
