﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ahp.Core.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class AhpEntities : DbContext
    {
        public AhpEntities()
            : base("name=AhpEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdministrasiMenu> AdministrasiMenus { get; set; }
        public virtual DbSet<AdministrasiRole> AdministrasiRoles { get; set; }
        public virtual DbSet<AdministrasiRoleDtl> AdministrasiRoleDtls { get; set; }
        public virtual DbSet<AdministrasiRoleUser> AdministrasiRoleUsers { get; set; }
        public virtual DbSet<AdministrasiTombol> AdministrasiTombols { get; set; }
        public virtual DbSet<AdministrasiUser> AdministrasiUsers { get; set; }
        public virtual DbSet<AdministrasiHakAks> AdministrasiHakAkses { get; set; }
        public virtual DbSet<AdministrasiHakAksesMenu> AdministrasiHakAksesMenus { get; set; }
        public virtual DbSet<AdministrasiHakAksesRole> AdministrasiHakAksesRoles { get; set; }
        public virtual DbSet<AdministrasiHakAksesTombol> AdministrasiHakAksesTombols { get; set; }
        public virtual DbSet<CalonKaryawan> CalonKaryawans { get; set; }
        public virtual DbSet<Lowongan> Lowongans { get; set; }
        public virtual DbSet<Kriteria> Kriterias { get; set; }
        public virtual DbSet<PerbandinganKriteria> PerbandinganKriterias { get; set; }
    
        public virtual ObjectResult<SpPerbandinganKriteria_Result> SpPerbandinganKriteria()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SpPerbandinganKriteria_Result>("SpPerbandinganKriteria");
        }
    }
}
