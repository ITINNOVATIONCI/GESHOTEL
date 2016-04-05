using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace GESHOTEL.Models
{
    public partial class ReservationTypes : ICategory
    {
        private Brush categoryBrush;
        public Brush CategoryBrush
        {
            get
            {

                switch (ReservationEtat)
                {
                    case "ACTIF":
                        return this.categoryBrush = SolidColorBrushHelper.FromNameString(this.Couleur);
                        break;

                    case "DUE OUT":
                        return this.categoryBrush = SolidColorBrushHelper.FromNameString("Red");
                        break;

                    case "TERMINER":
                        return this.categoryBrush = SolidColorBrushHelper.FromNameString("Green");
                        break;

                    case "RESERVER":
                        return this.categoryBrush = SolidColorBrushHelper.FromNameString("Brown");
                        break;

                    default:
                        return this.categoryBrush = SolidColorBrushHelper.FromNameString(this.Couleur);
                        break;
                }




                return this.categoryBrush;
            }
            set
            {
                this.Couleur = (this.categoryBrush as SolidColorBrush).Color.ToString().Substring(1);
                this.categoryBrush = value;
            }
        }

        public string CategoryName
        {
            get
            {

                return this.ReservationType;
            }
            set
            {
                this.ReservationType = value;
            }

        }

        public string DisplayName
        {
            get
            {
                return this.ReservationType;
            }
            set
            {
                this.ReservationType = value;
            }
        }

        string reservationEtat;
        public string ReservationEtat
        {
            get
            {
                return this.reservationEtat;
            }
            set
            {
                this.reservationEtat = value;
            }
        }


        public bool Equals(ICategory other)
        {
            return this.ReservationType == other.DisplayName;
        }
    }

    public partial class TimeMarkers : ITimeMarker
    {
        private Brush timeMarkerBrush;

        public Brush TimeMarkerBrush
        {
            get
            {
                if (this.timeMarkerBrush == null)
                {
                    this.timeMarkerBrush = SolidColorBrushHelper.FromNameString(this.TimeMarkerBrushName);
                }

                return this.timeMarkerBrush;
            }
            set
            {
                this.TimeMarkerBrushName = (this.timeMarkerBrush as SolidColorBrush).Color.ToString().Substring(1);
                this.timeMarkerBrush = value;
            }
        }

        public bool Equals(ITimeMarker other)
        {
            return this.TimeMarkerName != other.TimeMarkerName;
        }
    }

    [MetadataType(typeof(ReservationsMetadata))]
    public partial class Reservations
    {
        public string ChambreNumero
        {
            get
            {

                return this.Chambres.Numero;



            }
        }

        public string EtatCouleur
        {
            get
            {
                switch (Etat)
                {
                    case "ACTIF":
                        return this.ReservationTypes.Couleur;
                        break;

                    case "DUE OUT":
                        return "Red";
                        break;

                    case "TERMINER":
                        return "Green";
                        break;

                    case "RESERVER":
                        return "Brown";
                        break;

                    default:
                        return "White";
                        break;
                }


            }
        }

    }

    public class ReservationsMetadata
    {

        #region "ShortName"

        [Display(ShortName = "Chambre")]
        public object ChambreNumero { get; set; }   // note the 'object' type

        [Display(ShortName = "Type de Sejour")]
        public object idReservationType { get; set; }   // note the 'object' type

        [Display(ShortName = "Clients")]
        public object idClient { get; set; }   // note the 'object' type

        [Display(ShortName = "Date d'arrivée")]
        public object DateArrive { get; set; }   // note the 'object' type

        [Display(ShortName = "Date de départ")]
        public object DateDepart { get; set; }   // note the 'object' type

        [Display(ShortName = "Nbre de nuit")]
        public object NbreNuit { get; set; }   // note the 'object' type

        [Display(ShortName = "Total")]
        public object TotalTTC { get; set; }   // note the 'object' type

        [Display(ShortName = "Payé")]
        public object TotalPaye { get; set; }   // note the 'object' type

        [Display(ShortName = "Restant")]
        public object TotalReste { get; set; }   // note the 'object' type

        [Display(ShortName = "Date du CheckIn")]
        public object DateCheckIn { get; set; }   // note the 'object' type

        [Display(ShortName = "Date du CheckOut")]
        public object DateCheckOut { get; set; }   // note the 'object' type

        [Display(ShortName = "CheckIn")]
        public object isCheckIn { get; set; }   // note the 'object' type

        [Display(ShortName = "CheckOut")]
        public object isCheckOut { get; set; }   // note the 'object' type

        [Display(ShortName = "Etat")]
        public object EtatOperation { get; set; }   // note the 'object' type

        #endregion

        #region "AutoGenerateFilter"

        [Display(AutoGenerateFilter = false)]
        public object Date { get; set; }   // note the 'object' type

        [Display(AutoGenerateFilter = false)]
        public object Etat { get; set; }   // note the 'object' type

        [Display(AutoGenerateFilter = false)]
        public object EtatCouleur { get; set; }   // note the 'object' type

        [Display(AutoGenerateFilter = false)]
        public object idRaison { get; set; }   // note the 'object' type

        [Display(AutoGenerateFilter = false)]
        public object idRegistre { get; set; }   // note the 'object' type

        [Display(AutoGenerateFilter = false)]
        public object idPromotion { get; set; }   // note the 'object' type

        [Display(AutoGenerateFilter = false)]
        public object idHotel { get; set; }   // note the 'object' type

        [Display(AutoGenerateFilter = false)]
        public object TypeOperation { get; set; }   // note the 'object' type

        [Display(AutoGenerateFilter = false)]
        public object NbreAdult { get; set; }   // note the 'object' type

        [Display(AutoGenerateFilter = false)]
        public object NbreEnfant { get; set; }   // note the 'object' type

        [Display(AutoGenerateFilter = false)]
        public object idChambre { get; set; }   // note the 'object' type

        [Display(AutoGenerateFilter = false)]
        public object Chambres { get; set; }   // note the 'object' type

        [Display(AutoGenerateFilter = false)]
        public object ReservationTypes { get; set; }   // note the 'object' type

        [Display(AutoGenerateFilter = false)]
        public object Raisons { get; set; }   // note the 'object' type

        [Display(AutoGenerateFilter = false)]
        public object Promotions { get; set; }   // note the 'object' type

        [Display(AutoGenerateFilter = false)]
        public object Clients { get; set; }   // note the 'object' type

        [Display(AutoGenerateFilter = false)]
        public object Transactions { get; set; }   // note the 'object' type

        #endregion

    }

    public partial class Clients
    {

        public string FullName
        {
            get
            {
                string ret = string.Empty;
                if (!String.IsNullOrEmpty(Noms))
                    ret += Noms;
                if (!String.IsNullOrEmpty(Prenoms))
                    ret += " " + Prenoms;
                return ret;
            }
        }

    }

    public partial class Employees 
    {

        public string FullName
        {
            get
            {
                string ret = string.Empty;
                if (!String.IsNullOrEmpty(Nom))
                    ret += Nom;
                if (!String.IsNullOrEmpty(Prenoms))
                    ret += " " + Prenoms;
                return ret;
            }
        }

    }

}
