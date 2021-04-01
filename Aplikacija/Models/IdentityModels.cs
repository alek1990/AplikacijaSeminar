using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Aplikacija.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;

        }

        //Dodavanje novih svojstava u tablicu dbo.AspNetUsers
        public string Ime { get; set; }
        public string Prezime { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //Mjenjanje naziva tablice dbo.AspNetUsers i njezinih svojstava
        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>().ToTable("Zaposlenici").Property(p => p.Id).HasColumnName("IdZaposlenik");
            modelBuilder.Entity<IdentityUser>().Property(p => p.UserName).HasColumnName("KorisnickoIme");
            modelBuilder.Entity<ApplicationUser>().ToTable("Zaposlenici").Property(p => p.Id).HasColumnName("IdZaposlenik");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.UserName).HasColumnName("KorisnickoIme");
        }

        //Nove tablice
        public DbSet<Seminar> Seminari { get; set; }
        public DbSet<Predbiljezba> Predbiljezbe { get; set; }
  

    }
}