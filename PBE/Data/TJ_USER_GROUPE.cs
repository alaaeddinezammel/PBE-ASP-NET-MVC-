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
    
    public partial class TJ_USER_GROUPE
    {
        public int id_User_Group { get; set; }
        public string Matricule { get; set; }
        public Nullable<int> Id_Groupe_Utilisateur { get; set; }
        public string CMP_CODE { get; set; }
    
        public virtual TR_Groupe_Utilisateur TR_Groupe_Utilisateur { get; set; }
        public virtual Param_User Param_User { get; set; }
        public virtual COMPANy COMPANy { get; set; }
    }
}
