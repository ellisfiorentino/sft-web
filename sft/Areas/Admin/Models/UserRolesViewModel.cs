using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using sft.Models;

namespace sft.Areas.Admin.Models
{
  public class UserRolesViewModel
  {

    public string[] Roles { get; set; }

    public UserRolesViewModel()
    {
      Roles = new string[0];
    }


    public List<IdentityRole> GetRoles()
    {
      var list = new List<IdentityRole>();

      using (var db = ApplicationDbContext.Create())
      {
        list = db.Roles.ToList();
      }

      return list;
    }

    public List<IdentityRole> SelectedRoles(IEnumerable<string> ids)
    {
      var list = new List<IdentityRole>();

      using (var db = ApplicationDbContext.Create())
      {
        list = db.Roles.Where(role => ids.Any(id => id == role.Id)).ToList();
      }

      return list;
    } 

  }
}