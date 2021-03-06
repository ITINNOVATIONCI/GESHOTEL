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
    
    public partial class ListeReservation
    {
        public int ID { get; set; }
        public string ReservationType { get; set; }
        public string TypeChambres { get; set; }
        public string Chambre { get; set; }
        public string Client { get; set; }
        public Nullable<System.DateTime> DateArrive { get; set; }
        public Nullable<System.DateTime> DateDepart { get; set; }
        public Nullable<System.DateTime> DateCheckIn { get; set; }
        public Nullable<System.DateTime> DateCheckOut { get; set; }
        public Nullable<int> NbreNuit { get; set; }
        public Nullable<int> NbreAdult { get; set; }
        public Nullable<int> NbreEnfant { get; set; }
        public string Etat { get; set; }
        public string EtatOperation { get; set; }
        public int Passage { get; set; }
        public int Nuit { get; set; }
        public int Sejour { get; set; }
        public int Maintenance { get; set; }
        public int Annuler { get; set; }
        public string Status { get; set; }
        public Nullable<int> idRegistre { get; set; }
        public Nullable<int> idHotel { get; set; }
        public string Hotel { get; set; }
        public Nullable<decimal> TotalTTC { get; set; }
        public Nullable<decimal> TotalPaye { get; set; }
        public Nullable<decimal> TotalReste { get; set; }
        public Nullable<System.DateTime> ReservationDate { get; set; }
        public Nullable<System.DateTime> CancelDate { get; set; }
    }
}
