//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MasrafTakip.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Masraflar
    {
        public int MasrafID { get; set; }
        public Nullable<int> PersonelID { get; set; }
        public string Baslik { get; set; }
        public Nullable<decimal> Tutar { get; set; }
        public string Aciklama { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
        public string OnayDurumu { get; set; }
    
        public virtual Personeller Personeller { get; set; }
    }
}
