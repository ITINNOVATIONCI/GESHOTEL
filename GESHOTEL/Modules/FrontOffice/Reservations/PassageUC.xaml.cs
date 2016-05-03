using GESHOTEL.ReservationsModules;
using GESHOTEL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using GESHOTEL.Rapports.Viewers;

namespace GESHOTEL.ReservationsModules
{
    /// <summary>
    /// Interaction logic for PassageUC.xaml
    /// </summary>
    public partial class PassageUC : UserControl
    {
        List<DetailTransactions> ListChambreCharge = new List<DetailTransactions>();
        string Chambre;

        public PassageUC()
        {
            InitializeComponent();
        }

        public PassageUC(string num)
        {
            InitializeComponent();

            Chambre = num;
        }

        private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (!GlobalData.VerifyClotureSession())
                //{

                //    var result = MessageBox.Show("Voulez-vous fermer la session précédente ?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                //    if (result == MessageBoxResult.Yes)
                //    {
                //        RadDocumentPane radMenu = new RadDocumentPane();
                //        radMenu.Content = new GESHOTEL.ReservationsModules.Views.Win.ClotureJourneeFrm("Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString());
                //        radMenu.Header = "Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString();
                //        GlobalData.rrvMain.Title = "Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString();
                //        radMenu.FontFamily = new FontFamily("Perpetua Titling MT");
                //        radMenu.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                //        radMenu.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

                //        if (!GlobalData.VerifyDock("Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString()))
                //        {
                //            GlobalData.PaneGroup.AddItem(radMenu, Telerik.Windows.Controls.Docking.DockPosition.Center);
                //        }
                //        else
                //        {

                //        }
                //    }

                //    return;
                //}

                Chambres Cham = chambreAutoCompleteBox.SelectedItem as Chambres;

                if (Cham.EtatOperation == "SALLE")
                {
                    MessageBox.Show("La chambre est salle veuillez la nettoyée", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var results = MessageBox.Show("Voulez-vous enregistrer ?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (results == MessageBoxResult.Yes)
                {
                    int NbreHr = -rtHD.SelectedTime.Value.Subtract(rtHF.SelectedTime.Value).Hours;

                    if (rnHeure.Value <= 0) { MessageBox.Show("Le nombre d'heure ne peut pa etre egale a 0", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning); return; }
                    if (chambreAutoCompleteBox.SelectedItem == null) { MessageBox.Show("Choisissez une chambre svp", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning); return; }
                    if (NbreHr <= 0) { MessageBox.Show("Le nombre d'heure ne peut pa etre egale a 0", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning); return; }

                    Transactions Trans = new Transactions();
                    Trans.DateTransaction = DateTime.Now;

                    Trans.TotalPaye = Convert.ToDecimal(rntarif.Value);
                    Trans.TotalTTC = Convert.ToDecimal(rntarif.Value);
                    Trans.TotalHT = Convert.ToDecimal(rntarif.Value);
                    Trans.TVA = 0;
                    Trans.TotalReste = 0;

                    Trans.Etat = "PAYE";

                    DetailPaiements dtPaie = new DetailPaiements();
                    dtPaie.Montant = rntarif.Value;
                    dtPaie.DatePaiement = DateTime.Now;
                    dtPaie.MethodePaiements = GlobalData.model.MethodePaiements.Where(c => c.Libelle == "ESPECE").First();
                    dtPaie.Transactions = Trans;
                    dtPaie.Etat = "ACTIF";
                    //dtPaie.idHotel = GlobalData.HotId;

                    Trans.DetailPaiements.Add(dtPaie);

                    if (Cham.EtatOperation == "LIBRE" || Cham.EtatOperation == "RESERVER")
                    {
                        Cham.EtatOperation = "OCCUPER";
                    }

                    DetailTransactions dtTrans = new DetailTransactions();
                    dtTrans.Date = DateTime.Now;
                    dtTrans.Descriptions = "CHAMBRE " + Cham.TypeChambres.Libelle + " " + Cham.Numero;
                    dtTrans.prix = Convert.ToDecimal(rntarif.Value);
                    dtTrans.Quantite = 1;
                    dtTrans.Montant = Convert.ToDecimal(rntarif.Value);
                    dtTrans.Transactions = Trans;
                    dtTrans.Etat = "ACTIF";
                    //dtTrans.idHotel = GlobalData.HotId;
                    dtTrans.Produits = GlobalData.model.Produits.Where(c => c.Libelle == "CHAMBRE").First();

                    Trans.DetailTransactions.Add(dtTrans);

                    ReservationTypes resType = GlobalData.model.ReservationTypes.Where(c => c.ReservationType == "PASSAGE").First();

                    Reservations Reservations = new Reservations();
                    Reservations.ReservationDate = DateTime.Now;
                    Reservations.Chambres = Cham;
                    Reservations.Transactions.Add(Trans);
                    Reservations.ReservationTypes = resType;
                    Reservations.NbreNuit = NbreHr;
                    Reservations.Etat = "ACTIF";
                    Reservations.EtatOperation = "ARRIVEE";
                    Reservations.TotalPaye = Trans.TotalPaye;
                    Reservations.TotalReste = Trans.TotalReste;
                    Reservations.TotalTTC = Trans.TotalTTC;
                    Reservations.DateDepart = new DateTime(rtHF.SelectedDate.Value.Year, rtHF.SelectedDate.Value.Month, rtHF.SelectedDate.Value.Day, rtHF.SelectedTime.Value.Hours, rtHF.SelectedTime.Value.Minutes, rtHF.SelectedTime.Value.Seconds);
                    Reservations.DateArrive = new DateTime(rtHD.SelectedDate.Value.Year, rtHD.SelectedDate.Value.Month, rtHD.SelectedDate.Value.Day, rtHD.SelectedTime.Value.Hours, rtHD.SelectedTime.Value.Minutes, rtHD.SelectedTime.Value.Seconds);
                    Reservations.TypeOperation = "LOCATION CHAMBRE";
                    Reservations.DateCheckIn = DateTime.Now;
                    Reservations.isCheckIn = true;
                    //Reservations.idHotel = GlobalData.HotId;
                    Reservations.idRegistre = GlobalData.RegId;

                    //Trans.idHotel = GlobalData.HotId;
                    Trans.idRegistre = GlobalData.RegId;
                    Trans.TypeTransaction = resType.ReservationType;

                    Parametres param = GlobalData.model.Parametres.Where(c => c.IdParametre == 1).First();

                    Trans.TransactionNumero = GlobalData.GenerateFacture(param.TransNum.ToString(), 6);

                    param.TransNum++;


                    GlobalData.model.Transactions.Add(Trans);
                    GlobalData.model.Reservations.Add(Reservations);
                    GlobalData.model.SaveChanges();

                    var result = MessageBox.Show("Voulez vous imprimer le recu?", "Vente", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {

                        try
                        {
                            //RapportWindows frm = new RapportWindows(Reservations.ID, "", "ALL");
                            //frm.ShowDialog();
                            //frm.Print();
                        }
                        catch (Exception ex)
                        {

                        }

                    }

                    //MessageBox.Show("Operation terminée", "Message", MessageBoxButton.OK, MessageBoxImage.None);

                    Clear();

                    GlobalFO.rpVM.Load();

                    chambreAutoCompleteBox.Focus();

                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Échec du fournisseur sous-jacent sur Commit.")
                {

                }
                else
                {

                }


            }
        }

        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void rcbChambres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rcbChambres.SelectedItem != null)
            {
                Chambres Cham = rcbChambres.SelectedItem as Chambres;

                if (Cham.EtatOperation == "SALLE")
                {
                    MessageBox.Show("La chambre est salle veuillez la nettoyée", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                    rcbChambres.SelectedItem = null;
                    return;
                }
                else
                {
                    chambreAutoCompleteBox.SelectedItem = rcbChambres.SelectedItem as Chambres;

                    UpdateMontant();
                }

            }

            rtHD.Focus();
        }

        private void chambreAutoCompleteBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            UpdateMontant();
        }

        private void chambreAutoCompleteBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                if (chambreAutoCompleteBox.SelectedItem != null)
                {
                    Chambres Cham = chambreAutoCompleteBox.SelectedItem as Chambres;

                    if (Cham.EtatOperation == "SALLE")
                    {
                        MessageBox.Show("La chambre est salle veuillez la nettoyée", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                        rcbChambres.SelectedItem = null;
                        return;
                    }
                    else
                    {
                        rcbChambres.SelectedItem = chambreAutoCompleteBox.SelectedItem as Chambres;

                        rnHeure.Focus();
                        UpdateMontant();
                    }




                }
                else if (chambreAutoCompleteBox.SearchText != "")
                {
                    MessageBox.Show("Ce numéro de chambre n’existe pas. Veuillez entrer un numéro correct ou appuyez F1", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Clear();
                }



            }
        }

        private void Clear()
        {

            chambreAutoCompleteBox.SelectedItem = null;
            rcbChambres.SelectedIndex = -1;
            rtHD.SelectableDateStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 07, 00, 00);
            rtHD.SelectableDateEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 22, 00, 00);
            rtHF.SelectableDateStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 07, 00, 00);
            rtHF.SelectableDateEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 22, 00, 00);
            rtHD.SelectedTime = DateTime.Now.TimeOfDay;
            rtHF.SelectedTime = DateTime.Now.AddMinutes(60).TimeOfDay;
            //rtHD.SelectedValue = new DateTime(2013, 9, 18, 07, 00, 00);
            //rtHF.SelectedTime = rtHD.SelectedValue.Value.AddMinutes(60).TimeOfDay;
            rntarif.Value = 0;

            Load();

        }

        private void Load()
        {
            GlobalData.Init();

            var resSer = from res in GlobalData.model.Chambres
                         where res.Etat != "SUPPRIMER"
                         select res;

            ObservableCollection<Chambres> lstChambre = new ObservableCollection<Chambres>();

            foreach (Chambres item in resSer)
            {
                if (isAvailable(rtHD.SelectedValue.Value, rtHF.SelectedValue.Value, item.ID))
                {
                    if (item.EtatOperation == "LIBRE" || item.EtatOperation == "RESERVER")
                    {
                        lstChambre.Add(item);
                    }

                }
            }

            rcbChambres.ItemsSource = lstChambre;
            chambreAutoCompleteBox.ItemsSource = lstChambre;

            DisableBoutton(false);

            if (Chambre != null)
            {

                foreach (Chambres item in rcbChambres.Items)
                {
                    if (item.Numero == Chambre)
                    {
                        rcbChambres.SelectedItem = item;
                        //chambreAutoCompleteBox.SelectedItem = item;
                        //rcbChambres.SelectedItem = chambreAutoCompleteBox.SelectedItem as Chambres;
                        chambreAutoCompleteBox.Focus();
                        //DisableBoutton(true);
                    }
                }

                //chambreAutoCompleteBox.SearchText
            }

            
        }

        public bool isAvailable(DateTime datedeb, DateTime dateF, int chambre)
        {

            DateTime date = datedeb;
            DateTime datefin = dateF;

            if (chambre != null)
            {
                var resreserv = from res in GlobalData.model.Reservations
                                where res.idChambre == chambre && res.Etat != "TERMINER" && (date >= res.DateArrive || datefin >= res.DateArrive) && (date <= res.DateDepart || datefin <= res.DateDepart)
                                select res;

                if (resreserv != null && resreserv.Count() != 0)
                {
                    return false;
                }

            }

            return true;
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            Clear();
            chambreAutoCompleteBox.Focus();
        }

        private void rnHeure_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                UpdateMontant();
                btnValider.Focus();
                //rntarif.Focus();


            }
        }

        private void rtHD_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (chambreAutoCompleteBox.SelectedItem != null)
                {

                    Chambres item = chambreAutoCompleteBox.SelectedItem as Chambres;

                    if (!isAvailable(rtHD.SelectedValue.Value, rtHF.SelectedValue.Value, item.ID))
                    {
                        Load();
                        MessageBox.Show("Ce numéro de chambre n’existe pas. Veuillez entrer un numéro correct ou appuyez F1", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    }

                    rnHeure.Focus();
                }

            }
        }

        private void rtHF_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnValider.Focus();
                //rntarif.Focus();


            }
        }

        private void rntarif_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void rntarif_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                btnValider.Focus();

            }
        }

        private void btnAnnuler_KeyDown_1(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Enter)
            //{

            //    chambreAutoCompleteBox.Focus();


            //}
        }

        private void btnValider_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                //chambreAutoCompleteBox.Focus();


            }
        }

        private void rtHF_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            UpdateMontant();
        }

        private void rtHD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                rtHF.SelectedTime = rtHD.SelectedTime.Value.Add(TimeSpan.FromMinutes((double)rnHeure.Value * 60));

                Load();
                UpdateMontant();
            }
            catch (Exception)
            {
                
            }
        }

        private void UpdateMontant()
        {
            if (rcbChambres.SelectedItem != null)
            {
                Chambres Cham = rcbChambres.SelectedItem as Chambres;

                int NbreHr = -rtHD.SelectedTime.Value.Subtract(rtHF.SelectedTime.Value).Hours;

                if (NbreHr > 0)
                {

                    ReservationTypes resSer = GlobalData.model.ReservationTypes.Where(c => c.Etat == "ACTIF" && c.ReservationType == "PASSAGE").FirstOrDefault();

                    TypePrix tpPrix = GlobalData.model.TypePrix.Where(c => c.Etat == "ACTIF" && c.idReservationType == resSer.ID).FirstOrDefault();


                    var resreserv = from res in GlobalData.model.PrixChambres
                                    where res.Etat == "ACTIF" && res.idTypePrix == tpPrix.idTypePrix && res.idTypeChambre == Cham.TypeChambre
                                    select res;

                    if (resreserv != null && resreserv.Count() != 0)
                    {
                        PrixChambres prix = resreserv.FirstOrDefault();

                        var resOR = from res in GlobalData.model.OperationRules
                                    where res.Etat == "ACTIF" && res.idReservationTypes == resSer.ID && res.idTypeChambre == Cham.TypeChambre
                                    select res;

                        if (resOR != null && resOR.Count() != 0)
                        {

                            rntarif.Value = NbreHr * (double)prix.Prix;

                            foreach (OperationRules OpRule in resOR)
                            {

                                int nbr = (int)(NbreHr / (OpRule.Quantite + 1));
                                double h = NbreHr;
                                double n = (double)OpRule.Quantite;
                                double r = (double)OpRule.Reductions.Valeur;
                                double p = (double)prix.Prix;
                                double pc = 0;


                                switch (OpRule.Rules.idRules)
                                {
                                    case 1:

                                        pc = (r * (int)(h / (n + 1)));
                                        rntarif.Value -= pc;

                                        break;
                                    case 2:
                                        if (h > n)
                                        {
                                            pc = (r * (h - n));
                                        }
                                        else
                                        {
                                            pc = 0;
                                        }

                                        rntarif.Value -= pc;
                                        break;
                                    case 3:
                                        pc = (r * h);
                                        rntarif.Value -= pc;
                                        break;
                                    case 4:
                                        pc = (r);
                                        rntarif.Value -= pc;
                                        break;
                                    case 5:
                                        pc = (r);
                                        rntarif.Value -= pc;
                                        break;

                                    default:
                                        break;
                                }
                            }

                        }
                        else
                        {
                            rntarif.Value = Convert.ToDouble(prix.Prix * NbreHr);

                        }

                        DisableBoutton(true);

                    }
                    else
                    {
                        return;
                    }



                }
                else
                {
                    DisableBoutton(false);
                }



            }


        }

        public void DisableBoutton(bool ischecked)
        {
            btnValider.IsEnabled = ischecked;
        }

        private void rnHeure_ValueChanged(object sender, Telerik.Windows.Controls.RadRangeBaseValueChangedEventArgs e)
        {
            try
            {
                if (rtHD.SelectedDate != null && rnHeure.Value != null && rnHeure.Value != 0)
                {
                    //rnDuree.Value = dtFin.SelectedDate.Value.Subtract(dtDebut.SelectedDate.Value).Hours;

                    rtHF.SelectedTime = rtHD.SelectedTime.Value.Add(TimeSpan.FromMinutes((double)rnHeure.Value * 60));

                    double nh = Convert.ToDouble(rtHF.SelectedValue.Value.Hour + "," + rtHF.SelectedValue.Value.Minute);

                    if (nh > 23.59 || nh < 00)
                    {
                        rnHeure.Value = e.OldValue;
                        rtHF.SelectedTime = rtHD.SelectedTime;

                        MessageBox.Show("Le temp limite des passages est de " + 07 + "h à " + 00 + "h ", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    UpdateMontant();
                }
            }
            catch (Exception)
            {

            }
        }

        private void UserControl_Loaded_2(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.F1)
            //{
            //    HelpWindows frm = new HelpWindows();
            //    frm.Show();
            //}
        }

    }
}
