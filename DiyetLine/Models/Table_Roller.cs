//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DiyetLine.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Table_Roller
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Table_Roller()
        {
            this.Table_IsletmeSahibi = new HashSet<Table_IsletmeSahibi>();
            this.Table_Kullanicilar = new HashSet<Table_Kullanicilar>();
        }
    
        public int Sq_id { get; set; }
        public string RolAdi { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Table_IsletmeSahibi> Table_IsletmeSahibi { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Table_Kullanicilar> Table_Kullanicilar { get; set; }
    }
}
