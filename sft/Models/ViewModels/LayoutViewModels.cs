using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sft.Models.ViewModels
{
  public class RightSideBarLayoutViewModel
  {
    public object MainBodyViewModel { get; set; }
    public RightSideBarViewModel RightSideBar { get; set; }

    public RightSideBarLayoutViewModel()
    {
      RightSideBar = new RightSideBarViewModel();
    }

  }

  public class RightSideBarViewModel
  {
    public List<NewsArticle> Articles { get; set; }
  }

    public class FooterModel
    {
        public static List<NewsArticle> GetArticles()
        {
            var list = new List<NewsArticle>();
            using (var db = ApplicationDbContext.Create())
            {
                list = db.NewsArticles.OrderByDescending(article => article.Created).Take(5).ToList();
            }
            return list;
        }

        public static List<Rental> GetRentals()
        {
            var list = new List<Rental>();
            using (var db = ApplicationDbContext.Create())
            {
                list = db.Rentals.Where(rental => rental.Available).OrderBy(rental => rental.Name).Take(5).ToList();
            }
            return list;
        } 
    }

}