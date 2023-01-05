//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Customer
    {
        public Customer()
        {
            this.AttendEvents = new HashSet<AttendEvent>();
            this.PersonVoteds = new HashSet<PersonVoted>();
        }
    
        public string Id { get; set; }
        public Nullable<int> IdTypeCustomer { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public Nullable<int> Phone { get; set; }
        public string Position { get; set; }
        public string Company { get; set; }
        public Nullable<bool> New { get; set; }
        public string Des { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<bool> StatusEmail { get; set; }
        public Nullable<bool> Ok { get; set; }
    
        public virtual ICollection<AttendEvent> AttendEvents { get; set; }
        public virtual TypeCustomer TypeCustomer { get; set; }
        public virtual ICollection<PersonVoted> PersonVoteds { get; set; }
    }
}
