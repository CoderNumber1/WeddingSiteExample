using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WeddingSite.Models.Mapping
{
    public class BlogPostMap : EntityTypeConfiguration<BlogPost>
    {
        public BlogPostMap()
        {
            // Primary Key
            this.HasKey(t => t.BlogPostId);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("BlogPost");
            this.Property(t => t.BlogPostId).HasColumnName("BlogPostId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.Updated).HasColumnName("Updated");
        }
    }
}
