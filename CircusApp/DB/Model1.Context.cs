﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CircusApp.DB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class circus_dbEntities : DbContext
    {
        public circus_dbEntities()
            : base("name=circus_dbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<EquipmentPerformance> EquipmentPerformance { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Hall> Hall { get; set; }
        public virtual DbSet<Performance> Performance { get; set; }
        public virtual DbSet<PerformanceRepertoire> PerformanceRepertoire { get; set; }
        public virtual DbSet<Preparation> Preparation { get; set; }
        public virtual DbSet<PreparationType> PreparationType { get; set; }
        public virtual DbSet<Repertoire> Repertoire { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Skill> Skill { get; set; }
        public virtual DbSet<Specialization> Specialization { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }
        public virtual DbSet<TicketClass> TicketClass { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserPerformance> UserPerformance { get; set; }
        public virtual DbSet<UserSkill> UserSkill { get; set; }
        public virtual DbSet<UserSpecialization> UserSpecialization { get; set; }
    }
}
