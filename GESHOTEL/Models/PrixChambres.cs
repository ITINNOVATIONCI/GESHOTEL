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
    
    public partial class PrixChambres
    {
        public int idPrixChambres { get; set; }
        public int idTypeChambre { get; set; }
        public int idTypePrix { get; set; }
        public int idHotel { get; set; }
        public Nullable<decimal> Prix { get; set; }
        public string Etat { get; set; }
    
        public virtual TypeChambres TypeChambres { get; set; }
        public virtual TypePrix TypePrix { get; set; }
    }
}