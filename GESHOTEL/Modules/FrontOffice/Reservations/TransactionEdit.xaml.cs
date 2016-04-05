using GESHOTEL.Models;
using System;
using System.Collections.Generic;
using System.Data.Objects;
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

namespace GESHOTEL.FrontOffice
{
    /// <summary>
    /// Interaction logic for TransactionEdit.xaml
    /// </summary>
    public partial class TransactionEdit : UserControl
    {
        public GESHOTELEntities model;
        Reservations res;
        string PaneHeader = "";

        public TransactionEdit(int Res, string str)
        {
            InitializeComponent();

            model = new GESHOTELEntities();
            res = model.Reservations.Where(c => c.ID == Res).FirstOrDefault();
            PaneHeader = str;

            ClearArticle();
            ClearPaie();
            load();


        }

        private void load()
        {

            System.Windows.Data.CollectionViewSource reservationsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("reservationsViewSource")));

            reservationsViewSource.Source = model.Reservations.Where(c => c.ID == res.ID).ToList();

            System.Windows.Data.CollectionViewSource transactionsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("transactionsViewSource")));

            rcbModePaiement.ItemsSource = model.MethodePaiements.Where(c => c.Etat == "ACTIF").ToList();

            transactionsViewSource.Source = res.Transactions;
            //this.DataContext = res;
            Transactions Trans = res.Transactions.FirstOrDefault();
            //this.rtc.DataContext = Trans;
            rgvExtraCharges.ItemsSource = Trans.DetailTransactions.Where(c => c.Etat == "ACTIF" && c.Produits.Categories.Libelle != "CHAMBRES").ToList();
            grvChambreCharge.ItemsSource = Trans.DetailTransactions.Where(c => c.Etat == "ACTIF" && c.Produits.Categories.Libelle == "CHAMBRES").ToList();
            grvCharge.ItemsSource = Trans.DetailTransactions.Where(c => c.Etat == "ACTIF" && c.Produits.Categories.Libelle != "CHAMBRES").ToList();
            grvPaiement.ItemsSource = Trans.DetailPaiements.Where(c => c.Etat == "ACTIF").ToList();

            if (res.isCheckIn == true)
            {
                chkCheckedIn.IsEnabled = false;
            }
        }

        private void btnClientModifier_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSejourModifier_Click(object sender, RoutedEventArgs e)
        {
            //System.Windows.Data.CollectionViewSource transactionsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("transactionsViewSource")));
            //Transactions Trans = transactionsViewSource.View.CurrentItem as Transactions;

            //double montant = (double)Trans.DetailTransactions.Where(c => c.Produits.Categories.Libelle != "CHAMBRES").Sum(c => c.Montant);
            //ModificationSejourNuit view = new ModificationSejourNuit(res, model, montant);
            //view.ShowDialog();
        }


        private void btnChambreModifier_Click(object sender, RoutedEventArgs e)
        {

            //AssigneChambre view = new AssigneChambre(res, model, "Modifier");
            //view.ShowDialog();
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void rcbCategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rcbCategorie.SelectedItem != null)
            {
                Categories cat = rcbCategorie.SelectedItem as Categories;
                rcbProduit.ItemsSource = model.Produits.Where(c => c.Etat == "ACTIF" && c.Categorie == cat.ID).ToList();
            }

        }

        private void rcbProduit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rcbProduit.SelectedItem != null)
            {
                Produits pro = rcbProduit.SelectedItem as Produits;

                rnPU.Value = (double)pro.PrixUnitaire;
                rnQuantite.Value = 1;
            }
        }

        private void RadTabItem_Loaded_1(object sender, RoutedEventArgs e)
        {
            //model.Refresh(RefreshMode.StoreWins, model.Categories);
            //model.Refresh(RefreshMode.StoreWins, model.Produits);

            rcbCategorie.ItemsSource = model.Categories.Where(c => c.Etat == "ACTIF").ToList();

        }

        private void RadTabItem_GotFocus_1(object sender, RoutedEventArgs e)
        {
            //model.Refresh(RefreshMode.StoreWins, model.Categories);
            //model.Refresh(RefreshMode.StoreWins, model.Produits);

            rdpDate.SelectedValue = DateTime.Now;


            rcbCategorie.ItemsSource = model.Categories.Where(c => c.Etat == "ACTIF").ToList();
        }

        private void btnAjouter_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Data.CollectionViewSource transactionsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("transactionsViewSource")));
                Transactions Trans = transactionsViewSource.View.CurrentItem as Transactions;
                Produits pro = rcbProduit.SelectedItem as Produits;


                DetailTransactions trans = ArticleExistGrid(pro);

                if (trans != null)
                {

                    trans.Quantite += (float)rnQuantite.Value;
                    trans.Montant = Convert.ToDecimal(rnPU.Value * trans.Quantite);


                }
                else
                {
                    trans = new DetailTransactions();
                    trans.Produits = pro;
                    trans.Date = rdpDate.SelectedDate;
                    trans.Quantite = (float)rnQuantite.Value;
                    trans.prix = Convert.ToDecimal(rnPU.Value);
                    trans.Descriptions = pro.Libelle;
                    trans.Montant = Convert.ToDecimal(rnPU.Value * rnQuantite.Value);
                    trans.Etat = "ACTIF";
                    //trans.idHotel = GlobalData.HotId;

                }

                Trans.DetailTransactions.Add(trans);

                Trans.TotalTTC += Convert.ToDecimal(rnPU.Value * (float)rnQuantite.Value);
                Trans.TotalReste = Trans.TotalTTC - Trans.TotalPaye;

                res.TotalPaye = Trans.TotalPaye;
                res.TotalReste = Trans.TotalReste;
                res.TotalTTC = Trans.TotalTTC;
                res.Reduction = Trans.Reduction;

                if (Trans.TotalReste != 0)
                {

                    Trans.Etat = "PARTIELLEMENT PAYE";

                }

                ClearArticle();
                load();



            }
            catch (Exception)
            {

            }
        }

        private void ClearArticle()
        {
            rdpDate.SelectedValue = DateTime.Now;
            txtCommentaire.Text = "";
            txtVoucher.Text = "";
            rnQuantite.Value = 0;
            rnPU.Value = 0;

            rcbCategorie.SelectedIndex = -1;
            rcbProduit.SelectedIndex = -1;

        }

        private DetailTransactions ArticleExistGrid(Produits art)
        {
            System.Windows.Data.CollectionViewSource transactionsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("transactionsViewSource")));
            Transactions Trans = transactionsViewSource.View.CurrentItem as Transactions;

            foreach (DetailTransactions item in Trans.DetailTransactions)
            {

                if (item.Produits.ID == art.ID)
                {

                    return item;

                }
            }

            return null;
        }

        private void RadButton_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            var results = MessageBox.Show("Voulez-vous enregistrer ?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (results == MessageBoxResult.Yes)
            {

                model.SaveChanges();

                //GlobalFO.rpVM.Load();

                GlobalData.RemoveItem(PaneHeader);

            }
        }

        private void btnAjouterPaie_Click_1(object sender, RoutedEventArgs e)
        {
            if (rcbModePaiement.SelectedIndex != -1 && rnPaieMontant.Value != 0)
            {
                System.Windows.Data.CollectionViewSource transactionsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("transactionsViewSource")));
                Transactions Trans = transactionsViewSource.View.CurrentItem as Transactions;


                if ((double)Trans.TotalReste < rnPaieMontant.Value)
                {
                    MessageBox.Show("Le montant entrer est superieure au montant a payé", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Trans.TotalPaye += Convert.ToDecimal(rnPaieMontant.Value);
                Trans.TotalReste = Trans.TotalTTC - Trans.TotalPaye;

                res.TotalPaye = Trans.TotalPaye;
                res.TotalReste = Trans.TotalReste;
                res.TotalTTC = Trans.TotalTTC;
                res.Reduction = Trans.Reduction;

                DetailPaiements dtPaie = new DetailPaiements();
                if (Trans.TotalReste == 0)
                {

                    dtPaie.Montant = Convert.ToDouble(rnPaieMontant.Value);
                    dtPaie.DatePaiement = rdpDatepaie.SelectedDate;
                    dtPaie.MethodePaiements = rcbModePaiement.SelectedItem as MethodePaiements;
                    //dtPaie.Transactions = Trans;
                    dtPaie.Etat = "ACTIF";
                    //dtPaie.idHotel = GlobalData.HotId;

                    Trans.Etat = "PAYE";


                }
                else
                {
                    if (rnPaieMontant.Value != 0)
                    {

                        dtPaie.Montant = Convert.ToDouble(rnPaieMontant.Value);
                        dtPaie.DatePaiement = rdpDatepaie.SelectedDate;
                        dtPaie.MethodePaiements = rcbModePaiement.SelectedItem as MethodePaiements;
                        //dtPaie.Transactions = Trans;
                        dtPaie.Etat = "ACTIF";
                        //dtPaie.idHotel = GlobalData.HotId;

                        Trans.Etat = "PARTIELLEMENT PAYE";
                        //Trans.EtatTables = "TERMINER";
                    }

                }

                Trans.DetailPaiements.Add(dtPaie);
                ClearPaie();
            }

        }

        private void ClearPaie()
        {


            rdpDatepaie.SelectedValue = DateTime.Now;
            rnPaieMontant.Value = 0;

            rcbModePaiement.SelectedIndex = 0;

            try
            {
                System.Windows.Data.CollectionViewSource transactionsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("transactionsViewSource")));
                Transactions Trans = transactionsViewSource.View.CurrentItem as Transactions;

                grvPaiement.ItemsSource = Trans.DetailPaiements.Where(c => c.Etat == "ACTIF").ToList();

            }
            catch (Exception)
            {
            }
        }

        private void rcbModePaiement_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void rcbModePaiement_GotFocus(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource transactionsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("transactionsViewSource")));
            Transactions Trans = transactionsViewSource.View.CurrentItem as Transactions;

            if (Trans.Etat == "PAYE")
            {
                btnAjouterPaie.IsEnabled = false;
            }
            else
            {
                btnAjouterPaie.IsEnabled = true;
            }
        }

        private void btnbtnImprimer_Click(object sender, RoutedEventArgs e)
        {

                //try
                //{
                //    RapportWindows frm = new RapportWindows("Facture.trdx", res.Transactions.FirstOrDefault().ID);
                //    frm.ShowDialog();
                //}
                //catch (Exception)
                //{

                //}

            
        }

        private void chkCheckedIn_Checked(object sender, RoutedEventArgs e)
        {

            if (res.EtatOperation == "RESERVEE")
            {

                if (isAvailable(res.DateArrive.Value, res.DateDepart.Value, res.Chambres))
                {
                    if (res.Chambres.EtatOperation != "LIBRE" || res.Chambres.EtatOperation != "RESERVER")
                    {
                        res.Etat = "ACTIF";
                        res.EtatOperation = "ARRIVEE";
                        res.Chambres.EtatOperation = "OCCUPER";

                        res.DateCheckIn = DateTime.Now;
                        res.isCheckIn = true;
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

        }


        private void chkCheckedIn_Unchecked_1(object sender, RoutedEventArgs e)
        {
            if (res.EtatOperation != "RESERVEE")
            {
                chkCheckedIn.IsChecked = true;

            }
        }

        public bool isAvailable(DateTime datedeb, DateTime dateF, Chambres chambre)
        {

            DateTime date = datedeb;
            DateTime datefin = dateF;

            if (chambre != null)
            {
                var resreserv = from res in model.Reservations
                                where res.ID != res.ID && res.idChambre == chambre.ID && res.Etat != "TERMINER" && (date >= res.DateArrive || datefin >= res.DateArrive) && (date <= res.DateDepart || datefin <= res.DateDepart)
                                select res;

                if (resreserv != null && resreserv.Count() != 0)
                {
                    return false;
                }

            }

            return true;
        }

        private void chkCheckedOut_Checked(object sender, RoutedEventArgs e)
        {

            if (res.Etat != "DUE OUT")
            {
                var result = MessageBox.Show("Le séjour/passage n’est pas encore terminé. Voulez-vous libérer cette chambre ?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (res.TotalReste == 0)
                    {
                        res.Etat = "TERMINER";
                        res.EtatOperation = "TERMINER";
                        res.Chambres.EtatOperation = "SALLE";

                        res.DateCheckOut = DateTime.Now;
                        //res.isCheckOut = true;

                    }
                    else
                    {
                        MessageBox.Show("Le Sejour de la chambre " + res.Chambres.Numero + " n’a pas encore été soldé ", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    chkCheckedOut.IsChecked = false;
                }
            }
            else
            {
                if (res.TotalReste == 0)
                {
                    res.Etat = "TERMINER";
                    res.EtatOperation = "TERMINER";
                    res.Chambres.EtatOperation = "SALLE";

                    res.DateCheckOut = DateTime.Now;
                    //res.isCheckOut = true;

                }
                else
                {
                    chkCheckedOut.IsChecked = false;
                    MessageBox.Show("Le Sejour n’a pas encore été soldé", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                }
            }


        }


    }
}
