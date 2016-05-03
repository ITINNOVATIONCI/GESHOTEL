using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls.ScheduleView;
using Telerik.Windows.Controls;
using GESHOTEL.ReservationsModules.ViewModel;

namespace GESHOTEL.ReservationsModules
{
    public class CustomAppointment : Appointment
    {
        private int reservationID;
        private string nbreHr;
        private string chambre;
        private string localite;
        private string etat;


        public CustomAppointment()
        {

            this.uniqueId = Guid.NewGuid().ToString();
        }

        private string uniqueId;
        public string UniqueId
        {
            get
            {
                return this.uniqueId;
            }
            set
            {
                if (this.uniqueId != value)
                {
                    this.uniqueId = value;
                    OnPropertyChanged("UniqueId");
                }
            }
        }

        public int ReservationID
        {
            get
            {
                return this.reservationID;
            }
            set
            {
                if (this.reservationID != value)
                {
                    this.reservationID = value;
                    OnPropertyChanged("ReservationID");
                }
            }
        }

        public string ChambreID
        {
            get
            {
                IResource resource = this.Resources.Where(r => r.ResourceType == "Chambres").FirstOrDefault();
                if (resource != null)
                {
                    return resource.ResourceName;
                }
                return null;
            }
            set
            {
                this.Resources.Add(new ChambreResource(value, "Chambres"));
                OnPropertyChanged("ChambreID");
            }
        }

        //public string TypeChambreID
        //{
        //    get
        //    {
        //        IResource resource = this.Resources.Where(r => r.ResourceType == "TypeChambres").FirstOrDefault();
        //        if (resource != null)
        //        {
        //            return resource.ResourceName;
        //        }
        //        return null;
        //    }
        //    set
        //    {
        //        this.Resources.Add(new TypeChambreResource(value, "TypeChambres"));
        //        OnPropertyChanged("TypeChambreID");
        //    }
        //}

        public string NbreHeure
        {
            get
            {
                return this.nbreHr;
            }
            set
            {
                this.nbreHr = value;
            }
        }

        public string Chambre
        {
            get
            {
                return this.chambre;
            }
            set
            {
                this.chambre = value;
            }
        }

        public string Localite
        {
            get
            {
                return this.localite;
            }
            set
            {
                this.localite = value;
            }
        }

        public string Etat
        {
            get
            {
                return this.etat;
            }
            set
            {
                this.etat = value;
            }
        }

        public override ICategory Category
        {
            get
            {
                return base.Category;
            }
            set
            {
                base.Category = value;
            }
        }

        public override ITimeMarker TimeMarker
        {
            get
            {
                return base.TimeMarker;
            }
            set
            {
                base.TimeMarker = value;
            }
        }

        public override IAppointment Copy()
        {
            IAppointment appointment = new CustomAppointment();
            appointment.CopyFrom(this);
            return appointment;
        }

        public override void CopyFrom(IAppointment other)
        {
            base.CopyFrom(other);
            var appointment = other as CustomAppointment;
            if (appointment != null)
            {
                this.UniqueId = appointment.UniqueId;
                this.Chambre = appointment.Chambre;
                this.Etat = appointment.Etat;
                this.ChambreID = appointment.ChambreID;
                //this.TypeChambreID = appointment.TypeChambreID;
                this.Localite = appointment.Localite;
            }
        }

    }
}
