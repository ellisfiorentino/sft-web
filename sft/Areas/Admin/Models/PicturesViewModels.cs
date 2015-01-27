using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using sft.Models;

namespace sft.Areas.Admin.Models
{
  public class PicturesIndexViewModel
  {
    public List<Picture> Pictures { get; set; }
    public int Page { get; set; }
    public int Total { get; set; }
  }
}