namespace CustomTagHelper.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Text;
    using Microsoft.AspNetCore.Mvc.Rendering;

    [HtmlTargetElement("employee-details")]
    public class EmployeeDetailTagHelper : TagHelper
    {
        [HtmlAttributeName("for-name")]
        public ModelExpression EmployeeName { get; set; }
        [HtmlAttributeName("for-designation")]
        public ModelExpression Designation { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "EmployeeDetails";
            output.TagMode = TagMode.StartTagAndEndTag;

            var sb = new StringBuilder();
            sb.AppendFormat("<span>Name: {0}</span> <br/>", this.EmployeeName.Model);
            sb.AppendFormat("<span>Designation: {0}</span>", this.Designation.Model);

            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }
}
