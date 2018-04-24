using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WeddingSite.Models.Mapping
{
    public class FAQMap : EntityTypeConfiguration<FAQ>
    {
        public FAQMap()
        {
            // Primary Key
            this.HasKey(t => t.FAQId);

            // Properties
            this.Property(t => t.Question)
                .IsRequired()
                .HasMaxLength(4000);

            this.Property(t => t.Answer)
                .IsRequired()
                .HasMaxLength(4000);

            // Table & Column Mappings
            this.ToTable("FAQ");
            this.Property(t => t.FAQId).HasColumnName("FAQId");
            this.Property(t => t.Question).HasColumnName("Question");
            this.Property(t => t.Answer).HasColumnName("Answer");
        }
    }
}
