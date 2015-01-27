using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using sft.Areas.Admin.Helpers;
using sft.Areas.Admin.Models;
using sft.Helpers;
using sft.Models;

namespace sft.Areas.Admin.Controllers
{
    [Authorize]
    public class AdminRentalsController : Controller
    {
        private ApplicationDbContext _db = ApplicationDbContext.Create();
        //
        // GET: /Admin/AdminRentals/
        public async Task<ActionResult> Index()
        {
            if (!User.IsAuthorized("rentals"))
            {
                return RedirectToAction("Index", "AdminHome");
            }
            return View(await _db.Rentals.ToListAsync());
        }

        public async Task<ActionResult> ChooseHiglighted()
        {
            if (!User.IsAuthorized("rentals"))
            {
                return RedirectToAction("Index", "AdminHome");
            }
            var vm = new HIglightRentalViewModel
            {
                Rentals = await _db.Rentals.ToListAsync()
            };
            vm.Higlighted = vm.Rentals.Where(r => r.IsHighLighted).ToList();
            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> ChooseHiglighted(HIglightRentalViewModel model)
        {
            if (!User.IsAuthorized("rentals"))
            {
                return RedirectToAction("Index", "AdminHome");
            }
            var first = await _db.Rentals.FirstOrDefaultAsync(r => r.Id == model.First);
            var second = await _db.Rentals.FirstOrDefaultAsync(r => r.Id == model.Second);
            var thirt = await _db.Rentals.FirstOrDefaultAsync(r => r.Id == model.Thirt);
            var fourth = await _db.Rentals.FirstOrDefaultAsync(r => r.Id == model.Fourth);

            await _db.Database.ExecuteSqlCommandAsync("UPDATE Rentals SET [IsHighLighted] = 0");
            first.IsHighLighted = true;
            second.IsHighLighted = true;
            thirt.IsHighLighted = true;
            fourth.IsHighLighted = true;

            _db.Entry(first).State = EntityState.Modified;
            _db.Entry(second).State = EntityState.Modified;
            _db.Entry(thirt).State = EntityState.Modified;
            _db.Entry(fourth).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return RedirectToAction("ChooseHiglighted");
        }

        public async Task<ActionResult> Create()
        {
            if (!User.IsAuthorized("rentals"))
            {
                return RedirectToAction("Index", "AdminHome");
            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Rental model)
        {
            if (!User.IsAuthorized("rentals"))
            {
                return RedirectToAction("Index", "AdminHome");
            }
            if (ModelState.IsValid)
            {
                model.Id = Guid.NewGuid();
                model.Created = DateTime.Now;
                model.Modified = DateTime.Now;
                model.UrlName = model.Name.ConvertToUrlSafeString();
                if (await _db.Rentals.FirstOrDefaultAsync(r => r.UrlName == model.UrlName) != null)
                {
                    int i = 2;
                    model.UrlName = (model.Name + " " + i).ConvertToUrlSafeString();
                    while (await _db.Rentals.FirstOrDefaultAsync(r => r.UrlName == model.UrlName) != null)
                    {
                        i++;
                        model.UrlName = (model.Name + " " + i).ConvertToUrlSafeString();
                    }
                }
                _db.Rentals.Add(model);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            if (!User.IsAuthorized("rentals"))
            {
                return RedirectToAction("Index", "AdminHome");
            }
            return View(await _db.Rentals.FindAsync(id));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Rental model)
        {
            if (!User.IsAuthorized("rentals"))
            {
                return RedirectToAction("Index", "AdminHome");
            }
            if (ModelState.IsValid)
            {
                model.Modified = DateTime.Now;
                _db.Entry(model).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            if (!User.IsAuthorized("rentals"))
            {
                return RedirectToAction("Index", "AdminHome");
            }
            _db.Rentals.Remove(await _db.Rentals.FindAsync(id));
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}