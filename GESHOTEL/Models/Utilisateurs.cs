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
    
    public partial class Utilisateurs
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Utilisateurs()
        {
            this.Permissions = new HashSet<Permissions>();
        }
    
        public int idUtilisateur { get; set; }
        public string Nom { get; set; }
        public string Prenoms { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Etat { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Permissions> Permissions { get; set; }
    }
}
