using CMS_Entity.Entity;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;

namespace CMS_Entity
{
    public class CMS_Context : DbContext
    {
        public CMS_Context() : base("name=CMS_Context")
        {
            Database.SetInitializer<CMS_Context>(new ContextHandler());
            ((IObjectContextAdapter)this).ObjectContext.ContextOptions.LazyLoadingEnabled = true;
        }
        public virtual DbSet<CMS_Sims> CMS_Sims { get; set; }
        public virtual DbSet<CMS_Marketing> CMS_Marketing { get; set; }
        public virtual DbSet<CMS_API> CMS_API { get; set; }
        public virtual DbSet<CMS_PaymentMethod> CMS_PaymentMethod { get; set; }
        public virtual DbSet<CMS_DepositPackage> CMS_DepositPackage { get; set; }
        public virtual DbSet<CMS_SysConfigs> CMS_SysConfigs { get; set; }
        public virtual DbSet<CMS_Employee> CMS_Employees { get; set; }
        public virtual DbSet<CMS_Categories> CMS_Categories { get; set; }
        public virtual DbSet<CMS_Products> CMS_Products { get; set; }
        public virtual DbSet<CMS_Policy> CMS_Policys { get; set; }
        public virtual DbSet<CMS_News> CMS_News { get; set; }
        public virtual DbSet<CMS_Images> CMS_Images { get; set; }
        public virtual DbSet<CMS_Customers> CMS_Customers { get; set; }
        public virtual DbSet<CMS_Companies> CMS_Companies { get; set; }
        public virtual DbSet<CMS_DepositTransactions> CMS_DepositTransactions { get; set; }
        public virtual DbSet<CMS_CustomerActiveCode> CMS_CustomerActiveCode { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                                            .Where(type => !String.IsNullOrEmpty(type.Namespace))
                                            .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
                                            type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
