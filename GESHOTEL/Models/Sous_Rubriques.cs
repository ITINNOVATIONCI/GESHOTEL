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
    
    public partial class Sous_Rubriques
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sous_Rubriques()
        {
            this.DetatilDepenses = new HashSet<DetatilDepenses>();
        }
    
        public int idSousRubrique { get; set; }
        public string Libelle { get; set; }
        public Nullable<double> Prix { get; set; }
        public Nullable<int> idRubrique { get; set; }
        public string Etat { get; set; }
        public Nullable<int> IdHotel { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetatilDepenses> DetatilDepenses { get; set; }
        public virtual Rubriques Rubriques { get; set; }
    }
}
