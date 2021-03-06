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
    
    public partial class Article
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Article()
        {
            this.CategoryGroups = new HashSet<CategoryGroup>();
        }
    
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Category { get; set; }
        public Nullable<int> CommentId { get; set; }
        public string FeatureImg { get; set; }
    
        public virtual Comment Comment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CategoryGroup> CategoryGroups { get; set; }
        public virtual Post Post { get; set; }
    }
}
