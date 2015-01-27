using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using sft.Models;
using sft.Models.ViewModels;

namespace sft.Controllers
{
  public class HomeController : Controller
  {
    private ApplicationDbContext _db = ApplicationDbContext.Create();
    public async Task<ActionResult> Index()
    {
      var vm = new HomeViewModel();
      vm.Featured = await _db.NewsArticles.Where(article => article.IsHighLighted).Take(3).Select(article => new FeatureViewModel
      {
        Header = article.Name,
        LinkLocation = "/news/" + article.UrlName,
        ImgLocation = article.MainPictureLocation,
        SmallText = article.Text.Replace("<p>", "").Replace("</p>", "").Substring(0, (article.Text.Length > 117 ? 117 : article.Text.Length)) + "...",
        SubHeader = article.Author,
        Comments = article.Comments.Count(c => c.IsApproved)
      }).ToListAsync();
      vm.FeatureRentals =
        await _db.Rentals.Where(rental => rental.IsHighLighted).Take(4).Select(rental => new FeatureRentalViewModel
        {
          LinkLocation = "/rentals/" + rental.UrlName,
          ImageLocation = !string.IsNullOrEmpty(rental.MainPictureLocation) ? rental.MainPictureLocation: "http://img1.wikia.nocookie.net/__cb20141028171337/pandorahearts/images/a/ad/Not_available.jpg"
        }).ToListAsync();

      return View(vm);
    }

    public async  Task<ActionResult> About()
    {
      var vm = new RightSideBarLayoutViewModel();
      vm.RightSideBar.Articles = await _db.NewsArticles.OrderByDescending(article => article.Created).Take(6).ToListAsync();
      return View(vm);
    }
  }
}