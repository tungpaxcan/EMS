﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EMS.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EMSEntities : DbContext
    {
        public EMSEntities()
            : base("name=EMSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Admin> Admins { get; set; }
        public DbSet<AttendEvent> AttendEvents { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<EmailAdmin> EmailAdmins { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<PersonVoted> PersonVoteds { get; set; }
        public DbSet<TypeCustomer> TypeCustomers { get; set; }
        public DbSet<VoteNew> VoteNews { get; set; }
    }
}
