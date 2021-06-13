using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TableDefinitionBrowser.Models;

namespace TableDefinitionBrowser.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

		protected override void OnModelCreating(ModelBuilder builder)
		{
            base.OnModelCreating(builder);
            builder.Entity<ColumnDefinition>().HasKey(cd => new { cd.TableName, cd.PhysicalColumnName });
        }

        public DbSet<TableDefinition> TableDefinition { get; set; }
        public DbSet<ColumnDefinition> ColumnDefinition { get; set; }
    }
}