using GESHOTEL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Telerik.Windows.Controls;

namespace GESHOTEL.ReservationsModules.ViewModel
{
    public class RoomViewModel : GESHOTEL.COMMON.BaseClasses.ViewModelBase
    {
        #region Field
        GESHOTELEntities model;


        #endregion

        /* Note that this view GlobalData.Model does not actually use the services provided
         * by the ViewModel Base class. We show it as a base class here because 
         * most view models will use the base class services. */

        #region Constructor

        public RoomViewModel()
        {

            Load();

            DispatcherTimer messageTimer = new DispatcherTimer();
            messageTimer.Tick += new EventHandler(messageTimer_Tick);
            messageTimer.Interval = new TimeSpan(0, 0, 1, 0);
            messageTimer.Start();

        }

        void messageTimer_Tick(object sender, EventArgs e)
        {

            try
            {
                //model.Refresh(RefreshMode.StoreWins, model.Chambres);

                AllChambres = new ObservableCollection<Chambres>(model.Chambres.Where(c => c.Etat != "SUPPRIMER"));


            }
            catch (Exception)
            {

            }
        }

        #endregion

        #region Command Properties

        public ICommand RefreshCommand
        {
            get
            {
                return new ERP.LocationsModule.ViewModel.Commands.DelegateCommand(BeginRefresh, CanRefresh);
            }
        }

        public void BeginRefresh(object param)
        {

            Load();

        }

        private bool CanRefresh(object param)
        {

            return true;

            //return false;
        }

        public ICommand PassageCommand
        {
            get
            {
                return new ERP.LocationsModule.ViewModel.Commands.DelegateCommand(BeginPassage, CanPassage);
            }
        }

        public void BeginPassage(object param)
        {

            if (!GlobalData.VerifyClotureSession())
            {

                var result = MessageBox.Show("Voulez-vous fermer la session précédente ?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    RadDocumentPane radMenu = new RadDocumentPane();
                    radMenu.Content = new GESHOTEL.ReservationsModules.ClotureJourneeFrm("Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString());
                    radMenu.Header = "Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString();
                    GlobalData.rrvMain.Title = "Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString();
                    radMenu.FontFamily = new FontFamily("Perpetua Titling MT");
                    radMenu.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                    radMenu.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

                    if (!VerifyDock("Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString()))
                    {
                        GlobalData.PaneGroup.AddItem(radMenu, Telerik.Windows.Controls.Docking.DockPosition.Center);
                    }
                    else
                    {

                    }
                }

                return;
            }

            if (SelectedChambres.EtatOperation == "LIBRE" || SelectedChambres.EtatOperation == "RESERVER")
            {
                RadDocumentPane rad = new RadDocumentPane();
                rad.Content = new GESHOTEL.ReservationsModules.PassageUC(SelectedChambres.Numero);
                rad.Header = "Passage";
                GlobalData.rrvMain.Title = "Passage";
                rad.FontFamily = new FontFamily("Perpetua Titling MT");
                rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

                if (!VerifyDock("Passage"))
                {
                    GlobalData.PaneGroup.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
                }
                else
                {

                }
            }
            else if (SelectedChambres.EtatOperation == "SALLE")
            {
                MessageBox.Show("La chambre est salle veuillez la nettoyée", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show("Cette chambre est occupée", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }

        private bool CanPassage(object param)
        {

            return true;

            //return false;
        }

        public ICommand NuitSejourCommand
        {
            get
            {
                return new ERP.LocationsModule.ViewModel.Commands.DelegateCommand(BeginNuitSejour, CanNuitSejour);
            }
        }

        public void BeginNuitSejour(object param)
        {
            if (!GlobalData.VerifyClotureSession())
            {

                var result = MessageBox.Show("Voulez-vous fermer la session précédente ?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    RadDocumentPane radMenu = new RadDocumentPane();
                    radMenu.Content = new GESHOTEL.ReservationsModules.ClotureJourneeFrm("Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString());
                    radMenu.Header = "Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString();
                    GlobalData.rrvMain.Title = "Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString();
                    radMenu.FontFamily = new FontFamily("Perpetua Titling MT");
                    radMenu.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                    radMenu.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

                    if (!VerifyDock("Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString()))
                    {
                        GlobalData.PaneGroup.AddItem(radMenu, Telerik.Windows.Controls.Docking.DockPosition.Center);
                    }
                    else
                    {

                    }
                }

                return;
            }

            if (SelectedChambres.EtatOperation == "LIBRE" || SelectedChambres.EtatOperation == "RESERVER")
            {

                RadDocumentPane rad = new RadDocumentPane();
                rad.Content = new GESHOTEL.ReservationsModules.StayInsert(SelectedChambres.Numero, SelectedChambres.TypeChambres.Libelle);
                rad.Header = "Nuit/Sejour";
                GlobalData.rrvMain.Title = "Nuit/Sejour";
                rad.FontFamily = new FontFamily("Perpetua Titling MT");
                rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

                if (!VerifyDock("Nuit/Sejour"))
                {
                    GlobalData.PaneGroup.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
                }
                else
                {

                }

            }
            else if (SelectedChambres.EtatOperation == "SALLE")
            {
                MessageBox.Show("La chambre est salle veuillez la nettoyée", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show("Cette chambre est occupée", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private bool CanNuitSejour(object param)
        {

            return true;

            //return false;
        }

        public ICommand NewReservationCommand
        {
            get
            {
                return new ERP.LocationsModule.ViewModel.Commands.DelegateCommand(BeginNewReservation, CanNewReservation);
            }
        }

        public void BeginNewReservation(object param)
        {

            if (!GlobalData.VerifyClotureSession())
            {

                var result = MessageBox.Show("Voulez-vous fermer la session précédente ?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    RadDocumentPane radMenu = new RadDocumentPane();
                    radMenu.Content = new GESHOTEL.ReservationsModules.ClotureJourneeFrm("Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString());
                    radMenu.Header = "Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString();
                    GlobalData.rrvMain.Title = "Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString();
                    radMenu.FontFamily = new FontFamily("Perpetua Titling MT");
                    radMenu.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                    radMenu.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

                    if (!VerifyDock("Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString()))
                    {
                        GlobalData.PaneGroup.AddItem(radMenu, Telerik.Windows.Controls.Docking.DockPosition.Center);
                    }
                    else
                    {

                    }
                }

                return;
            }

            if (SelectedChambres.EtatOperation == "LIBRE" || SelectedChambres.EtatOperation == "RESERVER")
            {
                RadDocumentPane rad = new RadDocumentPane();
                rad.Content = new GESHOTEL.ReservationsModules.ReservationInsert(SelectedChambres.Numero, SelectedChambres.TypeChambres.Libelle);
                rad.Header = "Reservation";
                GlobalData.rrvMain.Title = "Reservation";
                rad.FontFamily = new FontFamily("Perpetua Titling MT");
                rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

                if (!VerifyDock("Reservation"))
                {
                    GlobalData.PaneGroup.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
                }
                else
                {

                }

            }
            else if (SelectedChambres.EtatOperation == "SALLE")
            {
                MessageBox.Show("La chambre est salle veuillez la nettoyée", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show("Cette chambre est occupée", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private bool CanNewReservation(object param)
        {

            return true;

            //return false;
        }

        public ICommand ChambrePropreCommand
        {
            get
            {
                return new ERP.LocationsModule.ViewModel.Commands.DelegateCommand(ChambrePropre, CanChambrePropre);
            }
        }

        public void ChambrePropre(object param)
        {
            if (SelectedChambres.EtatOperation != "SALLE" && SelectedChambres.EtatOperation != "INDISPONIBLE") return;

            SelectedChambres.EtatOperation = "LIBRE";

            model.SaveChanges();

            Load();


        }

        private bool CanChambrePropre(object param)
        {

            if (SelectedChambres == null) return false;

            return true;

            //return false;
        }

        public ICommand ChambreDispoCommand
        {
            get
            {
                return new ERP.LocationsModule.ViewModel.Commands.DelegateCommand(ChambreDispo, CanChambreDispo);
            }
        }

        public void ChambreDispo(object param)
        {
            if (SelectedChambres.EtatOperation != "INDISPONIBLE") return;

            var result = MessageBox.Show("Voulez vous vraiment rendre cette chambre Disponible ?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {

                SelectedChambres.EtatOperation = "LIBRE";

                model.SaveChanges();

                Load();


            }

        }

        private bool CanChambreDispo(object param)
        {

            if (SelectedChambres == null) return false;

            return true;

            //return false;
        }

        public ICommand ChambreInDispoCommand
        {
            get
            {
                return new ERP.LocationsModule.ViewModel.Commands.DelegateCommand(ChambreInDispo, CanChambreInDispo);
            }
        }

        public void ChambreInDispo(object param)
        {
            //if (SelectedChambres.EtatOperation != "LIBRE" && SelectedChambres.EtatOperation != "SALLE") return;

            //MaintenanceViews view = new MaintenanceViews(SelectedChambres.ID);
            //view.ShowDialog();

            //Load();

        }

        private bool CanChambreInDispo(object param)
        {

            if (SelectedChambres == null) return false;

            return true;

            //return false;
        }

        #endregion

        #region Internal Properties


        #endregion

        #region Properties

        private Chambres _selectedChambres = null;

        public Chambres SelectedChambres
        {
            get { return _selectedChambres; }
            set
            {
                if (value != this._selectedChambres)
                {
                    this._selectedChambres = value;

                    base.RaisePropertyChangedEvent("SelectedChambres");
                }
            }
        }

        ObservableCollection<Chambres> allChambres;

        public ObservableCollection<Chambres> AllChambres
        {
            get { return allChambres; }
            set
            {
                allChambres = value;
                base.RaisePropertyChangedEvent("AllChambres");
            }
        }

        ObservableCollection<TypeChambres> allTypeChambres;

        public ObservableCollection<TypeChambres> AllTypeChambres
        {
            get { return allTypeChambres; }
            set { allTypeChambres = value; base.RaisePropertyChangedEvent("AllTypeChambres"); }
        }


        #endregion

        #region Private Methods

        private void Load()
        {
            try
            {
                model = new GESHOTELEntities();

                //AllTypeChambres = new AutoRefreshWrapper<TypeChambres>(model.ChambreDisponibilite, RefreshMode.StoreWins);
                AllChambres = new ObservableCollection<Chambres>(model.Chambres.Where(c => c.Etat != "SUPPRIMER"));
                AllChambres.OrderBy(p => p.TypeChambres);

            }
            catch (Exception)
            {

                MessageBox.Show("Il y a eu un problem lors de la connection au serveur. \n Veiullez verifier la connection internet");
            }
            //base.RaisePropertyChangedEvent("AllChantiers");
        }

        public bool VerifyDock(string Header)
        {
            foreach (RadDocumentPane item in GlobalData.PaneGroup.Items)
            {
                string str = item.Header.ToString();
                if (str == Header)
                {
                    GlobalData.PaneGroup.SelectedItem = item;
                    return true;
                }
            }

            return false;
        }

        #endregion

    }
}
