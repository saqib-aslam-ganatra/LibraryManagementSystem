using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Data.Configurations
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.Property(m => m.FullName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(m => m.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(m => m.PhoneNumber)
                .HasMaxLength(50);
        }
    }
}
