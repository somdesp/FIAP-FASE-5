using FIAP.TECH.CORE.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.TECH.INFRASTRUCTURE.Configurations;

public class DoctorMap : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseIdentityColumn().IsRequired();
        builder.Property(x => x.CreatedDate).HasColumnType("datetime").IsRequired();
        builder.Property(x => x.Name).HasColumnType("varchar(50)").IsRequired();
        builder.Property(x => x.CRM).HasColumnType("varchar(10)").IsRequired();
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.Password).HasColumnType("varchar(50)").IsRequired();
    }
}
