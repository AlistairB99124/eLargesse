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
    
    public partial class CategoryGroup
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public int CategoryId { get; set; }
    
        public virtual Article Article { get; set; }
        public virtual ArticleCategory ArticleCategory { get; set; }
    }
}
