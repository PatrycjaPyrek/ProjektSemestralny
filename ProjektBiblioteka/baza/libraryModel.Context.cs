﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjektBiblioteka.baza
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class libraryEntitiesDataSet : DbContext
    {
        public libraryEntitiesDataSet()
            : base("name=libraryEntitiesDataSet")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cennik> Cennik { get; set; }
        public virtual DbSet<Doplaty> Doplaty { get; set; }
        public virtual DbSet<Egzemplarze> Egzemplarze { get; set; }
        public virtual DbSet<gatunki> gatunki { get; set; }
        public virtual DbSet<Klienci> Klienci { get; set; }
        public virtual DbSet<Ksiazki> Ksiazki { get; set; }
        public virtual DbSet<Tworcy> Tworcy { get; set; }
        public virtual DbSet<Wypozyczenia> Wypozyczenia { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
