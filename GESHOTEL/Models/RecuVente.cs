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
    
    public partial class RecuVente
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.DateTime> DateArrive { get; set; }
        public Nullable<System.DateTime> DateDepart { get; set; }
        public string ReservationType { get; set; }
        public Nullable<int> NbreNuit { get; set; }
        public Nullable<decimal> TotalTTC { get; set; }
        public Nullable<decimal> TotalPaye { get; set; }
        public Nullable<decimal> TotalReste { get; set; }
        public string Numero { get; set; }
        public string Libelle { get; set; }
        public string TransactionNumero { get; set; }
    }
}
