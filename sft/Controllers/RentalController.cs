using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MvcSiteMapProvider;
using sft.Models;
using sft.Models.ViewModels;

namespace sft.Controllers
{
  [RoutePrefix("rentals")]
  public class RentalController : Controller
  {
    private ApplicationDbContext _db = ApplicationDbContext.Create();
    //
    // GET: /Rental/
    [Route("")]
    public async Task<ActionResult> Index(int page = 1)
    {
      var rentals = await _db.Rentals.OrderBy(rental => rental.Available).ThenBy(rental => rental.Name).ToListAsync();
      return View(new RentalIndexViewModel
      {
        Page = page,
        Total = rentals.Count,
        Rentals = rentals.Skip((page -1) * 9).Take(9).ToList()
      });
    }

    [Route("{urlName}")]
    [MvcSiteMapNode(Title = "Rentals", ParentKey = "Rentals", DynamicNodeProvider = "sft.Helpers.RentalsDynaicNodeProvider, sft")]
    public async Task<ActionResult> Details(string urlName)
    {
      return View(await _db.Rentals.FirstOrDefaultAsync(rental => rental.UrlName == urlName));
    }
  }
}