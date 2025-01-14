using mamba.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace mamba.Configurations
{
    public class EmployeeConfiguration:IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee>builder)
        {
            builder.Property(a => a.FullName)
                .IsRequired()
                .HasColumnType("varchar(150)");
         

        }
    }
}
