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
    
    public partial class TR_Type_Document
    {
        public TR_Type_Document()
        {
            this.TR_USER_FLUX = new HashSet<TR_USER_FLUX>();
            this.TR_Type_Operation = new HashSet<TR_Type_Operation>();
            this.BK_MAN_REPORT = new HashSet<BK_MAN_REPORT>();
        }
    
        public int Id_Type_Document { get; set; }
        public string Code_Type_Document { get; set; }
        public Nullable<int> Id_Type_Transaction { get; set; }
    
        public virtual ICollection<TR_USER_FLUX> TR_USER_FLUX { get; set; }
        public virtual TR_Type_Transaction TR_Type_Transaction { get; set; }
        public virtual ICollection<TR_Type_Operation> TR_Type_Operation { get; set; }
        public virtual ICollection<BK_MAN_REPORT> BK_MAN_REPORT { get; set; }
    }
}
