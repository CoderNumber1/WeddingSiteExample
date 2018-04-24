using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WeddingSite.Models.Mapping
{
    public class BlogPostBodyMap : EntityTypeConfiguration<BlogPostBody>
    {
        public BlogPostBodyMap()
        {
            // Primary Key
            this.HasKey(t => t.BlogPostBodyId);

            // Properties
            this.Property(t => t.Content)
                .IsRequired()
                .HasMaxLength(2000);

            // Table & Column Mappings
            this.ToTable("BlogPostBody");
            this.Property(t => t.BlogPostBodyId).HasColumnName("BlogPostBodyId");
            this.Property(t => t.BlogPostId).HasColumnName("BlogPostId");
            this.Property(t => t.OrderBy).HasColumnName("OrderBy");
            this.Property(t => t.Content).HasColumnName("Content");

            // Relationships
            this.HasRequired(t => t.BlogPost)
                .WithMany(t => t.BlogPostBodies)
                .HasForeignKey(d => d.BlogPostId);

        }
    }
}
