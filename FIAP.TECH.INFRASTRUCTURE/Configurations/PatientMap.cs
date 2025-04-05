using FIAP.TECH.CORE.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.TECH.INFRASTRUCTURE.Configurations;

public class PatientMap : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseIdentityColumn().IsRequired();
        builder.Property(x => x.CreatedDate).HasColumnType("datetime").IsRequired();
        builder.Property(x => x.Name).HasColumnType("varchar(50)").IsRequired();
        builder.Property(x => x.CPF).HasColumnType("varchar(11)").IsRequired();
        builder.Property(x => x.Password).HasColumnType("varchar(50)").IsRequired();
        builder.Property(x => x.IsActive).IsRequired();

    }
}
