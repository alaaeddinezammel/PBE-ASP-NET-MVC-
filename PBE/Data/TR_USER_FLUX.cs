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
    
    public partial class TR_USER_FLUX
    {
        public int id_User_Flux { get; set; }
        public string BANK_CODE { get; set; }
        public string FLOW_CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<bool> I { get; set; }
        public Nullable<bool> FS { get; set; }
        public string Note { get; set; }
        public Nullable<bool> V { get; set; }
        public Nullable<bool> Electronique { get; set; }
        public Nullable<int> Delais { get; set; }
        public Nullable<int> Id_Type_Document { get; set; }
        public Nullable<int> Id_Groupe_Utilisateur { get; set; }
    
        public virtual TR_Type_Document TR_Type_Document { get; set; }
        public virtual TR_Groupe_Utilisateur TR_Groupe_Utilisateur { get; set; }
        public virtual TR_USER_BNK TR_USER_BNK { get; set; }
    }
}
