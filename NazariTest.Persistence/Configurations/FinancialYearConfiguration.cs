using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NazariTest.Domain.Entities;

namespace NazariTest.Persistence.Configurations
{
    public class FinancialYearConfiguration : IEntityTypeConfiguration<FinancialYear>
    {
        public void Configure(EntityTypeBuilder<FinancialYear> builder)
        {

            builder.Property(x => x.Title).HasMaxLength(100).IsRequired();    
            builder.Property(x=>x.StartDate).IsRequired();
            builder.Property(x=>x.EndDate).IsRequired();
            builder.Property(x=>x.IsDeleted).HasDefaultValue(false);
            builder.HasIndex(x=>x.Title).IsUnique();

        }
    }
}
