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
    
    public partial class P_MAJ_PNPRV_Result
    {
        public int MOVEMENT_ID { get; set; }
        public string BANK_CODE { get; set; }
        public string FLOW_CODE { get; set; }
        public Nullable<System.DateTime> BOOK_DATE { get; set; }
        public Nullable<System.DateTime> VALUE_DATE { get; set; }
        public Nullable<decimal> AMOUNT { get; set; }
        public string SIGNE { get; set; }
        public string ACC_CODE { get; set; }
        public string IBC_ZONE_1 { get; set; }
        public string IBC_ZONE_2 { get; set; }
        public string REFERENCE_NUMBER { get; set; }
        public string ELECTRONIC_VALUE { get; set; }
        public Nullable<bool> Ref { get; set; }
        public bool Validé { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<bool> MAN_Reception { get; set; }
        public Nullable<System.DateTime> MAN_Date_reception { get; set; }
        public string CMP_CODE { get; set; }
        public string SITE { get; set; }
        public string cmp { get; set; }
        public string MAN_Ref_Bord { get; set; }
        public Nullable<bool> PNP { get; set; }
        public bool FS { get; set; }
        public Nullable<int> FSS { get; set; }
        public Nullable<int> Id_Revision { get; set; }
        public Nullable<bool> E { get; set; }
    }
}
