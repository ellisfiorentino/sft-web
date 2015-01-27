using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace sft.Models
{
  // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
  public class ApplicationUser : IdentityUser
  {
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string AccountName { get; set; }

    public string GetDisplayName()
    {
      string displayName = FirstName;
      if (!string.IsNullOrEmpty(MiddleName))
      {
        displayName += " " + MiddleName;
      }
      displayName += string.Format(" {0} ({1})", LastName, AccountName);
      return displayName;
    }


    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
    {
      // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
      var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
      // Add custom user claims here
      return userIdentity;
    }
  }

  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public ApplicationDbContext()
      : base("DefaultConnection", throwIfV1Schema: false)
    {
    }

    public DbSet<Sim> Sims { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    public DbSet<NewsArticle> NewsArticles { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Picture> Pictures { get; set; }


    public static ApplicationDbContext Create()
    {
      return new ApplicationDbContext();
    }
  }
}