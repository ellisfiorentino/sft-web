using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;
using MvcSiteMapProvider.Linq;

namespace sft.Models
{
    public class Rental
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid SimId { get; set; }
        public virtual Sim Sim { get; set; }
        public String Name { get; set; }
        [Index("UrLName", IsUnique = true), MaxLength(255)]
        public String UrlName { get; set; }
        [UIHint("Html"), AllowHtml]
        public String Description { get; set; }
        [Display(Name = "Amount of prim allowed")]
        public int AllowedPrim { get; set; }
        [Display(Name = "Price per week")]
        public int WeekPrice { get; set; }
        [Display(Name = "X coordinate")]
        public int Xcord { get; set; }
        [Display(Name = "Y coordinate")]
        public int Ycord { get; set; }
        [Display(Name = "Z coordinate")]
        public int Zcord { get; set; }
        [Display(Name = "Available for rent")]
        public bool Available { get; set; }
        [Display(Name = "Street name")]
        public String StreetName { get; set; }
        [Display(Name = "Number")]
        public String HouseNumber { get; set; }
        public String MainPictureLocation { get; set; }
        public bool IsHighLighted { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public Rental()
        {
        }

        public static List<SelectListItem> GetSims(Guid id = new Guid())
        {
            var list = new List<SelectListItem>();
            using (var db = ApplicationDbContext.Create())
            {
                list = db.Sims.Select(sim => new SelectListItem
                {
                    Text = sim.SimName,
                    Value = sim.Id.ToString(),
                    Selected = sim.Id == id
                }).ToList();
            }
            return list;
        } 

    }
}