//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ahp.Core.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Kriteria
    {
        public int Kode { get; set; }
        public string Nama { get; set; }
        public Nullable<int> Faktor { get; set; }
        public Nullable<int> NilaiTarget { get; set; }
        public Nullable<int> JumlahBaris { get; set; }
        public Nullable<double> EigenvectorScore { get; set; }
        public Nullable<double> NilaiKali { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
