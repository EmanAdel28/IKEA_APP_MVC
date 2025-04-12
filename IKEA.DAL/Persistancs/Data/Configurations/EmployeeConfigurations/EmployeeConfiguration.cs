using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Common;
using IKEA.DAL.Models.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKEA.DAL.Persistancs.Data.Configurations.EmployeeConfigurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Id).UseIdentityColumn(1, 1);
            builder.Property(E => E.Name).IsRequired().HasColumnType("varchar(50)");
            builder.Property(E => E.Address).IsRequired().HasColumnType("varchar(50)");
            builder.Property(E => E.Salary).IsRequired().HasColumnType("decimal(8,2)");
            builder.Property(E => E.Gender).HasConversion(
                (gender) => gender.ToString(),
                (gender) => (Gender)Enum.Parse(typeof(Gender), gender)
                );
            builder.Property(E => E.EmployeeType).HasConversion(
                 (type) => (type).ToString(),
                 (type) => (EmployeeType)Enum.Parse(typeof(EmployeeType), type)
                 );

            builder.HasOne(D=>D.Department)
                   .WithMany(D => D.Employees)
                   .HasForeignKey(E=>E.DepartmentId)
                   .OnDelete(DeleteBehavior.SetNull);
                   


            //Development Usage
            builder.Property(D => D.CreatedOn).HasDefaultValueSql("GetDate()");
            builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("GetDate()");
        }
    }
    }

