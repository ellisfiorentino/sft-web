using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using sft.Areas.Admin.Helpers;
using sft.Areas.Admin.Models;
using sft.Models;

namespace sft.Areas.Admin.Controllers
{
  [Authorize]
  public class AdminPicturesController : Controller
  {
    private ApplicationDbContext _db = ApplicationDbContext.Create();
    private readonly String FilePath = HostingEnvironment.MapPath("~/Content/uploads");

    //
    // GET: /Admin/AdminPictures/
    public async Task<ActionResult> Index(int page = 1)
    {
      //            return View(await _db.Pictures.ToListAsync());
      if (!User.IsAuthorized("news", "rentals", "events"))
      {
        return RedirectToAction("Index", "AdminHome");
      }
      var pictures = await _db.Pictures.OrderBy(picture => picture.Id).ToListAsync();
      return View(new PicturesIndexViewModel
      {
        Page = page,
        Total = pictures.Count,
        Pictures = pictures.Skip((page - 1) * 9).Take(9).ToList()
      });
    }

    public async Task<ActionResult> Edit(Guid id)
    {
      if (!User.IsAuthorized("news", "rentals", "events"))
      {
        return RedirectToAction("Index", "AdminHome");
      }
      return View(await _db.Pictures.FindAsync(id));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(Picture picture)
    {
      if (!User.IsAuthorized("news", "rentals", "events"))
      {
        return RedirectToAction("Index", "AdminHome");
      }
      if (ModelState.IsValid)
      {
        _db.Entry(picture).State = EntityState.Modified;
        await _db.SaveChangesAsync();
      }
      return View(picture);
    }

    public ActionResult Create()
    {
      if (!User.IsAuthorized("news", "rentals", "events"))
      {
        return RedirectToAction("Index", "AdminHome");
      }
      return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(Picture picture)
    {
      if (!User.IsAuthorized("news", "rentals", "events"))
      {
        return RedirectToAction("Index", "AdminHome");
      }
      picture.Id = Guid.NewGuid();
      var file = Request.Files[0];
      if (file != null && file.ContentLength > 0)
      {
        var fileName = Path.GetFileName(file.FileName);
        var savePath = Path.Combine(FilePath, fileName);
        if (System.IO.File.Exists(savePath))
        {
          var extLess = Path.GetFileNameWithoutExtension(file.FileName);
          var ext = Path.GetExtension(file.FileName);
          var i = 0;
          var tmpName = extLess + "_" + i + ext;
          savePath = Path.Combine(FilePath, tmpName);
          while (System.IO.File.Exists(savePath))
          {
            i++;
            tmpName = extLess + "_" + i + ext;
            savePath = Path.Combine(FilePath, tmpName);
          }
        }
        file.SaveAs(savePath);
        picture.FileName = "http://www.serenityfallsest.com/Content/uploads/" + fileName;
        _db.Pictures.Add(picture);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
      }
      return View();
    }

    public ActionResult CreatePartial()
    {
      if (!User.IsAuthorized("news", "rentals", "events"))
      {
        return RedirectToAction("Index", "AdminHome");
      }
      return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<ActionResult> CreatePartial(Picture picture)
    {
      if (!User.IsAuthorized("news", "rentals", "events"))
      {
        return RedirectToAction("Index", "AdminHome");
      }
      picture.Id = Guid.NewGuid();
      var file = Request.Files[0];
      if (file != null && file.ContentLength > 0)
      {
        var fileName = Path.GetFileName(file.FileName);
        var savePath = Path.Combine(FilePath, fileName);
        if (System.IO.File.Exists(savePath))
        {
          var extLess = Path.GetFileNameWithoutExtension(file.FileName);
          var ext = Path.GetExtension(file.FileName);
          var i = 0;
          var tmpName = extLess + "_" + i + ext;
          savePath = Path.Combine(FilePath, tmpName);
          while (System.IO.File.Exists(savePath))
          {
            i++;
            tmpName = extLess + "_" + i + ext;
            savePath = Path.Combine(FilePath, tmpName);
          }
        }
        file.SaveAs(savePath);
        picture.FileName = "http://www.serenityfallsest.com/Content/uploads/" + fileName;
        _db.Pictures.Add(picture);
        await _db.SaveChangesAsync();
        return View("PartialResult", picture);
      }
      return View();
    }

    public async Task<ActionResult> SelectPartial()
    {
      if (!User.IsAuthorized("news", "rentals", "events"))
      {
        return RedirectToAction("Index", "AdminHome");
      }
      return View(await _db.Pictures.ToListAsync());
    }

    public async Task<ActionResult> Delete(Guid id)
    {
      if (!User.IsAuthorized("ADMIN"))
      {
        return RedirectToAction("Index", "AdminHome");
      }
      _db.Pictures.Remove(await _db.Pictures.FindAsync(id));
      await _db.SaveChangesAsync();
      return RedirectToAction("Index");
    }
  }
}