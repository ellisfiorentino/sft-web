using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using sft.Areas.Admin.Helpers;
using sft.Areas.Admin.Models;
using sft.Models;

namespace sft.Areas.Admin.Controllers
{
    [Authorize]
    public class AdminUsersController : Controller
    {
        private ApplicationDbContext _db = ApplicationDbContext.Create();
        //
        // GET: /Admin/AdminUsers/
        public async Task<ActionResult> Index()
        {
            if (!User.IsAuthorized("ADMIN"))
            {
                return RedirectToAction("Index", "AdminHome");
            }
            return View(await _db.Users.ToListAsync());
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (!User.IsAuthorized("ADMIN"))
            {
                return RedirectToAction("Index", "AdminHome");
            }
            return View(await _db.Users.FirstOrDefaultAsync(user => user.Id == id));
        }

        public async Task<ActionResult> RemoveCommentsFromUser(string id)
        {
            if (!User.IsAuthorized("ADMIN"))
            {
                return RedirectToAction("Index", "AdminHome");
            }
            Guid uId = Guid.Empty;
            Guid.TryParse(id, out uId);
            if (uId == Guid.Empty)
            {
                return new HttpNotFoundResult();
            }
            _db.Comments.RemoveRange(await _db.Comments.Where(c => c.UserId == uId).ToListAsync());
            await _db.SaveChangesAsync();
            return RedirectToAction("Edit", new { id });
        }

        public async Task<RedirectToRouteResult> LockOut(string id)
        {
            if (!User.IsAuthorized("ADMIN"))
            {
                return RedirectToAction("Index", "AdminHome");
            }
            var user = await _db.Users.FirstOrDefaultAsync(applicationUser => applicationUser.Id == id);
            user.LockoutEndDateUtc = DateTime.MaxValue;
            _db.Entry(user).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return RedirectToAction("Edit", new { id });
        }

        public async Task<RedirectToRouteResult> DisableLockOut(string id)
        {
            if (!User.IsAuthorized("ADMIN"))
            {
                return RedirectToAction("Index", "AdminHome");
            }
            var user = await _db.Users.FirstOrDefaultAsync(applicationUser => applicationUser.Id == id);
            user.LockoutEndDateUtc = null;
            _db.Entry(user).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return RedirectToAction("Edit", new { id });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<RedirectToRouteResult> SetRoles(UserRolesViewModel postBackModel, string id)
        {
            if (!User.IsAuthorized("ADMIN"))
            {
                return RedirectToAction("Index", "AdminHome");
            }
            var user = await _db.Users.FirstOrDefaultAsync(applicationUser => applicationUser.Id == id);
            user.Roles.Clear();
            var selectedRoles = await _db.Roles.Where(role => postBackModel.Roles.Any(rid => role.Id == rid)).ToListAsync();
            foreach (var userRole in selectedRoles)
            {
                var insertRole = new IdentityUserRole
                {
                    UserId = id,
                    RoleId = userRole.Id
                };
                user.Roles.Add(insertRole);
            }
            _db.Entry(user).State = EntityState.Modified; ;
            await _db.SaveChangesAsync();
            return RedirectToAction("Edit", new { id });
        }


    }
}