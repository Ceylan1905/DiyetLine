﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DiyetLine.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DiyetlineEntities : DbContext
    {
        public DiyetlineEntities()
            : base("name=DiyetlineEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<il> il { get; set; }
        public virtual DbSet<ilce> ilce { get; set; }
        public virtual DbSet<Table_IsletmeSahibi> Table_IsletmeSahibi { get; set; }
        public virtual DbSet<Table_Kullanicilar> Table_Kullanicilar { get; set; }
        public virtual DbSet<Table_Roller> Table_Roller { get; set; }
    }
}