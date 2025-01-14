using mamba.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace mamba.Configurations
{
    public class DepartmentsConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(a => a.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder
                .HasIndex(p => p.Name)
                .IsUnique();
        }
    }
}
