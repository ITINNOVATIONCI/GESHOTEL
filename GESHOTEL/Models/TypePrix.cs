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
    
    public partial class TypePrix
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TypePrix()
        {
            this.PrixChambres = new HashSet<PrixChambres>();
        }
    
        public int idTypePrix { get; set; }
        public string Libelle { get; set; }
        public string Etat { get; set; }
        public Nullable<int> idHotel { get; set; }
        public Nullable<int> idReservationType { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrixChambres> PrixChambres { get; set; }
        public virtual ReservationTypes ReservationTypes { get; set; }
    }
}
