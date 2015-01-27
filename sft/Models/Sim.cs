using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sft.Models
{
  public class Sim
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [Index("SimName", IsUnique = true), MaxLength(255)]
    public String SimName { get; set; }
    public virtual ICollection<Rental> Rentals { get; set; }

    public Sim()
    {
      Rentals = new List<Rental>();
    }
  }
}