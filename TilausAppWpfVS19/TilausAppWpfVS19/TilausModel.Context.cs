﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TilausAppWpfVS19
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TilausDBEntities : DbContext
    {
        public TilausDBEntities()
            : base("name=TilausDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Asiakkaat> Asiakkaat { get; set; }
        public virtual DbSet<Henkilot> Henkilot { get; set; }
        public virtual DbSet<Postitoimipaikat> Postitoimipaikat { get; set; }
        public virtual DbSet<Tilaukset> Tilaukset { get; set; }
        public virtual DbSet<Tilausrivit> Tilausrivit { get; set; }
        public virtual DbSet<Tuotteet> Tuotteet { get; set; }
    }
}
