using CarRental.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CarRental.WEB.Helpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
                                              PagingInfo pagingInfo,
                                              Func<int, string> pageUrl)
        {
            if (pagingInfo.CurrentPage > pagingInfo.TotalPages)
            {
                pagingInfo.CurrentPage = pagingInfo.TotalPages;
            }
            StringBuilder result = new StringBuilder();
            
            if (pagingInfo.TotalPages <= 6) //if we have less than 6 pages
            {
                for (int i = 1; i <= pagingInfo.TotalPages; i++)
                {
                    TagBuilder tag = new TagBuilder("a");
                    tag.MergeAttribute("href", pageUrl(i));
                    tag.InnerHtml = i.ToString();
                    if (i == pagingInfo.CurrentPage)
                    {
                        tag.AddCssClass("selected");
                        tag.AddCssClass("btn-primary ");

                    }
                    tag.AddCssClass("btn btn-default border");
                    result.Append(tag.ToString());
                }
            }
            else if (pagingInfo.CurrentPage <=6)  //If we are at the beginning
            {
                if(pagingInfo.CurrentPage != 1)
                {
                    TagBuilder tagc = new TagBuilder("a");
                    tagc.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage - 1));
                    tagc.InnerHtml = "<<";
                    tagc.AddCssClass("btn btn-default");
                    result.Append(tagc.ToString());
                }
                for (int i = 1; i <= 6; i++)
                {
                    TagBuilder tag = new TagBuilder("a");
                    tag.MergeAttribute("href",pageUrl(i));
                    tag.InnerHtml = i.ToString();
                    if (i == pagingInfo.CurrentPage)
                    {
                        tag.AddCssClass("selected");
                        tag.AddCssClass("btn-primary");
                    }
                    tag.AddCssClass("btn btn-default");
                    result.Append(tag.ToString());
                }
                TagBuilder tage = new TagBuilder("a");
                tage.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage + 1));
                tage.InnerHtml = ">>";
                tage.AddCssClass("btn btn-default");
                result.Append(tage.ToString());
            }
            else if (pagingInfo.TotalPages-pagingInfo.CurrentPage<6 )//if we are at the end
            {
                TagBuilder tagc = new TagBuilder("a");
                tagc.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage - 1));
                tagc.InnerHtml = "<<";
                tagc.AddCssClass("btn btn-default");
                result.Append(tagc.ToString());
                for (int i = pagingInfo.TotalPages-6; i <= pagingInfo.TotalPages; i++)
                {                  

                    TagBuilder tag = new TagBuilder("a");
                    tag.MergeAttribute("href", pageUrl(i));
                    tag.InnerHtml = i.ToString();
                    if (i == pagingInfo.CurrentPage)
                    {
                        tag.AddCssClass("selected");
                        tag.AddCssClass("btn-primary");
                    }
                    tag.AddCssClass("btn btn-default");
                    result.Append(tag.ToString());
                }
                if(pagingInfo.CurrentPage != pagingInfo.TotalPages)
                {
                    TagBuilder tage = new TagBuilder("a");
                    tage.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage + 1));
                    tage.InnerHtml = ">>";
                    tage.AddCssClass("btn btn-default");
                    result.Append(tage.ToString());
                }

            }
            else //ifwe are at the middle
            {
                TagBuilder tagc = new TagBuilder("a");
                tagc.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage - 1));
                tagc.InnerHtml = "<<";
                tagc.AddCssClass("btn btn-default");
                result.Append(tagc.ToString());

             
                for(int i = pagingInfo.CurrentPage-2; i < pagingInfo.CurrentPage + 2; i++)
                    {

                        TagBuilder tag = new TagBuilder("a");
                        tag.MergeAttribute("href", pageUrl(i));
                        tag.InnerHtml = i.ToString();
                        if (i == pagingInfo.CurrentPage)
                        {
                            tag.AddCssClass("selected");
                            tag.AddCssClass("btn-primary");
                        }
                        tag.AddCssClass("btn btn-default");
                        result.Append(tag.ToString());
                    }
                TagBuilder tage = new TagBuilder("a");
                tage.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage + 1));
                tage.InnerHtml = ">>";
                tage.AddCssClass("btn btn-default");
                result.Append(tage.ToString());
            }
            





            
            return MvcHtmlString.Create(result.ToString());
        }
    }
}