using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WeddingSite.Models.Mapping;

namespace WeddingSite.Models
{
    public partial class WeddingManagementContext : IdentityDbContext<
    User, UserRole, int, UserLogin, UserUserRole, UserClaim>

    {
        static WeddingManagementContext()
        {
            Database.SetInitializer<WeddingManagementContext>(null);
        }

        public WeddingManagementContext()
            : base("Name=WeddingManagementContext")
        {
            //base.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Banner> Banners { get; set; }
        public DbSet<BannerBlob> BannerBlobs { get; set; }
        public DbSet<Blob> Blobs { get; set; }
        public DbSet<BlobContainer> BlobContainers { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogPostBody> BlogPostBodies { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<GuestCode> GuestCodes { get; set; }
        public DbSet<PendingQuestion> PendingQuestions { get; set; }
        public DbSet<database_firewall_rules> database_firewall_rules { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BannerMap());
            modelBuilder.Configurations.Add(new BannerBlobMap());
            modelBuilder.Configurations.Add(new BlobMap());
            modelBuilder.Configurations.Add(new BlobContainerMap());
            modelBuilder.Configurations.Add(new BlogPostMap());
            modelBuilder.Configurations.Add(new BlogPostBodyMap());
            modelBuilder.Configurations.Add(new FAQMap());
            modelBuilder.Configurations.Add(new GuestMap());
            modelBuilder.Configurations.Add(new PendingQuestionMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserClaimMap());
            modelBuilder.Configurations.Add(new UserLoginMap());
            modelBuilder.Configurations.Add(new UserRoleMap());
            modelBuilder.Configurations.Add(new UserUserRoleMap());
            modelBuilder.Configurations.Add(new database_firewall_rulesMap());
        }
    }
}
