//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PBE.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class TR_Type_Transaction
    {
        public TR_Type_Transaction()
        {
            this.TR_Type_Document = new HashSet<TR_Type_Document>();
        }
    
        public int Id_Type_Transaction { get; set; }
        public string Code_Type_Transaction { get; set; }
        public string Libelle_Type_Transaction { get; set; }
    
        public virtual ICollection<TR_Type_Document> TR_Type_Document { get; set; }
    }
}