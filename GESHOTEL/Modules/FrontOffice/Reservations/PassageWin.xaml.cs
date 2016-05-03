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
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace GESHOTEL.ReservationsModules
{
    /// <summary>
    /// Interaction logic for PassageWin.xaml
    /// </summary>
    public partial class PassageWin : Window
    {

        Reservations Res;

        public PassageWin(int res)
        {
            InitializeComponent();

            Res = GlobalData.model.Reservations.Where(c => c.ID == res).FirstOrDefault();
        }

        private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
        {


            try
            {

                Chambres Cham = rcbChambres.SelectedItem as Chambres;

                int NbreHr = (int)rnHeure.Value;

                if (rcbChambres.SelectedIndex == -1) { MessageBox.Show("Choisissez une chambre svp", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning); return; }
                if (NbreHr <= 0) { MessageBox.Show("Le nombre d'heure ne peut pas etre égale a 0", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning); return; }

                Transactions Trans = Res.Transactions.FirstOrDefault();

                Trans.TotalPaye = Convert.ToDecimal(rntarif.Value);
                Trans.TotalTTC = Convert.ToDecimal(rntarif.Value);
                Trans.TotalHT = Convert.ToDecimal(rntarif.Value);
                Trans.TVA = 0;
                Trans.TotalReste = 0;

                //Trans.Etat = "PAYE";

                DetailPaiements dtPaie = Trans.DetailPaiements.FirstOrDefault();
                dtPaie.Montant = rntarif.Value;

                DetailTransactions dtTrans = Trans.DetailTransactions.FirstOrDefault();
                dtTrans.prix = Convert.ToDecimal(rntarif.Value);
                dtTrans.Quantite = 1;
                dtTrans.Montant = Convert.ToDecimal(rntarif.Value);

                Res.Chambres.EtatOperation = "LIBRE";
                Res.Chambres = Cham;
                Cham.EtatOperation = "OCCUPER";
                //Res.Chambres = Cham;
                Res.NbreNuit = NbreHr;
                Res.TotalPaye = Trans.TotalPaye;
                Res.TotalReste = Trans.TotalReste;
                Res.TotalTTC = Trans.TotalTTC;
                Res.DateDepart = new DateTime(rtHF.SelectedDate.Value.Year, rtHF.SelectedDate.Value.Month, rtHF.SelectedDate.Value.Day, rtHF.SelectedTime.Value.Hours, rtHF.SelectedTime.Value.Minutes, rtHF.SelectedTime.Value.Seconds);

                GlobalData.model.SaveChanges();

                MessageBox.Show("Operation terminée","Message", MessageBoxButton.OK, MessageBoxImage.None);

                this.Close();

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
            this.Close();
        }

        private void rcbChambres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rcbChambres.SelectedItem != null)
            {
                Chambres Cham = rcbChambres.SelectedItem as Chambres;

                chambreAutoCompleteBox.SelectedItem = rcbChambres.SelectedItem as Chambres;

                UpdateMontant();
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
                    rcbChambres.SelectedItem = chambreAutoCompleteBox.SelectedItem as Chambres;

                    rnHeure.Focus();
                    UpdateMontant();


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

            rntarif.Value = 0;

            Load();

            ModBinding();

            rtHD.Focus();

        }

        private void Load()
        {
            //GlobalData.model.Refresh(System.Data.Objects.RefreshMode.StoreWins, GlobalData.model.Chambres);

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

            System.Windows.Data.CollectionViewSource chambresViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("chambresViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            chambresViewSource.Source = lstChambre;

            //DisableBoutton(false);
        }

        private void UpdateMontant()
        {

            Chambres Cham = rcbChambres.SelectedItem as Chambres;

            if (rcbChambres.SelectedItem != null)
            {
                int NbreHr = -rtHD.SelectedTime.Value.Subtract(rtHF.SelectedTime.Value).Hours;

                if (NbreHr > 0)
                {

                    ReservationTypes resSer = GlobalData.model.ReservationTypes.Where(c =>  c.Etat == "ACTIF" && c.ReservationType == "PASSAGE").FirstOrDefault();

                    TypePrix tpPrix = GlobalData.model.TypePrix.Where(c =>  c.Etat == "ACTIF" && c.idReservationType == resSer.ID).FirstOrDefault();


                    var resreserv = from res in GlobalData.model.PrixChambres
                                    where  res.Etat == "ACTIF" && res.idTypePrix == tpPrix.idTypePrix && res.idTypeChambre == Cham.TypeChambre
                                    select res;

                    if (resreserv != null && resreserv.Count() != 0)
                    {
                        PrixChambres prix = resreserv.FirstOrDefault();

                        var resOR = from res in GlobalData.model.OperationRules
                                    where  res.Etat == "ACTIF" && res.idReservationTypes == resSer.ID && res.idTypeChambre == Cham.TypeChambre
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

                    }
                    else
                    {
                        return;
                    }



                }
                else
                {

                }
            }

        }

        public void DisableBoutton(bool ischecked)
        {
            btnValider.IsEnabled = ischecked;
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
                if (rcbChambres.SelectedItem != null)
                {
                    Chambres item = rcbChambres.SelectedItem as Chambres;

                    if (!isAvailable(rtHD.SelectedValue.Value, rtHF.SelectedValue.Value, item.ID))
                    {
                        Load();
                        MessageBox.Show("Chambre pas disponible", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    }

                    rnHeure.Focus();
                }

            }
        }

        private void rtHF_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                Chambres item = Res.Chambres;

                if (!isAvailable(rtHD.SelectedValue.Value, rtHF.SelectedValue.Value, item.ID))
                {
                    MessageBox.Show("Chambre pas disponible", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                }

                rnHeure.Focus();


                btnValider.Focus();
                //rntarif.Focus();


            }
        }

        public bool isAvailable(DateTime datedeb, DateTime dateF, int chambre)
        {

            DateTime date = datedeb;
            DateTime datefin = dateF;

            if (chambre != 0)
            {
                var resreserv = from res in GlobalData.model.Reservations
                                where res.ID != Res.ID && res.idChambre == chambre && res.Etat != "TERMINER" && (date >= res.DateArrive || datefin >= res.DateArrive) && (date <= res.DateDepart || datefin <= res.DateDepart)
                                select res;

                if (resreserv != null && resreserv.Count() != 0)
                {
                    return false;
                }

            }

            return true;
        }

        private void rtHF_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            UpdateMontant();

        }

        private void rtHD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rtHF.SelectedTime = rtHD.SelectedTime.Value.Add(TimeSpan.FromMinutes((double)rnHeure.Value * 60));

            Load();
            UpdateMontant();
        }

        private void rnHeure_ValueChanged(object sender, Telerik.Windows.Controls.RadRangeBaseValueChangedEventArgs e)
        {
            try
            {
                if (rtHD.SelectedDate != null && rnHeure.Value != null && rnHeure.Value != 0)
                {
                    //rnDuree.Value = dtFin.SelectedDate.Value.Subtract(dtDebut.SelectedDate.Value).Hours;

                    rtHF.SelectedTime = rtHD.SelectedTime.Value.Add(TimeSpan.FromMinutes((double)rnHeure.Value * 60));

                    double nh = Convert.ToDouble(rtHF.SelectedValue.Value.Hour + "." + rtHF.SelectedValue.Value.Minute);

                    if (nh > 23.59 || nh < 00)
                    {
                        rnHeure.Value = e.OldValue;
                    }

                    UpdateMontant();
                }
            }
            catch (Exception)
            {

            }
        }

        private void ModBinding()
        {

            rtHD.SelectedValue = Res.DateArrive;
            rnHeure.Value = (double)Res.NbreNuit;

            System.Windows.Data.CollectionViewSource chambresViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("chambresViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            chambresViewSource.View.MoveCurrentTo(Res.Chambres);

        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            Clear();
        }

    }
}
