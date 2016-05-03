using ERP.LocationsModule.ViewModel.Commands;
using GESHOTEL.COMMON;
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
using Telerik.Pivot.Core;
using Telerik.Windows.Controls;

namespace GESHOTEL.ReservationsModules.ViewModel
{
    public class OverviewsDashBoard : GESHOTEL.COMMON.BaseClasses.ViewModelBase
    {
        #region Field
        GESHOTELEntities model;

        #endregion

        /* Note that this view GlobalData.Model does not actually use the services provided
         * by the ViewModel Base class. We show it as a base class here because 
         * most view models will use the base class services. */

        #region Constructor

        public OverviewsDashBoard()
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
                //model.Refresh(RefreshMode.StoreWins, model.ReservationDashBoard);

                //model.Refresh(RefreshMode.StoreWins, model.Chambres);
                //model.Refresh(RefreshMode.StoreWins, model.Reservations);
                //model.Refresh(RefreshMode.StoreWins, model.Transactions);

                Load();


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

        #endregion

        #region Internal Properties


        #endregion

        #region Properties

        ObservableCollection<ReservationDashBoard> allPassage;

        public ObservableCollection<ReservationDashBoard> AllPassage
        {
            get { return allPassage; }
            set
            {
                allPassage = value;
                base.RaisePropertyChangedEvent("AllPassage");
            }
        }

        ObservableCollection<ReservationDashBoard> allSejour;

        public ObservableCollection<ReservationDashBoard> AllSejour
        {
            get { return allSejour; }
            set
            {
                allSejour = value;
                base.RaisePropertyChangedEvent("AllSejour");
            }
        }

        double maxChambres;

        public double MaxChambres
        {
            get { return maxChambres; }
            set
            {
                maxChambres = value;
                base.RaisePropertyChangedEvent("MaxChambres");
            }
        }

        double chambreDisponible;

        public double ChambreDisponible
        {
            get { return chambreDisponible; }
            set
            {
                chambreDisponible = value;
                base.RaisePropertyChangedEvent("ChambreDisponible");
            }
        }

        double chambreOccupe;

        public double ChambreOccupe
        {
            get { return chambreOccupe; }
            set
            {
                chambreOccupe = value;
                base.RaisePropertyChangedEvent("ChambreOccupe");
            }
        }

        double chambreDue;

        public double ChambreDue
        {
            get { return chambreDue; }
            set
            {
                chambreDue = value;
                base.RaisePropertyChangedEvent("ChambreDue");
            }
        }

        double chambreInDisponible;

        public double ChambreInDisponible
        {
            get { return chambreInDisponible; }
            set
            {
                chambreInDisponible = value;
                base.RaisePropertyChangedEvent("ChambreInDisponible");
            }
        }

        double chambreSalle;

        public double ChambreSalle
        {
            get { return chambreSalle; }
            set
            {
                chambreSalle = value;
                base.RaisePropertyChangedEvent("ChambreSalle");
            }
        }

        double arrive;

        public double Arrive
        {
            get { return arrive; }
            set
            {
                arrive = value;
                base.RaisePropertyChangedEvent("Arrive");
            }
        }

        double reserver;

        public double Reserver
        {
            get { return reserver; }
            set
            {
                reserver = value;
                base.RaisePropertyChangedEvent("Reserver");
            }
        }

        double checkOut;

        public double CheckOut
        {
            get { return checkOut; }
            set
            {
                checkOut = value;
                base.RaisePropertyChangedEvent("CheckOut");
            }
        }

        double passageMontant;

        public double PassageMontant
        {
            get { return passageMontant; }
            set
            {
                passageMontant = value;
                base.RaisePropertyChangedEvent("PassageMontant");
            }
        }

        double nuitMontant;

        public double NuitMontant
        {
            get { return nuitMontant; }
            set
            {
                nuitMontant = value;
                base.RaisePropertyChangedEvent("NuitMontant");
            }
        }

        double depense;

        public double Depense
        {
            get { return depense; }
            set
            {
                depense = value;
                base.RaisePropertyChangedEvent("Depense");
            }
        }

        double decaissement;

        public double Decaissement
        {
            get { return decaissement; }
            set
            {
                decaissement = value;
                base.RaisePropertyChangedEvent("Decaissement");
            }
        }

        double approvisionnement;

        public double Approvisionnement
        {
            get { return approvisionnement; }
            set
            {
                approvisionnement = value;
                base.RaisePropertyChangedEvent("Approvisionnement");
            }
        }

        double totalMontant;

        public double TotalMontant
        {
            get { return totalMontant; }
            set
            {
                totalMontant = value;
                base.RaisePropertyChangedEvent("TotalMontant");
            }
        }

        double totalDepense;

        public double TotalDepense
        {
            get { return totalDepense; }
            set
            {
                totalDepense = value;
                base.RaisePropertyChangedEvent("TotalDepense");
            }
        }

        double totalVersement;

        public double TotalVersement
        {
            get { return totalVersement; }
            set
            {
                totalVersement = value;
                base.RaisePropertyChangedEvent("TotalVersement");
            }
        }

        #endregion

        #region Private Methods

        private void Load()
        {
            try
            {
                if (model == null)
                {
                    model = new GESHOTELEntities();
                }


                GetReservationStat();
                GetChambreStat();
                LoadDepense();

            }
            catch (Exception)
            {

                MessageBox.Show("Il y a eu un problem lors de la connection au serveur. \n Veiullez verifier la connection internet");
            }
            //base.RaisePropertyChangedEvent("AllChantiers");
        }

        private void LoadDepense()
        {

            try
            {
                Depense = (double)model.ListDepense.Where(c => c.idRegistre == GlobalData.RegId && c.TypeTransaction == "DEPENSE").Sum(c => c.Montant);


            }
            catch (Exception)
            {
                Depense = 0;

            }

            try
            {
                Approvisionnement = (double)model.ListDepense.Where(c => c.idRegistre == GlobalData.RegId && c.TypeTransaction == "APPROVISIONNEMENT").Sum(c => c.Montant);


            }
            catch (Exception)
            {
                Approvisionnement = 0;

            }

            try
            {
                Decaissement = (double)model.ListDepense.Where(c => c.idRegistre == GlobalData.RegId && c.TypeTransaction == "DECAISSEMENT").Sum(c => c.Montant);


            }
            catch (Exception)
            {
                Decaissement = 0;

            }


            try
            {

                TotalDepense = (Depense + Decaissement);


                TotalVersement = (TotalMontant + Approvisionnement) - TotalDepense;
            }
            catch (Exception)
            {
                TotalDepense = 0;
                TotalVersement = (TotalMontant + Approvisionnement) - TotalDepense;

            }


        }

        private void GetChambreStat()
        {
            try
            {
                MaxChambres = model.Chambres.Where(res => res.Etat == "ACTIF").Count();
            }
            catch (Exception)
            {

            }

            try
            {
                ChambreDisponible = model.Chambres.Where(res => res.Etat == "ACTIF" && res.EtatOperation == "LIBRE").Count();
            }
            catch (Exception)
            {

            }

            try
            {
                ChambreOccupe = model.Chambres.Where(res => res.Etat == "ACTIF" && res.EtatOperation == "OCCUPER").Count();
            }
            catch (Exception)
            {

            }

            try
            {
                ChambreDue = model.Chambres.Where(res => res.Etat == "ACTIF" && res.EtatOperation == "DUE OUT").Count();
            }
            catch (Exception)
            {

            }

            try
            {
                ChambreInDisponible = model.Chambres.Where(res => res.Etat == "ACTIF" && res.EtatOperation == "MAINTENANCE").Count();
            }
            catch (Exception)
            {

            }

            try
            {
                ChambreSalle = model.Chambres.Where(res => res.Etat == "ACTIF" && res.EtatOperation == "SALLE").Count();
            }
            catch (Exception)
            {

            }

        }

        private void GetReservationStat()
        {

            string Date = GlobalData.CurrentRegistres.DateDebut.Value.ToString("dd-MM-yyyy");

            try
            {


                var resultat = from res in model.ReservationDashBoard
                               where res.ReservationType == "Passage" && res.DateTransaction == Date
                               select res;

                AllPassage = new ObservableCollection<ReservationDashBoard>(resultat);

                PassageMontant = (double)resultat.Sum(c => c.Montant);


            }
            catch (Exception)
            {
                PassageMontant = 0;

            }

            try
            {
                var resSejour = from res in model.ReservationDashBoard
                                where res.EtatOperation != "RESERVEE" && res.ReservationType != "Passage" && res.DateTransaction == Date
                                select res;

                AllSejour = new ObservableCollection<ReservationDashBoard>(resSejour);

                NuitMontant = (double)resSejour.Sum(c => c.Montant);

            }
            catch (Exception)
            {
                NuitMontant = 0;
            }


            try
            {
                TotalMontant = NuitMontant + PassageMontant;
            }
            catch (Exception)
            {

            }

            try
            {
                CheckOut = model.ReservationCheckOut.Where(res => res.DateCheckOutVC == Date).Count();
            }
            catch (Exception)
            {

            }

            try
            {
                Arrive = model.ReservationCheckIn.Where(res => res.DateCheckInVC == Date).Count();
            }
            catch (Exception)
            {

            }

            try
            {
                Reserver = model.Reservations.Where(res => res.Etat == "RESERVER" && res.EtatOperation == "RESERVEE").Count();
            }
            catch (Exception)
            {

            }


        }

        public bool ClotureSession()
        {
            bool isReservation = false;
            bool isChambreDue = false;
            bool isChambreSalle = false;
            bool isMaintenance = false;

            //Verifier si n'y a pas de reservation a traiter.
            if (Reserver != 0)
            {
                var result = MessageBox.Show("Il existe des Réservations en attente. Voulez-vous les annulées", "Message", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    AnnulerReservation();
                }

            }
            else
            {
                isReservation = true;
            }

            if (ChambreDue != 0)
            {

                var result = MessageBox.Show("Vous avez des chambres à libérer. Voulez-vous les libérer", "Message", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    isChambreDue = CheckOutAll();
                }
            }
            else
            {
                isChambreDue = true;
            }

            //if (chambreSalle != 0)
            //{


            //    var result = MessageBox.Show("Des chambres sont salle. Voulez-vous les rendre propre", "Message", MessageBoxButton.YesNo, MessageBoxImage.Question);

            //    if (result == MessageBoxResult.Yes)
            //    {
            //        RendreAllPropre();

            //        isChambreSalle = true;

            //    }
            //}
            //else
            //{
            //    isChambreSalle = true;
            //}

            if (ChambreInDisponible != 0)
            {


                var result = MessageBox.Show("Vous avez des chambres en Maintenance. Voulez-vous les libérer", "Message", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    LibererAllMaintenance();

                    isMaintenance = true;
                }
                else
                {
                    isMaintenance = true;
                }
            }
            else
            {
                isMaintenance = true;
            }


            if (isChambreDue == true  && isMaintenance == true && isReservation == true)
            {

                Registres Reg = model.Registres.Where(res => res.State == "OUVERT").ToList().LastOrDefault();

                Reg.State = "FERMER";
                Reg.DateFermeture = DateTime.Now;


                Registres reg = new Registres();
                reg.Date = DateTime.Now.Date;
                reg.DateDebut = DateTime.Now;
                reg.State = "OUVERT";
                reg.idUtilisateur = GlobalData.Id;


                model.Registres.Add(reg);
                model.SaveChanges();

                GlobalData.VerifyClotureSession();

                MessageBox.Show("Opération Terminée", "Message", MessageBoxButton.OK, MessageBoxImage.None);

                return true;
            }
            else
            {
                MessageBox.Show("Opération Annulée", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }

        public void AnnulerReservation()
        {

            var resultat = from res in model.Reservations
                           where res.idRegistre == GlobalData.RegId && res.EtatOperation == "RESERVEE"
                           select res;

            foreach (Reservations item in resultat)
            {
                item.Etat = "TERMINER";
                item.CancelDate = DateTime.Now;
                item.EtatOperation = "ANNULER";

                if (item.Chambres != null)
                {
                    item.Chambres.EtatOperation = "LIBRE";
                }
                


            }

            model.SaveChanges();

        }

        public bool CheckOutAll()
        {
            var resultat = from res in model.Reservations
                           where res.idRegistre == GlobalData.RegId && res.Etat == "DUE OUT"
                           select res;
            bool data = true;

            foreach (Reservations item in resultat)
            {
                if (item.Etat == "DUE OUT")
                {
                    if (item.TotalReste == 0)
                    {
                        item.Etat = "TERMINER";
                        item.EtatOperation = "TERMINER";
                        item.Chambres.EtatOperation = "LIBRE";
                        item.isCheckOut = true;
                        item.DateCheckOut = DateTime.Now;

                    }
                    else
                    {
                        data = false;
                        BeginModifierReservation(item);
                        MessageBox.Show("Le Sejour de la chambres " + item.Chambres.Numero + " n'est pa encore soldé", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    }
                }
            }

            model.SaveChanges();
            return data;


        }

        public void BeginModifierReservation(Reservations SelectedReservations)
        {

            if (SelectedReservations != null)
            {
                if (SelectedReservations.ReservationTypes.ReservationType == "PASSAGE")
                {
                    //PassageWin view = new PassageWin(SelectedReservations.ID);
                    //view.ShowDialog();
                }
                else if (SelectedReservations.ReservationTypes.ReservationType == "NUIT")
                {
                    RadDocumentPane rad = new RadDocumentPane();
                    rad.Content = new GESHOTEL.ReservationsModules.TransactionEdit(SelectedReservations.ID, "Modification de Sejour " + SelectedReservations.ID.ToString());
                    rad.Header = "Modification de Sejour " + SelectedReservations.ID.ToString();
                    GlobalData.rrvMain.Title = "Modification de Sejour " + SelectedReservations.ID.ToString();
                    rad.FontFamily = new FontFamily("Perpetua Titling MT");
                    rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                    rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

                    if (!GlobalData.VerifyDock("Modification de Sejour " + SelectedReservations.ID.ToString()))
                    {
                        GlobalData.PaneGroup.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
                    }
                    else
                    {

                    }
                }
                else if (SelectedReservations.ReservationTypes.ReservationType == "SEJOUR")
                {
                    RadDocumentPane rad = new RadDocumentPane();
                    rad.Content = new GESHOTEL.ReservationsModules.TransactionEdit(SelectedReservations.ID, "Modification de Sejour " + SelectedReservations.ID.ToString());
                    rad.Header = "Modification de Sejour " + SelectedReservations.ID.ToString();
                    GlobalData.rrvMain.Title = "Modification de Sejour " + SelectedReservations.ID.ToString();
                    rad.FontFamily = new FontFamily("Perpetua Titling MT");
                    rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                    rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

                    if (!GlobalData.VerifyDock("Modification de Sejour " + SelectedReservations.ID.ToString()))
                    {
                        GlobalData.PaneGroup.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
                    }
                    else
                    {

                    }
                }
            }

        }

        public void RendreAllPropre()
        {
            var resultat = from res in model.Chambres
                           where res.Etat == "ACTIF" && res.EtatOperation == "SALLE"
                           select res;

            foreach (Chambres item in resultat)
            {

                item.EtatOperation = "LIBRE"; ;

            }

            model.SaveChanges();



        }

        public void LibererAllMaintenance()
        {
            var resultat = from res in model.Reservations
                           where res.idRegistre == GlobalData.RegId && res.EtatOperation == "MAINTENANCE" && res.DateDepart < DateTime.Now
                           select res;

            foreach (Reservations item in resultat)
            {
                if (item.Etat == "DUE OUT")
                {

                    item.Etat = "TERMINER";
                    item.EtatOperation = "TERMINER";
                    item.Chambres.EtatOperation = "LIBRE";
                    item.isCheckOut = true;
                    item.DateCheckOut = DateTime.Now;

                }
            }

            model.SaveChanges();



        }

        #endregion
    }
}
