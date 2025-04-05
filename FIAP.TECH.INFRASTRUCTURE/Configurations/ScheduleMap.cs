using FIAP.TECH.CORE.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.TECH.INFRASTRUCTURE.Configurations;

public class ScheduleMap : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseIdentityColumn().IsRequired();
        builder.Property(x => x.CreatedDate).HasColumnType("datetime").IsRequired();
        builder.Property(x => x.Date).HasColumnType("datetime").IsRequired();
        builder.Property(x => x.IdDoctor).IsRequired();
        builder.Property(x => x.IdPatient).IsRequired();
        builder.Property(x => x.HaveSchedule).HasColumnType("bit").IsRequired();


        builder.HasOne(c => c.Doctor)
            .WithMany(r => r.Schedule)
            .HasForeignKey(c => c.IdDoctor);


        builder.HasOne(c => c.Patient)
            .WithMany(r => r.Schedule)
            .HasForeignKey(c => c.IdPatient);


    }
}
