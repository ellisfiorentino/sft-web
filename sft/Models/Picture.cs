using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace sft.Models
{
  public class Picture
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public String Name { get; set; }
    public String FileName { get; set; }
  }
}