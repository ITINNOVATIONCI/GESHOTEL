//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GESHOTEL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Nationalités
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Nationalités()
        {
            this.Clients = new HashSet<Clients>();
        }
    
        public int ID { get; set; }
        public string Libelle { get; set; }
        public Nullable<int> idHotel { get; set; }
        public string Etat { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Clients> Clients { get; set; }
    }
}
