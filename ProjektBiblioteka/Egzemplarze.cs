//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjektBiblioteka
{
    using System;
    using System.Collections.Generic;
    
    public partial class Egzemplarze
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Egzemplarze()
        {
            this.Wypozyczenia = new HashSet<Wypozyczenia>();
        }
    
        public int idEgzemplarza { get; set; }
        public int idKsiazki { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Wypozyczenia> Wypozyczenia { get; set; }
        public virtual Ksiazki Ksiazki { get; set; }
    }
}
