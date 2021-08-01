using ExpenseManager.Core;
using Lookif.Layers.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace ExpenseManager.Data.Context
{
    public class ExpenseManagerDbContext : ApplicationDbContext
    {
        public ExpenseManagerDbContext(DbContextOptions options)
            : base(options)
        {
        }
        Assembly GetAssemblyByName(string name)
        {
            return AppDomain.CurrentDomain.GetAssemblies().
                   SingleOrDefault(assembly => assembly.GetName().Name == name);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            CoreLayerAssembly = typeof(MyTest).Assembly;
            Debug.WriteLine(CoreLayerAssembly.FullName);
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ExpenseManager;Integrated Security=true");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
