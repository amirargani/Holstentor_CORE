using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Holstentor.Models;
using Holstentor.Data.Class_DbContext;

namespace Holstentor.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private static bool _Created = false;
        // ** Code ctor **
        public ApplicationDbContext()
        {
            if (!_Created)
            {
                _Created = true;
                Database.EnsureCreated();
            }
        }
        // ** https://stackoverflow.com/questions/38982387/entity-framework-core-1-0-connection-strings **
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //    optionsBuilder.UseSqlServer("Data Source=somedatabase.database.windows.net;Initial Catalog=database;Integrated Security=False;User ID=username;Password=password;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=true");

        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server = .; initial catalog = DbHolstentor; Integrated Security = True; MultipleActiveResultSets = True; App = EntityFramework & quot;");
        }

        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        //    : base(options)
        //{
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<Index_Db>().HasIndex(I => new { I.IDIndex }).HasName("IX_Index-Db");
        }

        public DbSet<Index_Db> Tbl_Index { get; set; }
        public DbSet<IndexET_Db> Tbl_IndexET { get; set; }
        public DbSet<Album_Db> Tbl_Album { get; set; }
        public DbSet<Kategorie_Db> Tbl_Kategorie { get; set; }
        public DbSet<Galerie_Db> Tbl_Galerie { get; set; }
        public DbSet<Produkte_Db> Tbl_Produkte { get; set; }
        public DbSet<Unterkategorie_Db> Tbl_Unterkategorie { get; set; }
        public DbSet<Nachricht_Db> Tbl_Nachricht { get; set; }
        public DbSet<Hochladen_Db> Tbl_Hochladen { get; set; }
        //public DbSet<Einstellungen_Db> Tbl_Einstellungen { get; set; }
        public DbSet<Holstentor.Models.ApplicationUser> ApplicationUser { get; set; }
    }
}