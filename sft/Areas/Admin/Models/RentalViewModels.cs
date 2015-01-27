using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sft.Models;

namespace sft.Areas.Admin.Models
{
    public class HIglightRentalViewModel
    {
        public List<Rental> Rentals { get; set; }
        public List<Rental> Higlighted { get; set; }

        public Guid First { get; set; }
        public Guid Second { get; set; }
        public Guid Thirt { get; set; }
        public Guid Fourth { get; set; }


        public List<SelectListItem> GetRentalSelecter()
        {
            var list = new List<SelectListItem>();
            bool oneSelected = false;
            foreach (var rental in Rentals)
            {
                var tempSelected = Higlighted.Any(r => r.Id == rental.Id) && !oneSelected;
                list.Add(new SelectListItem
                {
                    Text = rental.Name,
                    Value = rental.Id.ToString(),
                    Selected = tempSelected
                });
                if (tempSelected)
                {
                    if (First == Guid.Empty)
                    {
                        First = rental.Id;
                    }
                    else if (Second == Guid.Empty)
                    {
                        Second = rental.Id;
                    }
                    else if (Thirt == Guid.Empty)
                    {
                        Thirt = rental.Id;
                    }
                    else if (Fourth == Guid.Empty)
                    {
                        Fourth = rental.Id;
                    }
                    oneSelected = true;
                    Higlighted.Remove(Higlighted.FirstOrDefault(r => r.Id == rental.Id));
                }
            }
            return list;
        }
    }
}