using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using intexii.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace intexii.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-blah")]
    public class PaginationTagHelper : TagHelper
    {
        //dynamically create the page links

        private IUrlHelperFactory uhf;

        public PaginationTagHelper(IUrlHelperFactory temp)
        {
            uhf = temp;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext vc { get; set; }
        //diff
        public PageInfo PageBlah { get; set; }
        public string PageAction { get; set; }
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }
        public override void Process(TagHelperContext thc, TagHelperOutput tho)
        {
            IUrlHelper uh = uhf.GetUrlHelper(vc);

            TagBuilder final = new TagBuilder("div");

            for (int i = 1; i <= PageBlah.TotalPages; i++)
            {
                TagBuilder tb = new TagBuilder("a");

                if (i == 1 || i == PageBlah.CurrentPage || i == PageBlah.TotalPages || (i >= PageBlah.CurrentPage - 2 && i <= PageBlah.CurrentPage + 2))
                {

                    tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i });
                    tb.InnerHtml.Append(i.ToString());
                }


                else if (i == PageBlah.CurrentPage - 3 || i == PageBlah.CurrentPage + 3)
                {
                    //display ellipsis (...) to indicate skipped pages
                    tb.InnerHtml.Append("...");
                }

                final.InnerHtml.AppendHtml(tb);
            }

            tho.Content.AppendHtml(final.InnerHtml);

        }
    }
}