using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NazariTest.Domain.Common;
using Pluralize.NET;

namespace NazariTest.Persistence.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static Action<ModelBuilder> ModelBuilderConfigureAction { get; set; }

        public static void ExecuteModelBuilderConfigureAction(this ModelBuilder modelBuilder)
        {
            if (ModelBuilderConfigureAction != null)
                ModelBuilderConfigureAction.Invoke(modelBuilder);
        }

        public static void AddPluralizingTableNameConvention(
            this ModelBuilder modelBuilder,
            bool setSingularizing = false
        )
        {
            var pluralizer = new Pluralizer();
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (setSingularizing)
                    entityType.SetTableName(pluralizer.Singularize(tableName));
                else
                    entityType.SetTableName(pluralizer.Pluralize(tableName));
            }
        }

        public static void AddRestrictDeleteBehaviorConvention(this ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;
        }

        public static void RegisterIEntityTypeConfiguration(
            this ModelBuilder modelBuilder,
            Assembly assembly
        )
        {
            var applyGenericMethod = typeof(ModelBuilder)
                .GetMethods()
                .First(x => x.Name == nameof(ModelBuilder.ApplyConfiguration));
            var types = assembly
                .GetExportedTypes()
                .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic)
                .ToList();
            foreach (var type in types)
            {
                var iface = type.GetInterfaces()
                    .FirstOrDefault(
                        x =>
                            x.IsConstructedGenericType
                            && x.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)
                    );
                if (iface != null)
                {
                    var applyConcreteMethod = applyGenericMethod.MakeGenericMethod(
                        iface.GenericTypeArguments[0]
                    );
                    applyConcreteMethod.Invoke(
                        obj: modelBuilder,
                        parameters: new object[] { Activator.CreateInstance(type) }
                    );
                }
            }
        }

        public static void RegisterAllEntities(this ModelBuilder modelBuilder, Assembly assembly)
        {
            var types = assembly
                .GetExportedTypes()
                .Where(
                    c =>
                    c.IsClass
                    && !c.IsAbstract
                        && c.IsPublic
                        && typeof(BaseEntity).IsAssignableFrom(c)
                )
                .ToList();
            foreach (var type in types)
            {
                modelBuilder.Entity(type);
            }
        }
    }
}
