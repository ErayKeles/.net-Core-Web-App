using Microsoft.AspNetCore.Razor.TagHelpers;


    [HtmlTargetElement("bold")]
    [HtmlTargetElement(Attributes = "bold")]
    [HtmlTargetElement("p", ParentTag = "boldThemAll")]
    public class BoldTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll("bold");
            output.PreContent.SetHtmlContent("<strong>");
            output.PostContent.SetHtmlContent("</strong>");
        }
    }
