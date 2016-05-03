using ERP.LocationsModule.ViewModel.Commands;
using GESHOTEL.COMMON.BaseClasses;
using GESHOTEL.ReservationsModules;
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
    public class ReservationPanelViewModel : GESHOTEL.COMMON.BaseClasses.ViewModelBase
    {
        #region Field
        string Etat = "ALL";
        #endregion

        #region Constructor

        public ReservationPanelViewModel()
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
                if (Etat == "ALL")
                {

                    GlobalData.modelRP = new GESHOTELEntities();

                    AllReservations = new ObservableCollection<Reservations>(GlobalData.modelRP.Reservations.Where(c => c.Etat != "TERMINER" && c.isCheckIn == true && c.Etat != "ANNULER"));
                }


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

            RadDocumentPane rad = new RadDocumentPane();
            rad.Content = new GESHOTEL.ReservationsModules.PassageUC();
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

            RadDocumentPane rad = new RadDocumentPane();
            rad.Content = new GESHOTEL.ReservationsModules.StayInsert();
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

        private bool CanNuitSejour(object param)
        {

            return true;

            //return false;
        }

        public ICommand CheckOutCommand
        {
            get
            {
                return new ERP.LocationsModule.ViewModel.Commands.DelegateCommand(BeginCheckOut, CanCheckOut);
            }
        }

        public void BeginCheckOut(object param)
        {
            try
            {
                if (SelectedReservations.Etat != "DUE OUT")
                {
                    var result = MessageBox.Show("Le séjour/passage n’est pas encore terminé. Voulez-vous libérer cette chambre ?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {

                        if (SelectedReservations.TotalReste == 0)
                        {
                            SelectedReservations.Etat = "TERMINER";
                            SelectedReservations.EtatOperation = "TERMINER";

                            if (SelectedReservations.Chambres != null)
                            {
                                SelectedReservations.Chambres.EtatOperation = "SALLE";
                            }

                            SelectedReservations.DateCheckOut = DateTime.Now;
                            SelectedReservations.isCheckOut = true;

                            GlobalData.modelRP.SaveChanges();

                            Load();

                            MessageBox.Show("Operation terminée", "Message", MessageBoxButton.OK, MessageBoxImage.None);

                        }
                        else
                        {
                            BeginModifierReservation(null);
                            MessageBox.Show("Le Sejour de la chambre " + SelectedReservations.Chambres.Numero + " n’a pas encore été soldé ", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                        }
                    }
                }
                else
                {

                    if (SelectedReservations.TotalReste == 0)
                    {
                        SelectedReservations.Etat = "TERMINER";
                        SelectedReservations.EtatOperation = "TERMINER";

                        if (SelectedReservations.Chambres != null)
                        {
                            SelectedReservations.Chambres.EtatOperation = "SALLE";
                        }


                        SelectedReservations.DateCheckOut = DateTime.Now;
                        SelectedReservations.isCheckOut = true;

                        GlobalData.modelRP.SaveChanges();

                        Load();

                        MessageBox.Show("Operation terminée", "Message", MessageBoxButton.OK, MessageBoxImage.None);

                    }
                    else
                    {
                        BeginModifierReservation(null);
                        MessageBox.Show("Le Sejour n'est pa encore soldé", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception)
            {
                
            }         


        }

        private bool CanCheckOut(object param)
        {
            //if (SelectedReservations == null) return false;

            //return SelectedReservations.Etat != "TERMINER";

            return true;
        }

        public ICommand CheckInCommand
        {
            get
            {
                return new ERP.LocationsModule.ViewModel.Commands.DelegateCommand(BeginCheckIn, CanCheckIn);
            }
        }

        public void BeginCheckIn(object param)
        {
            try
            {
                if (SelectedReservations.EtatOperation == "RESERVEE")
                {
                    if (SelectedReservations.Chambres == null)
                    {
                        BeginModifierReservation(null);
                    }
                    else
                    {

                        var result = MessageBox.Show("Voulez vous enregistrer cette arrivée?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            if (SelectedReservations.TotalReste == 0)
                            {
                                if (isAvailable(SelectedReservations.DateArrive.Value, SelectedReservations.DateDepart.Value, SelectedReservations.Chambres))
                                {
                                    if (SelectedReservations.Chambres.EtatOperation != "LIBRE" || SelectedReservations.Chambres.EtatOperation != "RESERVER")
                                    {
                                        SelectedReservations.Etat = "ACTIF";
                                        SelectedReservations.EtatOperation = "ARRIVEE";
                                        SelectedReservations.Chambres.EtatOperation = "OCCUPER";

                                        SelectedReservations.DateCheckIn = DateTime.Now;
                                        SelectedReservations.isCheckIn = true;

                                        GlobalData.modelRP.SaveChanges();

                                        Load();

                                        MessageBox.Show("Operation terminée", "Message", MessageBoxButton.OK, MessageBoxImage.None);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Cette chambre n'est pas disponible", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("Cette chambre n'est pas disponible", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }

                            }
                            else
                            {
                                BeginModifierReservation(null);
                                //MessageBox.Show("Le Sejour de la chambre " + SelectedReservations.Chambres.Numero + " n’a pas encore été soldé ", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                            }

                        }
                    }
                }
            }
            catch (Exception)
            {

            }

        }

        private bool CanCheckIn(object param)
        {
            //if (SelectedReservations == null) return false;
            //if (SelectedReservations.EtatOperation != "RESERVEE") return false;

            //return SelectedReservations.Etat != "TERMINER";

            return true;
        }

        public ICommand CheckOutAllCommand
        {
            get
            {
                return new ERP.LocationsModule.ViewModel.Commands.DelegateCommand(BeginCheckOutAll, CanCheckOutAll);
            }
        }

        public void BeginCheckOutAll(object param)
        {
            foreach (Reservations item in AllReservations)
            {
                if (item.Etat == "DUE OUT")
                {
                    if (item.TotalReste == 0)
                    {
                        item.Etat = "TERMINER";
                        item.EtatOperation = "TERMINER";

                        if (item.Chambres != null)
                        {
                            item.Chambres.EtatOperation = "SALLE";
                        }

                        item.DateCheckOut = DateTime.Now;
                        SelectedReservations.isCheckOut = true;

                    }
                    else
                    {
                        BeginModifierReservation(null);
                        MessageBox.Show("Le Sejour n’a pas encore été soldé ", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    }
                }
            }

            GlobalData.modelRP.SaveChanges();

            Load();

        }

        private bool CanCheckOutAll(object param)
        {
            return true;
        }

        public ICommand AnnulerCommand
        {
            get
            {
                return new ERP.LocationsModule.ViewModel.Commands.DelegateCommand(BeginAnnuler, CanAnnuler);
            }
        }

        public void BeginAnnuler(object param)
        {
            try
            {
                if (SelectedReservations.EtatOperation == "RESERVEE")
                {
                    var result = MessageBox.Show("Voulez vous vraiment annuler cette reservation ?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        var canresult = from res in GlobalData.modelRP.Cancelations
                                        where res.Etat == "ACTIF"
                                        select res;

                        if (canresult != null && canresult.Count() != 0)
                        {
                            Cancelations can = canresult.FirstOrDefault();

                            switch (can.idCancelationRules)
                            {
                                case 1:

                                    GetCancelFeeTotal(can);

                                    break;

                                case 2:
                                    GetCancelFeeFirstLast(can);
                                    break;

                                case 3:
                                    GetCancelFeeFirstLast(can);
                                    break;

                                default:
                                    break;
                            }

                        }

                        Load();

                    }
                }
            }
            catch (Exception)
            {
                
            } 

        }

        private void GetCancelFeeTotal(Cancelations can)
        {
            Transactions Trans = SelectedReservations.Transactions.FirstOrDefault();
            List<DetailTransactions> lstDtChambres = Trans.DetailTransactions.Where(c => c.Etat == "ACTIF" && c.Produits.Categories.Libelle == "CHAMBRES").ToList();
            List<DetailPaiements> lstDtPaie = Trans.DetailPaiements.Where(c => c.Etat == "ACTIF").ToList();

            //Annuler les details transactions concernant la chambre
            foreach (DetailTransactions item in lstDtChambres)
            {
                item.Etat = "ANNULER";
            }

            foreach (DetailPaiements item in lstDtPaie)
            {
                item.Etat = "ANNULER";
            }

            double NbreHr = SelectedReservations.DateArrive.Value.Subtract(DateTime.Now).TotalDays;

            if (NbreHr < can.TimeFrame)
            {
                DetailTransactions trans = new DetailTransactions();
                trans.Produits = GlobalData.modelRP.Produits.Where(c => c.Libelle == "FRAIS D'ANNULATION DE LA RESERVATION").First(); ;
                trans.Date = DateTime.Now;
                trans.Quantite = (float)1;

                if (can.Type == 0)
                {
                    trans.prix = Convert.ToDecimal((double)Trans.TotalTTC * can.PourcentageAfter * 0.01) + can.HandlingFee;
                }
                else
                {
                    trans.prix = Convert.ToDecimal(can.MontantAfter) + can.HandlingFee;
                }

                trans.Descriptions = "FRAIS D'ANNULATION DE LA RESERVATION " + SelectedReservations.ID;
                trans.Montant = Convert.ToDecimal(trans.Quantite * (double)trans.prix);
                trans.Etat = "ACTIF";
                //trans.idHotel = GlobalData.HotId;

                Trans.DetailTransactions.Add(trans);

                Trans.TotalTTC = Convert.ToDecimal(trans.Quantite * (double)trans.prix) + can.HandlingFee;
                Trans.TotalHT = Trans.TotalTTC;
                Trans.TVA = 0;

                if ((Trans.TotalPaye - Trans.TotalTTC) < 0)
                {
                    Trans.TotalReste = 0;

                    DetailPaiements dtPaie = new DetailPaiements();
                    dtPaie.Montant = (double)Trans.TotalPaye;
                    dtPaie.DatePaiement = DateTime.Now;
                    dtPaie.MethodePaiements = GlobalData.modelRP.MethodePaiements.Where(c => c.Libelle == "ESPECE").FirstOrDefault();
                    dtPaie.Transactions = Trans;
                    dtPaie.Etat = "ACTIF";
                    //dtPaie.idHotel = GlobalData.HotId;
                }
                else
                {
                    Trans.TotalReste = 0;
                    Trans.TotalPaye = Trans.TotalTTC;

                    DetailPaiements dtPaie = new DetailPaiements();
                    dtPaie.Montant = (double)Trans.TotalPaye;
                    dtPaie.DatePaiement = DateTime.Now;
                    dtPaie.MethodePaiements = GlobalData.modelRP.MethodePaiements.Where(c => c.Libelle == "ESPECE").FirstOrDefault();
                    dtPaie.Transactions = Trans;
                    dtPaie.Etat = "ACTIF";
                    //dtPaie.idHotel = GlobalData.HotId;

                }



                SelectedReservations.TotalPaye = Trans.TotalPaye;
                SelectedReservations.TotalReste = Trans.TotalReste;
                SelectedReservations.TotalTTC = Trans.TotalTTC;
                SelectedReservations.Reduction = Trans.Reduction;

                Trans.Etat = "PAYE";
                SelectedReservations.CancelDate = DateTime.Now;
                SelectedReservations.Etat = "TERMINER";
                SelectedReservations.EtatOperation = "ANNULER";

                if (SelectedReservations.Chambres != null)
                {
                    SelectedReservations.Chambres.EtatOperation = "LIBRE";
                }

                GlobalData.modelRP.SaveChanges();

                Load();

                MessageBox.Show("Operation terminée", "Message", MessageBoxButton.OK, MessageBoxImage.None);

            }
            else if (NbreHr > can.TimeFrame)
            {
                DetailTransactions trans = new DetailTransactions();
                trans.Produits = GlobalData.modelRP.Produits.Where(c => c.Libelle == "FRAIS D'ANNULATION DE LA RESERVATION").First(); ;
                trans.Date = DateTime.Now;
                trans.Quantite = (float)1;

                if (can.Type == 0)
                {
                    trans.prix = Convert.ToDecimal((double)Trans.TotalTTC * can.PourcentageBefore * 0.01) + can.HandlingFee;
                }
                else
                {
                    trans.prix = Convert.ToDecimal(can.MonantBefore) + can.HandlingFee;
                }

                trans.Descriptions = "FRAIS D'ANNULATION DE LA RESERVATION " + SelectedReservations.ID;
                trans.Montant = Convert.ToDecimal(trans.Quantite * (double)trans.prix);
                trans.Etat = "ACTIF";
                //trans.idHotel = GlobalData.HotId;

                Trans.DetailTransactions.Add(trans);

                Trans.TotalTTC = Convert.ToDecimal(trans.Quantite * (double)trans.prix) + can.HandlingFee;
                Trans.TotalHT = Trans.TotalTTC;
                Trans.TVA = 0;

                if ((Trans.TotalPaye - Trans.TotalTTC) < 0)
                {
                    Trans.TotalReste = 0;

                    DetailPaiements dtPaie = new DetailPaiements();
                    dtPaie.Montant = (double)Trans.TotalPaye;
                    dtPaie.DatePaiement = DateTime.Now;
                    dtPaie.MethodePaiements = GlobalData.modelRP.MethodePaiements.Where(c => c.Libelle == "ESPECE").FirstOrDefault();
                    dtPaie.Transactions = Trans;
                    dtPaie.Etat = "ACTIF";
                    //dtPaie.idHotel = GlobalData.HotId;
                }
                else
                {
                    Trans.TotalReste = 0;
                    Trans.TotalPaye = Trans.TotalTTC;

                    DetailPaiements dtPaie = new DetailPaiements();
                    dtPaie.Montant = (double)Trans.TotalPaye;
                    dtPaie.DatePaiement = DateTime.Now;
                    dtPaie.MethodePaiements = GlobalData.modelRP.MethodePaiements.Where(c => c.Libelle == "ESPECE").FirstOrDefault();
                    dtPaie.Transactions = Trans;
                    dtPaie.Etat = "ACTIF";
                    //dtPaie.idHotel = GlobalData.HotId;

                }

                SelectedReservations.TotalPaye = Trans.TotalPaye;
                SelectedReservations.TotalReste = Trans.TotalReste;
                SelectedReservations.TotalTTC = Trans.TotalTTC;
                SelectedReservations.Reduction = Trans.Reduction;

                Trans.Etat = "PAYE";
                SelectedReservations.CancelDate = DateTime.Now;
                SelectedReservations.Etat = "TERMINER";
                SelectedReservations.EtatOperation = "ANNULER";
                SelectedReservations.Chambres.EtatOperation = "LIBRE";

                GlobalData.modelRP.SaveChanges();

                Load();

                MessageBox.Show("Operation terminée", "Message", MessageBoxButton.OK, MessageBoxImage.None);
            }
            else
            {

            }

        }

        private void GetCancelFeeFirstLast(Cancelations can)
        {
            Transactions Trans = SelectedReservations.Transactions.FirstOrDefault();
            List<DetailTransactions> lstDtChambres = Trans.DetailTransactions.Where(c => c.Etat == "ACTIF" && c.Produits.Categories.Libelle == "CHAMBRES").ToList();
            List<DetailPaiements> lstDtPaie = Trans.DetailPaiements.Where(c => c.Etat == "ACTIF").ToList();

            //Annuler les details transactions concernant la chambre
            foreach (DetailTransactions item in lstDtChambres)
            {
                item.Etat = "ANNULER";
            }

            foreach (DetailPaiements item in lstDtPaie)
            {
                item.Etat = "ANNULER";
            }

            double NbreHr = SelectedReservations.DateArrive.Value.Subtract(DateTime.Now).TotalDays;

            if (NbreHr < can.TimeFrame)
            {
                DetailTransactions trans = new DetailTransactions();
                trans.Produits = GlobalData.modelRP.Produits.Where(c => c.Libelle == "FRAIS D'ANNULATION DE LA RESERVATION").FirstOrDefault(); ;
                trans.Date = DateTime.Now;
                trans.Quantite = (float)1;

                if (can.Type == 0)
                {
                    DetailTransactions dt = lstDtChambres.FirstOrDefault();

                    trans.prix = Convert.ToDecimal((double)dt.prix * can.PourcentageAfter * 0.01) + can.HandlingFee;
                }
                else
                {
                    trans.prix = Convert.ToDecimal(can.MontantAfter) + can.HandlingFee;
                }

                trans.Descriptions = "FRAIS D'ANNULATION DE LA RESERVATION " + SelectedReservations.ID;
                trans.Montant = Convert.ToDecimal(trans.Quantite * (double)trans.prix);
                trans.Etat = "ACTIF";
                //trans.idHotel = GlobalData.HotId;

                Trans.DetailTransactions.Add(trans);

                Trans.TotalTTC = Convert.ToDecimal(trans.Quantite * (double)trans.prix) + can.HandlingFee;
                Trans.TotalHT = Trans.TotalTTC;
                Trans.TVA = 0;

                if ((Trans.TotalPaye - Trans.TotalTTC) < 0)
                {
                    Trans.TotalReste = 0;

                    DetailPaiements dtPaie = new DetailPaiements();
                    dtPaie.Montant = (double)Trans.TotalPaye;
                    dtPaie.DatePaiement = DateTime.Now;
                    dtPaie.MethodePaiements = GlobalData.modelRP.MethodePaiements.Where(c => c.Libelle == "ESPECE").FirstOrDefault();
                    dtPaie.Transactions = Trans;
                    dtPaie.Etat = "ACTIF";
                    //dtPaie.idHotel = GlobalData.HotId;
                }
                else
                {
                    Trans.TotalReste = 0;
                    Trans.TotalPaye = Trans.TotalTTC;

                    DetailPaiements dtPaie = new DetailPaiements();
                    dtPaie.Montant = (double)Trans.TotalPaye;
                    dtPaie.DatePaiement = DateTime.Now;
                    dtPaie.MethodePaiements = GlobalData.modelRP.MethodePaiements.Where(c => c.Libelle == "ESPECE").FirstOrDefault();
                    dtPaie.Transactions = Trans;
                    dtPaie.Etat = "ACTIF";
                    //dtPaie.idHotel = GlobalData.HotId;

                }

                SelectedReservations.TotalPaye = Trans.TotalPaye;
                SelectedReservations.TotalReste = Trans.TotalReste;
                SelectedReservations.TotalTTC = Trans.TotalTTC;
                SelectedReservations.Reduction = Trans.Reduction;

                Trans.Etat = "PAYE";
                SelectedReservations.CancelDate = DateTime.Now;
                SelectedReservations.Etat = "TERMINER";
                SelectedReservations.EtatOperation = "ANNULER";

                if (SelectedReservations.Chambres != null)
                {
                    SelectedReservations.Chambres.EtatOperation = "LIBRE";
                }

                GlobalData.modelRP.SaveChanges();

                Load();

                MessageBox.Show("Operation terminée", "Message", MessageBoxButton.OK, MessageBoxImage.None);

            }
            else if (NbreHr > can.TimeFrame)
            {
                DetailTransactions trans = new DetailTransactions();
                trans.Produits = GlobalData.modelRP.Produits.Where(c => c.Libelle == "FRAIS D'ANNULATION DE LA RESERVATION").First(); ;
                trans.Date = DateTime.Now;
                trans.Quantite = (float)1;

                if (can.Type == 0)
                {
                    DetailTransactions dt = lstDtChambres.FirstOrDefault();

                    trans.prix = Convert.ToDecimal((double)dt.prix * can.PourcentageBefore * 0.01) + can.HandlingFee;
                }
                else
                {
                    trans.prix = Convert.ToDecimal(can.MonantBefore) + can.HandlingFee;
                }

                trans.Descriptions = "FRAIS D'ANNULATION DE LA RESERVATION " + SelectedReservations.ID;
                trans.Montant = Convert.ToDecimal(trans.Quantite * (double)trans.prix);
                trans.Etat = "ACTIF";
                //trans.idHotel = GlobalData.HotId;

                Trans.DetailTransactions.Add(trans);

                Trans.TotalTTC = Convert.ToDecimal(trans.Quantite * (double)trans.prix) + can.HandlingFee;
                Trans.TotalHT = Trans.TotalTTC;
                Trans.TVA = 0;

                if ((Trans.TotalPaye - Trans.TotalTTC) < 0)
                {
                    Trans.TotalReste = 0;

                    DetailPaiements dtPaie = new DetailPaiements();
                    dtPaie.Montant = (double)Trans.TotalPaye;
                    dtPaie.DatePaiement = DateTime.Now;
                    dtPaie.MethodePaiements = GlobalData.modelRP.MethodePaiements.Where(c => c.Libelle == "ESPECE").FirstOrDefault();
                    dtPaie.Transactions = Trans;
                    dtPaie.Etat = "ACTIF";
                    //dtPaie.idHotel = GlobalData.HotId;
                }
                else
                {
                    Trans.TotalReste = 0;
                    Trans.TotalPaye = Trans.TotalTTC;

                    DetailPaiements dtPaie = new DetailPaiements();
                    dtPaie.Montant = (double)Trans.TotalPaye;
                    dtPaie.DatePaiement = DateTime.Now;
                    dtPaie.MethodePaiements = GlobalData.modelRP.MethodePaiements.Where(c => c.Libelle == "ESPECE").FirstOrDefault();
                    dtPaie.Transactions = Trans;
                    dtPaie.Etat = "ACTIF";
                    //dtPaie.idHotel = GlobalData.HotId;

                }

                SelectedReservations.TotalPaye = Trans.TotalPaye;
                SelectedReservations.TotalReste = Trans.TotalReste;
                SelectedReservations.TotalTTC = Trans.TotalTTC;
                SelectedReservations.Reduction = Trans.Reduction;

                Trans.Etat = "PAYE";
                SelectedReservations.CancelDate = DateTime.Now;
                SelectedReservations.Etat = "TERMINER";
                SelectedReservations.EtatOperation = "ANNULER";
                SelectedReservations.Chambres.EtatOperation = "LIBRE";


                GlobalData.modelRP.SaveChanges();

                Load();

                MessageBox.Show("Operation terminée", "Message", MessageBoxButton.OK, MessageBoxImage.None);
            }
            else
            {

            }

        }

        private bool CanAnnuler(object param)
        {

            //if (SelectedReservations == null) return false;
            ////if (SelectedReservations.ReservationTypes.ReservationType != "RESERVATION") return false;
            //if (SelectedReservations.EtatOperation != "RESERVEE") return false;

            //return SelectedReservations.Etat == "RESERVER";

            return true;
        }

        public ICommand ModifierReservationCommand
        {
            get
            {
                return new ERP.LocationsModule.ViewModel.Commands.DelegateCommand(BeginModifierReservation, CanModifierReservation);
            }
        }

        public void BeginModifierReservation(object param)
        {

            try
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

                        if (!VerifyDock("Modification de Sejour " + SelectedReservations.ID.ToString()))
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

                        if (!VerifyDock("Modification de Sejour " + SelectedReservations.ID.ToString()))
                        {
                            GlobalData.PaneGroup.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
                        }
                        else
                        {

                        }
                    }
                }
            }
            catch (Exception)
            {

            }

        }

        private bool CanModifierReservation(object param)
        {

            return SelectedReservations != null;

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

            RadDocumentPane rad = new RadDocumentPane();
            rad.Content = new GESHOTEL.ReservationsModules.ReservationInsert();
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

        private bool CanNewReservation(object param)
        {

            return true;

            //return false;
        }

        public ICommand ProlongerSejourCommand
        {
            get
            {
                return new ERP.LocationsModule.ViewModel.Commands.DelegateCommand(BeginProlongerSejour, CanProlongerSejour);
            }
        }

        public void BeginProlongerSejour(object param)
        {
            try
            {
                //if (SelectedReservations.ReservationTypes.ReservationType == "PASSAGE")
                //{
                //    ProlongerSejour view = new ProlongerSejour(SelectedReservations.ID);
                //    view.ShowDialog();

                //    Load();
                //}
                //else if (SelectedReservations.ReservationTypes.ReservationType == "NUIT")
                //{
                //    var result = MessageBox.Show("Voulez vous changer cette nuite en sejour?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Question);

                //    if (result == MessageBoxResult.Yes)
                //    {

                //        ProlongerSejourNuit view = new ProlongerSejourNuit(SelectedReservations.ID);
                //        view.ShowDialog();

                //        Load();

                //    }

                //}
                //else if (SelectedReservations.ReservationTypes.ReservationType == "SEJOUR")
                //{
                //    ProlongerSejourNuit view = new ProlongerSejourNuit(SelectedReservations.ID);
                //    view.ShowDialog();

                //    Load();
                //}
            }
            catch (Exception)
            {
                
            }
            
        }

        private bool CanProlongerSejour(object param)
        {

            if (SelectedReservations == null) return false;
            return true;

            //return false;
        }

        public ICommand ChangerSejourCommand
        {
            get
            {
                return new ERP.LocationsModule.ViewModel.Commands.DelegateCommand(BeginChangerSejour, CanChangerSejour);
            }
        }

        public void BeginChangerSejour(object param)
        {
            try
            {
                if (SelectedReservations.ReservationTypes.ReservationType == "PASSAGE")
                {
                    //PassageWin view = new PassageWin(SelectedReservations.ID);
                    //view.ShowDialog();

                    Load();
                }
                else if (SelectedReservations.ReservationTypes.ReservationType == "NUIT")
                {
                    var result = MessageBox.Show("Voulez vous changer cette chambre?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {

                        AssigneChambre view = new AssigneChambre(SelectedReservations, GlobalData.modelRP, "Autre");
                        view.ShowDialog();

                        Load();

                    }

                }
                else if (SelectedReservations.ReservationTypes.ReservationType == "SEJOUR")
                {
                    var result = MessageBox.Show("Voulez vous changer cette chambre?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {

                        AssigneChambre view = new AssigneChambre(SelectedReservations, GlobalData.modelRP, "Autre");
                        view.ShowDialog();

                        Load();

                    }
                }
            }
            catch (Exception)
            {

            }
            
        }

        private bool CanChangerSejour(object param)
        {

            if (SelectedReservations == null) return false;
            return true;

            //return false;
        }

        #endregion

        #region Internal Properties


        #endregion

        #region Properties

        private Reservations _selectedReservations = null;

        public Reservations SelectedReservations
        {
            get { return _selectedReservations; }
            set
            {
                if (value != this._selectedReservations)
                {
                    this._selectedReservations = value;

                    base.RaisePropertyChangedEvent("SelectedReservations");
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
                base.RaisePropertyChangedEvent("AllReservations");
                base.RaisePropertyChangedEvent("AllChambres");
            }
        }

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

        ObservableCollection<TypeChambres> allTypeChambres;

        public ObservableCollection<TypeChambres> AllTypeChambres
        {
            get { return allTypeChambres; }
            set { allTypeChambres = value; base.RaisePropertyChangedEvent("AllTypeChambres"); }
        }

        ObservableCollection<Clients> allClients;

        public ObservableCollection<Clients> AllClients
        {
            get { return allClients; }
            set { allClients = value; base.RaisePropertyChangedEvent("AllClients"); }
        }

        ObservableCollection<Reservations> allReservations;

        public ObservableCollection<Reservations> AllReservations
        {
            get { return allReservations; }
            set
            {
                allReservations = value;
                base.RaisePropertyChangedEvent("AllReservations");
                base.RaisePropertyChangedEvent("AllChambres");
            }
        }


        #endregion

        #region Private Methods

        public void Load()
        {
            try
            {

                Etat = "ALL";
                GlobalData.modelRP = new GESHOTELEntities();

                AllReservations = new ObservableCollection<Reservations>(GlobalData.modelRP.Reservations.Where(c => c.Etat != "TERMINER" && c.isCheckIn == true && c.Etat != "ANNULER"));
                //AllClients = new AutoRefreshWrapper<Clients>(GlobalData.modelRP.Clients.Where(c => c.Etat == "ACTIF") as ObjectQuery<Clients>, RefreshMode.StoreWins);
                //AllTypeChambres = new AutoRefreshWrapper<TypeChambres>(GlobalData.modelRP.TypeChambres.Where(c => c.Etat == "ACTIF") as ObjectQuery<TypeChambres>, RefreshMode.StoreWins);
                //AllChambres = new AutoRefreshWrapper<Chambres>(GlobalData.modelRP.Chambres.Where(c => c.Etat == "ACTIF") as ObjectQuery<Chambres>, RefreshMode.StoreWins);
                //AllChambres.OrderBy(p => p.TypeChambre);

            }
            catch (Exception)
            {

                MessageBox.Show("Il y a eu un problem lors de la connection au serveur. \n Veiullez verifier la connection internet");
            }
            //base.RaisePropertyChangedEvent("AllChantiers");
        }

        public void Recherche(string Chambre)
        {
            try
            {
                Etat = "Chambre";
                //GlobalData.modelRP.Refresh(RefreshMode.StoreWins, GlobalData.modelRP.Reservations);

                AllReservations = new ObservableCollection<Reservations>(GlobalData.modelRP.Reservations.Where(c => c.Etat != "TERMINER" && c.Etat != "ANNULER" && c.Chambres.Numero == Chambre));

            }
            catch (Exception)
            {

                MessageBox.Show("Il y a eu un problem lors de la connection au serveur. \n Veiullez verifier la connection internet");
            }
            //base.RaisePropertyChangedEvent("AllChantiers");
        }

        public void Recherche(Clients cli)
        {
            try
            {
                Etat = "Client";
                //GlobalData.modelRP.Refresh(RefreshMode.StoreWins, GlobalData.modelRP.Reservations);

                AllReservations = new ObservableCollection<Reservations>(GlobalData.modelRP.Reservations.Where(c => c.Etat != "TERMINER" && c.Etat != "ANNULER" && c.idClient == cli.ID));

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

        public bool isAvailable(DateTime datedeb, DateTime dateF, Chambres chambre)
        {

            DateTime date = datedeb;
            DateTime datefin = dateF;

            if (chambre != null)
            {
                var resreserv = from res in GlobalData.modelRP.Reservations
                                where res.ID != SelectedReservations.ID && res.idChambre == chambre.ID && res.Etat != "TERMINER" && (date >= res.DateArrive || datefin >= res.DateArrive) && (date <= res.DateDepart || datefin <= res.DateDepart)
                                select res;

                if (resreserv != null && resreserv.Count() != 0)
                {
                    return false;
                }

            }

            return true;
        }

        #endregion

    }
}
