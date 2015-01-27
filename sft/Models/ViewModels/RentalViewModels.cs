using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sft.Models.ViewModels
{
  public class RentalIndexViewModel
  {
    public List<Rental> Rentals { get; set; } 
    public int Page { get; set; }
    public int Total { get; set; }
  }
}