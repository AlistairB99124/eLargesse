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
    
    public partial class Wishlist
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Wishlist()
        {
            this.WishlistItems = new HashSet<WishlistItem>();
        }
    
        public int Id { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public bool IsCurrent { get; set; }
        public Nullable<bool> Shared { get; set; }
    
        public virtual Client Client { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WishlistItem> WishlistItems { get; set; }
    }
}