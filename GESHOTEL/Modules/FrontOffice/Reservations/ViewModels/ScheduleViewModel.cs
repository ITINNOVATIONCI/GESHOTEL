using GESHOTEL.Models;
using GESHOTEL.COMMON;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Scheduling;
using System.Data.Objects;
using System.Windows.Threading;
using GESHOTEL.ReservationsModules.ViewModel;

namespace GESHOTEL.ReservationsModules.ViewModel
{
    public class ScheduleViewModel : ViewModelBase
    {
        private Telerik.Windows.Controls.ResourceType resourceType = new Telerik.Windows.Controls.ResourceType("Chambres");
        private Telerik.Windows.Controls.ResourceType resourceTypeTC = new Telerik.Windows.Controls.ResourceType("TypeChambres");
        private ICommand refreshCommand;
        private ObservableCollection<CustomAppointment> appointments;
        private ResourceTypeCollection resourceTypes;
        private bool _IsLoading;
        private int ID;

        public bool IsLoading
        {
            get { return _IsLoading; }
            set
            {
                _IsLoading = value;

                this.OnPropertyChanged("IsLoading");
            }
        }

        DateTime _CurrentDate;

        public DateTime CurrentDate
        {
            get { return _CurrentDate; }
            set
            {
                _CurrentDate = value;
                this.OnPropertyChanged("CurrentDate");
                LoadData();
            }
        }

        private ReservationTypes selectedCategory;


        public ScheduleViewModel()
        {
            this.Categories = new ObservableCollection<ReservationTypes>();
            _CurrentDate = DateTime.Now;
            //this.ID = ID;
            LoadData();

            //DispatcherTimer messageTimer = new DispatcherTimer();
            //messageTimer.Tick += new EventHandler(messageTimer_Tick);
            //messageTimer.Interval = TimeSpan.FromSeconds(1);
            //messageTimer.Start();
            

        }

        void messageTimer_Tick(object sender, EventArgs e)
        {
            //GlobalData.model.Refresh(RefreshMode.StoreWins, GlobalData.model.Reservations);


            //this.appointments = LoadAppointments(GlobalData.model.Reservations.Where(a => (a.idHotel == GlobalData.HotId)));

        }

        public void LoadData()
        {
            //GlobalData.Init();
            //GlobalData.model.Refresh(RefreshMode.StoreWins, GlobalData.model.Reservations);

            IsLoading = true;

            this.Categories.Clear();
            this.Categories.AddRange(GlobalData.model.ReservationTypes.ToList());

            DateTime datedebut = CurrentDate.AddDays(-14);
            DateTime datefin = CurrentDate.AddDays(14);
            //string datedebut = "05/08/2013";

            var result = GlobalData.model.Reservations.Where(a => (a.DateArrive >= datedebut && a.DateArrive < datefin && a.EtatOperation != "TERMINER" && a.EtatOperation != "ANNULER")).ToList<Reservations>();

            this.appointments = LoadAppointments(result);

            ResourceTypes.Remove(resourceType);
            resourceType.Resources.Clear();
            resourceType.Resources.AddRange(this.GetResources());
            resourceTypes.Add(resourceType);

            //this.OnPropertyChanged("Appointments");

            IsLoading = false;
        }

        // Transform the given rental orders to custom appointments ready to be bound to the scheduler
        public ObservableCollection<CustomAppointment> LoadAppointments(IEnumerable<Reservations> rentalOrders)
        {
            IEnumerable<Chambres> cars = GlobalData.model.Chambres.Where(c => c.Etat != "SUPPRIMER");
            IEnumerable<ReservationTypes> cats = GlobalData.model.ReservationTypes.Where(c => c.Etat != "SUPPRIMER");
            IEnumerable<TimeMarkers> tm = GlobalData.model.TimeMarkers;

            ObservableCollection<CustomAppointment> appointments = new ObservableCollection<CustomAppointment>();
            if (rentalOrders != null)
            {
                foreach (Reservations order in rentalOrders)
                {
                    try
                    {
                        CustomAppointment app = new CustomAppointment();
                        Chambres cham = cars.First(x => x.ID == order.idChambre);
                        ReservationTypes category = cats.First(x => x.ID == order.idReservationType);
                        TimeMarkers timemarker = tm.First(x => x.TimeMarkerName == order.EtatOperation);
                        app.UniqueId = order.ID.ToString();
                        app.ReservationID = order.ID;
                        app.ChambreID = order.idChambre.ToString();
                        app.Start = order.DateArrive.Value;
                        app.End = order.DateDepart.Value;
                        app.Category = category;
                        app.TimeMarker = timemarker;
                        app.Chambre = "Chambres : " + cham.Numero;

                        //app.TypeChambreID = cham.TypeChambre.ToString();

                        if (order.ReservationTypes.ReservationType == "PASSAGE")
                        {
                            app.Subject = order.ReservationTypes.ReservationType;
                            int nbr = -order.DateArrive.Value.Subtract(order.DateDepart.Value).Hours;
                            app.NbreHeure = "" + nbr + " Hr";
                        }
                        else if (order.ReservationTypes.ReservationType == "MAINTENANCE")
                        {
                            app.Subject = order.ReservationTypes.ReservationType;
                            app.NbreHeure = "" + order.NbreNuit + " Nuit";
                        }
                        else
                        {
                            app.Subject = order.Clients.Noms;
                            app.NbreHeure = "" + order.NbreNuit + " Nuit";
                        }


                        if (order.Etat == "RESERVER")
                        {
                            ReservationTypes Dcategory = cats.First(x => x.CategoryName == "RESERVATION");
                            Dcategory.ReservationEtat = order.Etat;
                            app.Category = Dcategory;
                        }

                        if (order.Etat == "DUE OUT")
                        {
                            ReservationTypes Dcategory = cats.First(x => x.CategoryName == "DUE OUT");
                            Dcategory.ReservationEtat = order.Etat;
                            app.Category = Dcategory;
                        }

                        //category.ReservationEtat = order.Etat;
                        app.Etat = order.Etat;
                        //app.Localite = order.Localite;
                        appointments.Add(app);
                    }
                    catch (Exception)
                    {
                        
                    }
                }
            }
            return appointments;
        }

        /// <summary>
        /// Represents the rental orders
        /// </summary>
        public ObservableCollection<CustomAppointment> Appointments
        {
            get
            {
                if (this.appointments == null)
                {
                    //GlobalData.model.Refresh(RefreshMode.StoreWins, GlobalData.model.Reservations);
                    this.appointments = LoadAppointments(GlobalData.model.Reservations);

                }

                return this.appointments;
            }
        }

        public ObservableCollection<ReservationTypes> Categories
        {
            get;
            private set;
        }

        public ReservationTypes SelectedCategory
        {
            get
            {
                return this.selectedCategory;
            }
            set
            {
                if (this.selectedCategory != value)
                {
                    this.selectedCategory = value;
                    this.OnPropertyChanged("SelectedCategory");
                }
            }
        }

        public ResourceTypeCollection ResourceTypes
        {
            get
            {
                if (resourceTypes == null)
                {
                    resourceTypes = new ResourceTypeCollection();
                    resourceTypes.Add(resourceType);
                    //resourceTypes.Add(resourceTypeTC);
                }

                return resourceTypes;
            }
        }

        private ObservableCollection<Telerik.Windows.Controls.IResource> GetResources()
        {
            var resources = new ObservableCollection<Telerik.Windows.Controls.IResource>();

            foreach (var car in GlobalData.model.Chambres.Where(c => c.Etat != "SUPPRIMER"))
            {
                string resourceType = "Chambres";
                Telerik.Windows.Controls.Resource resource = new ChambreResource(car.ID.ToString(), resourceType);
                resource.DisplayName = car.Numero;
                resources.Add(resource);
            }

            return resources;
        }

        private ObservableCollection<Telerik.Windows.Controls.IResource> GetResourcesTC()
        {
            var resources = new ObservableCollection<Telerik.Windows.Controls.IResource>();

            foreach (var car in GlobalData.model.TypeChambres.Where(c => c.Etat == "ACTIF"))
            {
                string resourceType = "TypeChambres";
                Telerik.Windows.Controls.Resource resource = new TypeChambreResource(car.ID.ToString(), resourceType);
                resource.DisplayName = car.Libelle;
                resources.Add(resource);
            }


            return resources;
        }

    }
}
