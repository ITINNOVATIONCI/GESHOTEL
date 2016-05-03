﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GESHOTELEntities : DbContext
    {
        public GESHOTELEntities()
            : base("name=GESHOTELEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Amenities> Amenities { get; set; }
        public virtual DbSet<AmenitiesTypes> AmenitiesTypes { get; set; }
        public virtual DbSet<BI> BI { get; set; }
        public virtual DbSet<CancelationRules> CancelationRules { get; set; }
        public virtual DbSet<Cancelations> Cancelations { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<CategorieSchedule> CategorieSchedule { get; set; }
        public virtual DbSet<Chambres> Chambres { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Communes> Communes { get; set; }
        public virtual DbSet<Comptes> Comptes { get; set; }
        public virtual DbSet<Conditionnements> Conditionnements { get; set; }
        public virtual DbSet<DetailPaiements> DetailPaiements { get; set; }
        public virtual DbSet<DetailTransactions> DetailTransactions { get; set; }
        public virtual DbSet<DetatilDepenses> DetatilDepenses { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<EtatOperation> EtatOperation { get; set; }
        public virtual DbSet<Fournisseurs> Fournisseurs { get; set; }
        public virtual DbSet<HotelInfo> HotelInfo { get; set; }
        public virtual DbSet<IDTypes> IDTypes { get; set; }
        public virtual DbSet<MethodePaiements> MethodePaiements { get; set; }
        public virtual DbSet<Nationalités> Nationalités { get; set; }
        public virtual DbSet<OperationRules> OperationRules { get; set; }
        public virtual DbSet<Parametres> Parametres { get; set; }
        public virtual DbSet<Pays> Pays { get; set; }
        public virtual DbSet<PrixChambres> PrixChambres { get; set; }
        public virtual DbSet<Produits> Produits { get; set; }
        public virtual DbSet<Promotions> Promotions { get; set; }
        public virtual DbSet<Quartiers> Quartiers { get; set; }
        public virtual DbSet<Raisons> Raisons { get; set; }
        public virtual DbSet<RaisonType> RaisonType { get; set; }
        public virtual DbSet<Reductions> Reductions { get; set; }
        public virtual DbSet<Registres> Registres { get; set; }
        public virtual DbSet<Reservations> Reservations { get; set; }
        public virtual DbSet<ReservationTypes> ReservationTypes { get; set; }
        public virtual DbSet<Rubriques> Rubriques { get; set; }
        public virtual DbSet<Rules> Rules { get; set; }
        public virtual DbSet<Sous_Rubriques> Sous_Rubriques { get; set; }
        public virtual DbSet<TimeMarkers> TimeMarkers { get; set; }
        public virtual DbSet<TranReductions> TranReductions { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
        public virtual DbSet<TypeChambres> TypeChambres { get; set; }
        public virtual DbSet<TypeEmployees> TypeEmployees { get; set; }
        public virtual DbSet<TypePrix> TypePrix { get; set; }
        public virtual DbSet<Villes> Villes { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<Utilisateurs> Utilisateurs { get; set; }
        public virtual DbSet<ListDepense> ListDepense { get; set; }
        public virtual DbSet<ReservationDashBoard> ReservationDashBoard { get; set; }
        public virtual DbSet<ChambreDisponibilite> ChambreDisponibilite { get; set; }
        public virtual DbSet<ChambreMaintenance> ChambreMaintenance { get; set; }
        public virtual DbSet<ChambreNuitRapport> ChambreNuitRapport { get; set; }
        public virtual DbSet<ChambreReserver> ChambreReserver { get; set; }
        public virtual DbSet<ListePrixChambre> ListePrixChambre { get; set; }
        public virtual DbSet<ListeReservation> ListeReservation { get; set; }
        public virtual DbSet<ListeStatChambre> ListeStatChambre { get; set; }
        public virtual DbSet<RecuReservations> RecuReservations { get; set; }
        public virtual DbSet<RecuVente> RecuVente { get; set; }
        public virtual DbSet<ReservationCheckIn> ReservationCheckIn { get; set; }
        public virtual DbSet<ReservationCheckOut> ReservationCheckOut { get; set; }
    }
}
