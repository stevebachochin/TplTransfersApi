//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TplTransfersApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Depopulation_Batch
    {
        public int did { get; set; }
        public string DonorID { get; set; }
        public string TransferStep { get; set; }
        public Nullable<System.DateTime> TransferDate { get; set; }
        public string SID_UNID { get; set; }
    }
}