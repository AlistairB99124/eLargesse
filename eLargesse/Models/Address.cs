//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eLargesse.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Address
    {
        public int Id { get; set; }
        public Nullable<int> ClientId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string CIty { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    
        public virtual Client Client { get; set; }
    }
}