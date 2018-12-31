using Microsoft.AspNetCore.Razor.TagHelpers;

namespace PeopleApp.Common
{
    public class StarsTagHelper : TagHelper
    {
        public int Level { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i < Level)
                {

                    output.Content.AppendHtml("<i class='fa fa-star' style='color:#ffd800'></i>");
                }
                else
                {
                    output.Content.AppendHtml("<i class='fa fa-star-o'></i>");
                }
            }
        }
    }
}
