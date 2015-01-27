using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace sft.Models
{
  public class NewsArticle
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [Index("UrlName", IsUnique = true), MaxLength(255)]
    public String UrlName { get; set; }
    public String Name { get; set; }
    [UIHint("Html"), AllowHtml]
    public String Text { get; set; }
    public bool HasPicture { get; set; }
    public String MainPictureLocation { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public String Author { get; set; }
    public Guid AuthorId { get; set; }
    public String Editor { get; set; }
    public Guid? EditorId { get; set; } 
    public bool IsCommentingEnabled { get; set; }
    public bool IsHighLighted { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }

    public NewsArticle()
    {
      Comments = new List<Comment>();
    }
  }
}