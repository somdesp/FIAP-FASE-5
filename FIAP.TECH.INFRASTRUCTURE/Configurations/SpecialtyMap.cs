using FIAP.TECH.CORE.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.TECH.INFRASTRUCTURE.Configurations
{
    public class SpecialtyMap : IEntityTypeConfiguration<Specialty>
    {
        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn().IsRequired();
            builder.Property(x => x.CreatedDate).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.Name).HasColumnType("varchar(50)").IsRequired();

            builder.HasMany(c => c.Doctor)
                .WithOne(r => r.Specialty)
                .HasForeignKey(c => c.IdSpecialty);

        }
    }
}
