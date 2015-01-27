using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using sft.Areas.Admin.Helpers;
using sft.Areas.Admin.Models;
using sft.Helpers;
using sft.Models;

namespace sft.Areas.Admin.Controllers
{
    [Authorize]
    public class AdminNewsController : Controller
    {
        private ApplicationDbContext _db = ApplicationDbContext.Create();
        // GET: Admin/AdminNews
        public async Task<ActionResult> Index()
        {
            if (!User.IsAuthorized("news"))
            {
                return RedirectToAction("Index", "AdminHome");
            }
            return View(await _db.NewsArticles.ToListAsync());
        }
        public async Task<ActionResult> ChooseHiglighted()
        {
            if (!User.IsAuthorized("news"))
            {
                return RedirectToAction("Index", "AdminHome");
            }
            var vm = new HighlitedNewsNiewModels
            {
                Articles = await _db.NewsArticles.ToListAsync()
            };
            vm.Higlighted = vm.Articles.Where(r => r.IsHighLighted).ToList();
            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> ChooseHiglighted(HighlitedNewsNiewModels model)
        {
            if (!User.IsAuthorized("news"))
            {
                return RedirectToAction("Index", "AdminHome");
            }
            var first = await _db.NewsArticles.FirstOrDefaultAsync(r => r.Id == model.First);
            var second = await _db.NewsArticles.FirstOrDefaultAsync(r => r.Id == model.Second);
            var thirt = await _db.NewsArticles.FirstOrDefaultAsync(r => r.Id == model.Thirt);

            await _db.Database.ExecuteSqlCommandAsync("UPDATE NewsArticles SET [IsHighLighted] = 0");
            first.IsHighLighted = true;
            second.IsHighLighted = true;
            thirt.IsHighLighted = true;

            _db.Entry(first).State = EntityState.Modified;
            _db.Entry(second).State = EntityState.Modified;
            _db.Entry(thirt).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return RedirectToAction("ChooseHiglighted");
        }
        public async Task<ActionResult> ListComments(Guid id)
        {
            if (!User.IsAuthorized("news"))
            {
                return RedirectToAction("Index", "AdminHome");
            }
            return View(await _db.NewsArticles.FindAsync(id));
        }

        public async Task<HttpStatusCodeResult> ToggleComment(Guid id)
        {
            if (!User.IsAuthorized("news"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "Your not allowed to do this");
            }
            var comment = await _db.Comments.FindAsync(id);
            if (comment == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            if (comment.IsApproved)
            {
                comment.IsApproved = false;
            }
            else
            {
                comment.IsApproved = true;
            }
            _db.Entry(comment).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }


        public async Task<ActionResult> Edit(Guid id)
        {
            if (!User.IsAuthorized("news"))
            {
                return RedirectToAction("Index", "AdminHome");
            }
            return View(await _db.NewsArticles.FindAsync(id));
        }

        public ActionResult Create()
        {
            if (!User.IsAuthorized("news"))
            {
                return RedirectToAction("Index", "AdminHome");
            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(NewsArticle article)
        {
            if (!User.IsAuthorized("news"))
            {
                return RedirectToAction("Index", "AdminHome");
            }
            if (ModelState.IsValid)
            {
                article.Modified = DateTime.Now;
                var uid = User.Identity.GetUserId();
                var user = await _db.Users.FirstOrDefaultAsync(applicationUser => applicationUser.Id == uid);
                article.Editor = user.GetDisplayName();
                article.EditorId = new Guid(user.Id);
                _db.Entry(article).State = EntityState.Modified;
                await _db.SaveChangesAsync();
            }
            return View(article);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NewsArticle article)
        {
            if (!User.IsAuthorized("news"))
            {
                return RedirectToAction("Index", "AdminHome");
            }
            if (ModelState.IsValid)
            {
                article.Created = DateTime.Now;
                article.Modified = DateTime.Now;
                var uid = User.Identity.GetUserId();
                var user = await _db.Users.FirstOrDefaultAsync(applicationUser => applicationUser.Id == uid);
                article.Author = user.GetDisplayName();
                article.AuthorId = new Guid(user.Id);
                article.Id = Guid.NewGuid();
                article.UrlName = article.Name.ConvertToUrlSafeString();
                _db.NewsArticles.Add(article);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            if (!User.IsAuthorized("news"))
            {
                return RedirectToAction("Index", "AdminHome");
            }
            _db.NewsArticles.Remove(await _db.NewsArticles.FindAsync(id));
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}