using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace sft.Areas.Admin.Helpers
{
  public static class AuthHelper
  {
    public static bool IsAuthorized(this IPrincipal user, params string[] allowedRoles)
    {
      return user.IsInRole("ADMIN") || allowedRoles.Any(user.IsInRole);
    }
  }
}