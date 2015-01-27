using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace sft.Models
{
  public class Comment
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public bool IsApproved { get; set; }
    public String Text { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public String User { get; set; }
    public Guid UserId { get; set; }
    public bool IsSubComment { get; set; }
    public Guid? SubCommentFrom { get; set; }
    public virtual ICollection<Comment> SubComments { get; set; }

    public Comment()
    {
      SubComments = new List<Comment>();
    }
  }
}