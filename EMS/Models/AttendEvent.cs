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
    
    public partial class AttendEvent
    {
        public int Id { get; set; }
        public string IdCustomer { get; set; }
        public Nullable<bool> Status { get; set; }
    
        public virtual Customer Customer { get; set; }
    }
}