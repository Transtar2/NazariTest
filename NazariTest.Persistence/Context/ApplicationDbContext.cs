using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NazariTest.Domain.Common;
using NazariTest.Persistence.Extensions;

namespace NazariTest.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        private readonly Assembly assembly;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {
            //Debugger.Launch();
            this.assembly = Assembly.GetAssembly(typeof(BaseEntity));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ExecuteModelBuilderConfigureAction();
            modelBuilder.RegisterAllEntities(assembly: assembly);
            modelBuilder.RegisterIEntityTypeConfiguration(assembly: Assembly.GetExecutingAssembly());
            modelBuilder.AddRestrictDeleteBehaviorConvention();
            modelBuilder.AddPluralizingTableNameConvention();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
