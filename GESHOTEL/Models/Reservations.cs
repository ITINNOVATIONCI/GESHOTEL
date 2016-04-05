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
    
    public partial class Reservations
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reservations()
        {
            this.Transactions = new HashSet<Transactions>();
        }
    
        public int ID { get; set; }
        public Nullable<int> idChambre { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.DateTime> DateArrive { get; set; }
        public Nullable<System.DateTime> DateDepart { get; set; }
        public Nullable<int> idReservationType { get; set; }
        public Nullable<int> NbreNuit { get; set; }
        public Nullable<int> NbreAdult { get; set; }
        public Nullable<int> NbreEnfant { get; set; }
        public Nullable<int> idClient { get; set; }
        public Nullable<decimal> TotalTTC { get; set; }
        public Nullable<decimal> TotalPaye { get; set; }
        public Nullable<decimal> TotalReste { get; set; }
        public Nullable<int> idRaison { get; set; }
        public Nullable<int> idRegistre { get; set; }
        public Nullable<decimal> Reduction { get; set; }
        public Nullable<System.DateTime> DateCheckIn { get; set; }
        public Nullable<bool> isCheckIn { get; set; }
        public Nullable<System.DateTime> DateCheckOut { get; set; }
        public Nullable<bool> isCheckOut { get; set; }
        public Nullable<System.DateTime> ReservationDate { get; set; }
        public Nullable<System.DateTime> CancelDate { get; set; }
        public Nullable<int> idPromotion { get; set; }
        public string EtatOperation { get; set; }
        public string Etat { get; set; }
        public Nullable<int> idHotel { get; set; }
        public string TypeOperation { get; set; }
        public Nullable<int> idTypeChambre { get; set; }
    
        public virtual Chambres Chambres { get; set; }
        public virtual Clients Clients { get; set; }
        public virtual Promotions Promotions { get; set; }
        public virtual Raisons Raisons { get; set; }
        public virtual ReservationTypes ReservationTypes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transactions> Transactions { get; set; }
        public virtual TypeChambres TypeChambres { get; set; }
    }
}