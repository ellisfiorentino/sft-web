using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MvcSiteMapProvider;
using sft.Models;

namespace sft.Controllers
{
  [RoutePrefix("news")]
  public class NewsController : Controller
  {
    private ApplicationDbContext _db = ApplicationDbContext.Create();
    // GET: News
    [Route("")]
    public async Task<ActionResult> Index()
    {
      return View(await _db.NewsArticles.OrderByDescending(article => article.Created).ToListAsync());
    }

    [Route("{urlName}")]
    [MvcSiteMapNode(Title = "News", ParentKey = "News", DynamicNodeProvider = "sft.Helpers.NewsDynaicNodeProvider, sft")]
    public async Task<ActionResult> Details(string urlName)
    {
      var article = await _db.NewsArticles.FirstOrDefaultAsync(newsArticle => newsArticle.UrlName == urlName.ToLower());
      if (article == null)
      {
        return HttpNotFound();
      }
      return View(article);
    }

    [HttpPost, ValidateAntiForgeryToken, Route("comment/{articleId}")]
    public async Task<ActionResult> Comment(Comment comment, Guid articleId)
    {
      var article = await _db.NewsArticles.FirstOrDefaultAsync(newsArticle => newsArticle.Id == articleId);
      var uId = User.Identity.GetUserId();
      var user = await _db.Users.FirstOrDefaultAsync(applicationUser => applicationUser.Id == uId);
      comment.Id = Guid.NewGuid();
      comment.User = user.GetDisplayName();
      comment.UserId = new Guid(User.Identity.GetUserId());
      comment.Created = DateTime.Now;
      comment.Modified = DateTime.Now;
      comment.IsApproved = true;
      article.Comments.Add(comment);
      _db.Entry(article);
      await _db.SaveChangesAsync();
      return RedirectToAction("Details", new {urlName = article.UrlName});
    }
  }
}