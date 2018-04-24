using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WeddingSite.Models.Mapping
{
    public class PendingQuestionMap : EntityTypeConfiguration<PendingQuestion>
    {
        public PendingQuestionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ReplyEmail)
                .HasMaxLength(50);

            this.Property(t => t.Question)
                .IsRequired()
                .HasMaxLength(2000);

            // Table & Column Mappings
            this.ToTable("PendingQuestion");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ReplyEmail).HasColumnName("ReplyEmail");
            this.Property(t => t.Question).HasColumnName("Question");
            this.Property(t => t.WillAnswer).HasColumnName("WillAnswer");
        }
    }
}
