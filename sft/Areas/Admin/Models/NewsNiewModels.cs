using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sft.Models;

namespace sft.Areas.Admin.Models
{
    public class HighlitedNewsNiewModels
    {
        public List<NewsArticle> Articles { get; set; }
        public List<NewsArticle> Higlighted { get; set; }

        public Guid First { get; set; }
        public Guid Second { get; set; }
        public Guid Thirt { get; set; }


        public List<SelectListItem> GetArticleSelector()
        {
            var list = new List<SelectListItem>();
            bool oneSelected = false;
            foreach (var article in Articles)
            {
                var tempSelected = Higlighted.Any(a => a.Id == article.Id) && !oneSelected;
                list.Add(new SelectListItem
                {
                    Text = article.Name,
                    Value = article.Id.ToString(),
                    Selected = tempSelected
                });
                if (tempSelected)
                {
                    if (First == Guid.Empty)
                    {
                        First = article.Id;
                    }
                    else if (Second == Guid.Empty)
                    {
                        Second = article.Id;
                    }
                    else if (Thirt == Guid.Empty)
                    {
                        Thirt = article.Id;
                    }
                    oneSelected = true;
                    Higlighted.Remove(Higlighted.FirstOrDefault(a => a.Id == article.Id));
                }
            }
            return list;
        }
    }

}