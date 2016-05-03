using GESHOTEL.Models;
using GESHOTEL.Rapports.Viewers;
using GESHOTEL.ReservationsModules;
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

namespace GESHOTEL.ReservationsModules
{
    /// <summary>
    /// Interaction logic for StayInsert.xaml
    /// </summary>
    public partial class StayInsert : UserControl
    {

        string Etat;
        string EtatClear;
        string msg;
        string ReductionType;
        string Chambre;
        string ResType;
        bool loaded = false;

        public string Msg
        {
            get { return msg; }
            set { msg = value; }
        }

        string errorMsg;

        public string ErrorMsg
        {
            get { return errorMsg; }
            set { errorMsg = value; }
        }

        public StayInsert()
        {
            InitializeComponent();
        }

        public StayInsert(string num, string resType)
        {
            InitializeComponent();

            this.Chambre = num;
            this.ResType = resType;
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            Clear();

            rnAdult.Value = 1;
        }

        private void Load()
        {
            GlobalData.Init();

            rcbTypeReservation.ItemsSource = GlobalData.model.ReservationTypes.Where(c => c.Etat == "ACTIF" && c.ReservationType != "PASSAGE").ToList();

            rcbTypeChambre.ItemsSource = GlobalData.model.TypeChambres.Where(c => c.Etat == "ACTIF").ToList();

            System.Windows.Data.CollectionViewSource paysViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("paysViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            paysViewSource.Source = GlobalData.model.Pays.Where(c => c.Etat == "ACTIF").ToList();
            System.Windows.Data.CollectionViewSource villesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("villesViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            villesViewSource.Source = GlobalData.model.Villes.Where(c => c.Etat == "ACTIF").ToList();
            System.Windows.Data.CollectionViewSource nationalitesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("NationalitesViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            nationalitesViewSource.Source = GlobalData.model.Nationalités.Where(c => c.Etat == "ACTIF").ToList();
            nomsRadComboBox.ItemsSource = GlobalData.model.Clients.Where(c => c.Etat == "ACTIF").ToList();
            txtNoms.ItemsSource = GlobalData.model.Clients.Where(c => c.Etat == "ACTIF").ToList();
            System.Windows.Data.CollectionViewSource iDTypesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("iDTypesViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            iDTypesViewSource.Source = GlobalData.model.IDTypes.Where(c => c.Etat == "ACTIF").ToList();

            System.Windows.Data.CollectionViewSource communesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("communesViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            communesViewSource.Source = GlobalData.model.Communes.Where(c => c.Etat == "ACTIF").ToList();
            System.Windows.Data.CollectionViewSource quartiersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("quartiersViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            quartiersViewSource.Source = GlobalData.model.Quartiers.Where(c => c.Etat == "ACTIF").ToList();

            rcbModePaiement.ItemsSource = GlobalData.model.MethodePaiements.Where(c => c.Etat == "ACTIF").ToList();
            rcbRules.ItemsSource = GlobalData.model.Rules.Where(c => c.Etat == "ACTIF").ToList();
            rcbReductions.ItemsSource = GlobalData.model.Reductions.Where(c => c.Etat == "ACTIF").ToList();
            
            dpArrival.SelectedValue = DateTime.Now;

            if (rnNuit.Value == null)
            {
                dpDepart.SelectedValue = DateTime.Now.AddDays(0);
            }
            else
            {
                dpDepart.SelectedValue = DateTime.Now.AddDays((int)rnNuit.Value);
            }


            rcbReductions.SelectedIndex = -1;
            rcbRules.SelectedIndex = -1;
            rcbTypeChambre.SelectedIndex = 0;

            loaded = true;

        }

        private void Clear()
        {

            EtatClear = "CLEAR";
            dpArrival.SelectableDateStart = DateTime.Now;
            rnAdult.Value = 1;

            //rcbTypeChambre.SelectedIndex = -1;

            Load();

            rcbReductions.SelectedIndex = -1;
            rcbRules.SelectedIndex = -1;
            rcbChambres.SelectedIndex = -1;

            paysRadComboBox.SelectedIndex = -1;
            villeRadComboBox.SelectedIndex = -1;
            communesRadComboBox.SelectedIndex = -1;
            quartierRadComboBox.SelectedIndex = -1;
            nationaliteRadComboBox.SelectedIndex = -1;
            iDTypesRadComboBox.SelectedIndex = -1;
            nomsRadComboBox.SelectedIndex = -1;
            rdMontantRestant.Value = 0;
            rdMontant.Value = 0;
            rdTTC.Value = 0;
            rdTVA.Value = 0;

            txtAddresse.Text = "";
            txtCellulaire.Text = "";
            txtEmail.Text = "";
            txtFax.Text = "";
            txtIdNumber.Text = "";
            txtLieuNaissance.Text = "";
            txtNoms.SearchText = "";
            txtPrenoms.Text = "";
            txttelephone.Text = "";

            Reservations Reservations = new Reservations();
            this.gbSejour.DataContext = Reservations;

            Clients client = new Clients();
            this.gbInfoClient.DataContext = client;
            this.gbContactClient.DataContext = client;
            this.gbAutreClient.DataContext = client;

            Transactions Trans = new Transactions();
            Trans.Reduction = 0;
            this.gbFacture.DataContext = Trans;
            this.grdPaiement.DataContext = Trans;
            this.grdReduction.DataContext = Trans;

            if (Chambre != null)
            {
                int i = 0;
                foreach (TypeChambres item in rcbTypeChambre.Items)
                {
                    if (item.Libelle == ResType)
                    {

                        rcbTypeChambre.SelectedIndex = i;

                    }

                    i++;
                }


                i = 0;
                foreach (Chambres item in rcbChambres.Items)
                {
                    if (item.Numero == Chambre)
                    {

                        //rcbChambres.SelectedIndex = i;
                        rcbChambres.SelectedValue = item;

                    }

                    i++;
                }

                //chambreAutoCompleteBox.SearchText
            }

            rnAdult.Value = 1;
            EtatClear = "";

        }

        private void ClearAll()
        {
            //rnAdult.Value = 1;
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


                if (!isAvailable())
                {
                    MessageBox.Show("Cette chambre n'est pas disponible", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Chambres Cham = rcbChambres.SelectedItem as Chambres;

                if (Cham.EtatOperation == "SALLE")
                {
                    MessageBox.Show("La chambre est salle veuillez la nettoyée", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var results = MessageBox.Show("Voulez-vous enregistrer cette reservation ?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (results == MessageBoxResult.Yes)
                {

                    Clients client = (Clients)this.gbInfoClient.DataContext;
                    client.Noms = txtNoms.SearchText;
                    client.Villes = autoSaveVilles(villeRadComboBox.Text);
                    client.Nationalités = autoSaveNationalites(nationaliteRadComboBox.Text);
                    client.Communes = autoSaveCommunes(communesRadComboBox.Text);
                    client.Quartiers = autoSaveQuartiers(quartierRadComboBox.Text);

                    if (client.Etat == null)
                    {
                        client.Etat = "ACTIF";
                        //client.idHotel = GlobalData.HotId;
                    }

                    Transactions Trans = (Transactions)this.gbFacture.DataContext;

                    if (client.Noms == null) { MessageBox.Show("Choisissez un Client svp", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning); return; }
                    //if (client.Numero == null || client.IDTypes == null) { MessageBox.Show("Veuillez choisir le type de piece et saisisser le numero svp", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning); return; }
                    if (rcbChambres.SelectedIndex == -1) { MessageBox.Show("Choisissez une chambre svp", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning); return; }
                    if (rnNuit.Value <= 0) { MessageBox.Show("Le nombre de nuit ne peut pa etre egale a 0", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning); return; }

                    Trans.DateTransaction = DateTime.Now;
                    Trans.TransactionNumero = "";


                    DetailPaiements dtPaie = new DetailPaiements();
                    if (Trans.TotalReste == 0)
                    {

                        dtPaie.Montant = rdMontantPaye.Value;
                        dtPaie.DatePaiement = DateTime.Now;
                        dtPaie.MethodePaiements = autoSaveMethodePaiements(rcbModePaiement.Text);
                        dtPaie.Transactions = Trans;
                        dtPaie.Etat = "ACTIF";
                        //dtPaie.idHotel = GlobalData.HotId;

                        Trans.Etat = "PAYE";


                    }
                    else
                    {
                        if (rdMontantPaye.Value != 0)
                        {

                            dtPaie.Montant = rdMontantPaye.Value;
                            dtPaie.DatePaiement = DateTime.Now;
                            dtPaie.MethodePaiements = autoSaveMethodePaiements(rcbModePaiement.Text);
                            dtPaie.Transactions = Trans;
                            dtPaie.Etat = "ACTIF";
                            //dtPaie.idHotel = GlobalData.HotId;

                            Trans.Etat = "PARTIELLEMENT PAYE";
                            //Trans.EtatTables = "TERMINER";
                        }

                    }

                    Trans.DetailPaiements.Add(dtPaie);

                    TypeChambres tpCham = rcbTypeChambre.SelectedItem as TypeChambres;

                    if (Cham.EtatOperation == "LIBRE" || Cham.EtatOperation == "RESERVER")
                    {
                        Cham.EtatOperation = "OCCUPER";
                    }

                    ReservationTypes resType = rcbTypeReservation.SelectedItem as ReservationTypes;

                    Reservations Reservations = (Reservations)this.gbSejour.DataContext;
                    Reservations.Clients = client;
                    Reservations.Chambres = Cham;
                    Reservations.Transactions.Add(Trans);
                    Reservations.ReservationTypes = resType;

                    Reservations.DateArrive = dpArrival.SelectedValue;
                    Reservations.DateDepart = dpDepart.SelectedValue;

                    Reservations.TotalPaye = Trans.TotalPaye;
                    Reservations.TotalReste = Trans.TotalReste;
                    Reservations.TotalTTC = Trans.TotalTTC;
                    Reservations.Reduction = Trans.Reduction;
                    Reservations.TypeOperation = "LOCATION CHAMBRE";
                    //Reservations.idHotel = GlobalData.HotId;
                    Reservations.idRegistre = GlobalData.RegId;
                    Reservations.ReservationDate = DateTime.Now;
                    Reservations.DateCheckIn = DateTime.Now;
                    Reservations.isCheckIn = true;
                    Reservations.EtatOperation = "ARRIVEE";
                    Reservations.Etat = "ACTIF";


                    GetDetailTrans();
                    GetReductionAuto();

                    if (rcbReductions.SelectedItem != null && rcbRules.SelectedItem != null && (double)Trans.TotalHT != 0 && (double)rdReduction.Value != 0)
                    {
                        TranReductions transred = new TranReductions();
                        transred.Reductions = rcbReductions.SelectedItem as Reductions;
                        transred.Rules = rcbRules.SelectedItem as Rules;
                        transred.Transactions = Trans;
                        transred.Quantite = (int)rdNNuit.Value;
                        transred.Pourcentage = rdPourcentage.Value;
                        transred.Valeur = Convert.ToDecimal(rdReduction.Value);
                        transred.Type = ReductionType;
                        transred.Etat = "ACTIF";
                        //transred.idHotel = GlobalData.HotId;

                        Trans.TranReductions.Add(transred);
                    }

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
                            RapportWindows frm = new RapportWindows("Facture.trdx", Trans.ID);
                            frm.ShowDialog();
                        }
                        catch (Exception)
                        {

                        }

                    }

                    GlobalFO.rpVM.Load();

                    Clear();

                    //MessageBox.Show("Operation terminée", "Message", MessageBoxButton.OK, MessageBoxImage.None);

                    Msg = "OK";

                }

            }
            catch (Exception ex)
            {
                if (ex.Message == "Échec du fournisseur sous-jacent sur Commit.")
                {
                    Msg = "OK";
                }
                else
                {
                    Msg = "Error";
                    ErrorMsg = ex.Message;
                }

            }

        }

        private void GetDetailTrans()
        {
            Transactions Trans = (Transactions)this.gbFacture.DataContext;
            DetailTransactions dt;

            if (rcbRules.SelectedItem != null && (double)Trans.TotalHT != 0 && (double)rdReduction.Value != 0)
            {
                Rules OpRule = rcbRules.SelectedItem as Rules;


                int nbr = (int)rnNuit.Value;
                double h = (double)rnNuit.Value;
                double n = (double)rdNNuit.Value;
                double r = 0;
                double p = 0;

                if (ReductionType == "FIXE")
                {
                    r = (double)rdReduction.Value;
                }
                else
                {
                    rdReduction.Value = (double)Trans.TotalHT * (double)rdPourcentage.Value * 0.01;
                    r = (double)rdReduction.Value;
                }

                Chambres Cham = rcbChambres.SelectedItem as Chambres;
                TypeChambres tpCham = rcbTypeChambre.SelectedItem as TypeChambres;

                if (rcbTypeReservation.SelectedIndex != -1)
                {
                    ReservationTypes resType = rcbTypeReservation.SelectedItem as ReservationTypes;

                    TypePrix tpPrix = GlobalData.model.TypePrix.Where(c => c.Etat == "ACTIF" && c.idReservationType == resType.ID).FirstOrDefault();


                    var resreserv = from res in GlobalData.model.PrixChambres
                                    where res.Etat == "ACTIF" && res.idTypePrix == tpPrix.idTypePrix && res.idTypeChambre == tpCham.ID
                                    select res;

                    if (resreserv != null && resreserv.Count() != 0)
                    {
                        PrixChambres prix = resreserv.FirstOrDefault();
                        p = (double)prix.Prix;
                    }
                }

                double pc = 0;


                switch (OpRule.idRules)
                {
                    case 1:

                        for (int i = 1; i <= rnNuit.Value; i++)
                        {
                            if (i % (n + 1) == 0)
                            {
                                DetailTransactions dtTrans = new DetailTransactions();
                                dtTrans.Date = DateTime.Now.AddDays(i - 1);
                                dtTrans.Descriptions = "CHAMBRE " + Cham.TypeChambres.Libelle + " " + Cham.Numero;
                                dtTrans.prix = Convert.ToDecimal(p);
                                dtTrans.Quantite = 1;
                                dtTrans.Reduction = Convert.ToDecimal(r);
                                dtTrans.Montant = (dtTrans.prix * Convert.ToDecimal(dtTrans.Quantite)) - dtTrans.Reduction;
                                dtTrans.Transactions = Trans;
                                dtTrans.Etat = "ACTIF";
                                //dtTrans.idHotel = GlobalData.HotId;
                                dtTrans.Produits = GlobalData.model.Produits.Where(c => c.Libelle == "CHAMBRE").First();

                                Trans.DetailTransactions.Add(dtTrans);
                            }
                            else
                            {
                                DetailTransactions dtTrans = new DetailTransactions();
                                dtTrans.Date = DateTime.Now.AddDays(i - 1);
                                dtTrans.Descriptions = "CHAMBRE " + Cham.TypeChambres.Libelle + " " + Cham.Numero;
                                dtTrans.prix = Convert.ToDecimal(p);
                                dtTrans.Quantite = 1;
                                dtTrans.Reduction = 0;
                                //dtTrans.ReductionAuto = 0;
                                dtTrans.Montant = (dtTrans.prix * Convert.ToDecimal(dtTrans.Quantite)) - dtTrans.Reduction;
                                dtTrans.Transactions = Trans;
                                dtTrans.Etat = "ACTIF";
                                //dtTrans.idHotel = GlobalData.HotId;
                                dtTrans.Produits = GlobalData.model.Produits.Where(c => c.Libelle == "CHAMBRE").First();

                                Trans.DetailTransactions.Add(dtTrans);
                            }



                        }


                        break;
                    case 2:

                        for (int i = 1; i <= rnNuit.Value; i++)
                        {
                            if (i > n)
                            {
                                DetailTransactions dtTrans = new DetailTransactions();
                                dtTrans.Date = DateTime.Now.AddDays(i - 1);
                                dtTrans.Descriptions = "CHAMBRE " + Cham.TypeChambres.Libelle + " " + Cham.Numero;
                                dtTrans.prix = Convert.ToDecimal(p);
                                dtTrans.Quantite = 1;
                                dtTrans.Reduction = Convert.ToDecimal(r);
                                dtTrans.Montant = (dtTrans.prix * Convert.ToDecimal(dtTrans.Quantite)) - dtTrans.Reduction;
                                dtTrans.Transactions = Trans;
                                dtTrans.Etat = "ACTIF";
                                //dtTrans.idHotel = GlobalData.HotId;
                                dtTrans.Produits = GlobalData.model.Produits.Where(c => c.Libelle == "CHAMBRE").First();

                                Trans.DetailTransactions.Add(dtTrans);
                            }
                            else
                            {
                                DetailTransactions dtTrans = new DetailTransactions();
                                dtTrans.Date = DateTime.Now.AddDays(i - 1);
                                dtTrans.Descriptions = "CHAMBRE " + Cham.TypeChambres.Libelle + " " + Cham.Numero;
                                dtTrans.prix = Convert.ToDecimal(p);
                                dtTrans.Quantite = 1;
                                dtTrans.Reduction = 0;
                                dtTrans.Montant = (dtTrans.prix * Convert.ToDecimal(dtTrans.Quantite)) - dtTrans.Reduction;
                                dtTrans.Transactions = Trans;
                                dtTrans.Etat = "ACTIF";
                                //dtTrans.idHotel = GlobalData.HotId;
                                dtTrans.Produits = GlobalData.model.Produits.Where(c => c.Libelle == "CHAMBRE").First();

                                Trans.DetailTransactions.Add(dtTrans);
                            }

                        }

                        break;
                    case 3:
                        for (int i = 1; i <= rnNuit.Value; i++)
                        {
                            DetailTransactions dtTrans = new DetailTransactions();
                            dtTrans.Date = DateTime.Now.AddDays(i - 1);
                            dtTrans.Descriptions = "CHAMBRE " + Cham.TypeChambres.Libelle + " " + Cham.Numero;
                            dtTrans.prix = Convert.ToDecimal(p);
                            dtTrans.Quantite = 1;
                            dtTrans.Reduction = Convert.ToDecimal(r);
                            dtTrans.Montant = (dtTrans.prix * Convert.ToDecimal(dtTrans.Quantite)) - dtTrans.Reduction;
                            dtTrans.Transactions = Trans;
                            dtTrans.Etat = "ACTIF";
                            //dtTrans.idHotel = GlobalData.HotId;
                            dtTrans.Produits = GlobalData.model.Produits.Where(c => c.Libelle == "CHAMBRE").First();

                            Trans.DetailTransactions.Add(dtTrans);
                        }
                        break;
                    case 4:

                        for (int i = 1; i <= rnNuit.Value; i++)
                        {
                            DetailTransactions dtTrans = new DetailTransactions();
                            dtTrans.Date = DateTime.Now.AddDays(i - 1);
                            dtTrans.Descriptions = "CHAMBRE " + Cham.TypeChambres.Libelle + " " + Cham.Numero;
                            dtTrans.prix = Convert.ToDecimal(p);
                            dtTrans.Quantite = 1;
                            dtTrans.Montant = dtTrans.prix * Convert.ToDecimal(dtTrans.Quantite);
                            dtTrans.Transactions = Trans;
                            dtTrans.Etat = "ACTIF";
                            //dtTrans.idHotel = GlobalData.HotId;
                            dtTrans.Produits = GlobalData.model.Produits.Where(c => c.Libelle == "CHAMBRE").First();

                            Trans.DetailTransactions.Add(dtTrans);
                        }

                        dt = Trans.DetailTransactions.FirstOrDefault();

                        dt.Reduction = Convert.ToDecimal(r);
                        dt.Montant = (dt.prix * Convert.ToDecimal(dt.Quantite)) - dt.Reduction;

                        break;
                    case 5:

                        for (int i = 1; i <= rnNuit.Value; i++)
                        {
                            DetailTransactions dtTrans = new DetailTransactions();
                            dtTrans.Date = DateTime.Now.AddDays(i - 1);
                            dtTrans.Descriptions = "CHAMBRE " + Cham.TypeChambres.Libelle + " " + Cham.Numero;
                            dtTrans.prix = Convert.ToDecimal(p);
                            dtTrans.Quantite = 1;
                            dtTrans.Montant = dtTrans.prix * Convert.ToDecimal(dtTrans.Quantite);
                            dtTrans.Transactions = Trans;
                            dtTrans.Etat = "ACTIF";
                            //dtTrans.idHotel = GlobalData.HotId;
                            dtTrans.Produits = GlobalData.model.Produits.Where(c => c.Libelle == "CHAMBRE").First();

                            Trans.DetailTransactions.Add(dtTrans);
                        }

                        dt = Trans.DetailTransactions.LastOrDefault();

                        dt.Reduction = Convert.ToDecimal(r);
                        dt.Montant = (dt.prix * Convert.ToDecimal(dt.Quantite)) - dt.Reduction;

                        break;

                    default:

                        break;
                }
            }
            else
            {
                Chambres Cham = rcbChambres.SelectedItem as Chambres;
                TypeChambres tpCham = rcbTypeChambre.SelectedItem as TypeChambres;

                if (rcbTypeReservation.SelectedIndex != -1)
                {
                    ReservationTypes resType = rcbTypeReservation.SelectedItem as ReservationTypes;

                    TypePrix tpPrix = GlobalData.model.TypePrix.Where(c => c.Etat == "ACTIF" && c.idReservationType == resType.ID).FirstOrDefault();


                    var resreserv = from res in GlobalData.model.PrixChambres
                                    where res.Etat == "ACTIF" && res.idTypePrix == tpPrix.idTypePrix && res.idTypeChambre == tpCham.ID
                                    select res;

                    if (resreserv != null && resreserv.Count() != 0)
                    {
                        PrixChambres prix = resreserv.FirstOrDefault();

                        for (int i = 1; i <= rnNuit.Value; i++)
                        {
                            DetailTransactions dtTrans = new DetailTransactions();
                            dtTrans.Date = DateTime.Now.AddDays(i - 1);
                            dtTrans.Descriptions = "CHAMBRE " + Cham.TypeChambres.Libelle + " " + Cham.Numero;
                            dtTrans.prix = Convert.ToDecimal(prix.Prix);
                            dtTrans.Quantite = 1;
                            dtTrans.Montant = dtTrans.prix * Convert.ToDecimal(dtTrans.Quantite);
                            dtTrans.Transactions = Trans;
                            dtTrans.Etat = "ACTIF";
                            //dtTrans.idHotel = GlobalData.HotId;
                            dtTrans.Produits = GlobalData.model.Produits.Where(c => c.Libelle == "CHAMBRE").First();

                            Trans.DetailTransactions.Add(dtTrans);
                        }

                    }
                }
            }

        }

        private void GetReductionAuto()
        {
            Transactions Trans = (Transactions)this.gbFacture.DataContext;
            DetailTransactions dt;

            Chambres Cham = rcbChambres.SelectedItem as Chambres;
            TypeChambres tpCham = rcbTypeChambre.SelectedItem as TypeChambres;

            if (rcbTypeReservation.SelectedIndex != -1)
            {
                ReservationTypes resType = rcbTypeReservation.SelectedItem as ReservationTypes;

                TypePrix tpPrix = GlobalData.model.TypePrix.Where(c => c.Etat == "ACTIF" && c.idReservationType == resType.ID).FirstOrDefault();


                var resreserv = from res in GlobalData.model.PrixChambres
                                where res.Etat == "ACTIF" && res.idTypePrix == tpPrix.idTypePrix && res.idTypeChambre == tpCham.ID
                                select res;

                if (resreserv != null && resreserv.Count() != 0)
                {
                    PrixChambres prix = resreserv.FirstOrDefault();
                    var resOR = from res in GlobalData.model.OperationRules
                                where res.Etat == "ACTIF" && res.idReservationTypes == resType.ID && res.idTypeChambre == tpCham.ID
                                select res;

                    if (resOR != null && resOR.Count() != 0)
                    {

                        foreach (OperationRules OpRule in resOR)
                        {

                            int nbr = (int)(rnNuit.Value / (OpRule.Quantite + 1));
                            double h = (double)rnNuit.Value;
                            double n = (double)OpRule.Quantite;
                            double r = (double)OpRule.Reductions.Valeur;
                            double p = (double)prix.Prix;
                            double pc = 0;


                            switch (OpRule.Rules.idRules)
                            {
                                case 1:
                                    int i = 1;

                                    foreach (DetailTransactions item in Trans.DetailTransactions.Where(c => c.Etat == "ACTIF" && c.Produits.Categories.Libelle == "CHAMBRES"))
                                    {
                                        if (i % (n + 1) == 0)
                                        {
                                            DetailTransactions dtTrans = item;
                                            dtTrans.ReductionAuto = Convert.ToDecimal(r);
                                            dtTrans.Montant = (dtTrans.prix * Convert.ToDecimal(dtTrans.Quantite)) - dtTrans.Reduction - dtTrans.ReductionAuto;

                                        }

                                        i++;
                                    }


                                    break;
                                case 2:

                                    int j = 1;

                                    foreach (DetailTransactions item in Trans.DetailTransactions.Where(c => c.Etat == "ACTIF" && c.Produits.Categories.Libelle == "CHAMBRES"))
                                    {
                                        if (j > n)
                                        {
                                            DetailTransactions dtTrans = item;

                                            dtTrans.ReductionAuto = Convert.ToDecimal(r);
                                            dtTrans.Montant = (dtTrans.prix * Convert.ToDecimal(dtTrans.Quantite)) - dtTrans.Reduction - dtTrans.ReductionAuto;

                                        }

                                        j++;
                                    }

                                    break;
                                case 3:
                                    foreach (DetailTransactions item in Trans.DetailTransactions.Where(c => c.Etat == "ACTIF" && c.Produits.Categories.Libelle == "CHAMBRES"))
                                    {
                                        DetailTransactions dtTrans = item;
                                        dtTrans.ReductionAuto = Convert.ToDecimal(r);
                                        dtTrans.Montant = (dtTrans.prix * Convert.ToDecimal(dtTrans.Quantite)) - dtTrans.Reduction - dtTrans.ReductionAuto;

                                    }
                                    break;
                                case 4:

                                    dt = Trans.DetailTransactions.Where(c => c.Etat == "ACTIF" && c.Produits.Categories.Libelle == "CHAMBRES").FirstOrDefault();

                                    dt.ReductionAuto = Convert.ToDecimal(r);
                                    dt.Montant = (dt.prix * Convert.ToDecimal(dt.Quantite)) - dt.Reduction - dt.ReductionAuto;

                                    break;
                                case 5:

                                    dt = Trans.DetailTransactions.Where(c => c.Etat == "ACTIF" && c.Produits.Categories.Libelle == "CHAMBRES").LastOrDefault();

                                    dt.ReductionAuto = Convert.ToDecimal(r);
                                    dt.Montant = (dt.prix * Convert.ToDecimal(dt.Quantite)) - dt.Reduction - dt.ReductionAuto;

                                    break;

                                default:

                                    break;
                            }

                        }
                    }
                }
            }

        }

        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
            Clear();
        }

        private void rcbConditionnement_DropDownOpened_1(object sender, EventArgs e)
        {

        }

        private void btnNewClient_Click(object sender, RoutedEventArgs e)
        {

            nomsRadComboBox.SelectedIndex = -1;
            txtNoms.SelectedItem = null;

            Clients client = new Clients();
            client.Noms = nomsRadComboBox.Text;
            this.gbInfoClient.DataContext = client;
            this.gbContactClient.DataContext = client;
            this.gbAutreClient.DataContext = client;

            //txtAddresse.Focus();


        }

        private void nomsRadComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (nomsRadComboBox.SelectedIndex != -1)
            {
                Clients client = nomsRadComboBox.SelectedItem as Clients;
                txtNoms.SearchText = client.Noms;
                this.gbInfoClient.DataContext = client;
                this.gbContactClient.DataContext = client;
                this.gbAutreClient.DataContext = client;
                                

                //rcbTypeChambre.Focus();
            }

        }

        private void txtNoms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (txtNoms.SelectedItem != null)
            {
                nomsRadComboBox.SelectedItem = txtNoms.SelectedItem;

                //rcbTypeChambre.Focus();
            }
        }

        private Clients autoSaveClients(string Data)
        {
            Clients loc = new Clients();

            if (nomsRadComboBox.SelectedItem == null)
            {

                loc.Noms = Data;

                GlobalData.model.Clients.Add(loc);

                return loc;

            }

            return nomsRadComboBox.SelectedItem as Clients;
        }

        private Villes autoSaveVilles(string Data)
        {
            Villes loc = new Villes();

            if (villeRadComboBox.SelectedItem == null)
            {

                loc.Libelle = Data;

                GlobalData.model.Villes.Add(loc);

                return loc;

            }

            return villeRadComboBox.SelectedItem as Villes;
        }

        private Communes autoSaveCommunes(string Data)
        {
            Communes loc = new Communes();

            if (communesRadComboBox.SelectedItem == null)
            {

                loc.Libelle = Data;

                GlobalData.model.Communes.Add(loc);

                return loc;

            }

            return communesRadComboBox.SelectedItem as Communes;
        }

        private Quartiers autoSaveQuartiers(string Data)
        {
            Quartiers loc = new Quartiers();

            if (quartierRadComboBox.SelectedItem == null)
            {

                loc.Libelle = Data;

                GlobalData.model.Quartiers.Add(loc);

                return loc;

            }

            return quartierRadComboBox.SelectedItem as Quartiers;
        }

        private Nationalités autoSaveNationalites(string Data)
        {
            Nationalités loc = new Nationalités();

            if (nationaliteRadComboBox.SelectedItem == null)
            {

                loc.Libelle = Data;

                GlobalData.model.Nationalités.Add(loc);

                return loc;

            }

            return nationaliteRadComboBox.SelectedItem as Nationalités;
        }

        private MethodePaiements autoSaveMethodePaiements(string Data)
        {
            MethodePaiements loc = new MethodePaiements();

            if (rcbModePaiement.SelectedItem == null)
            {

                loc.Libelle = Data;

                GlobalData.model.MethodePaiements.Add(loc);

                return loc;

            }

            return rcbModePaiement.SelectedItem as MethodePaiements;
        }

        private void RadButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (nomsRadComboBox.SelectedIndex != -1)
            {
                Clients client = nomsRadComboBox.SelectedItem as Clients;
                this.gbInfoClient.DataContext = client;
                this.gbContactClient.DataContext = client;
                this.gbAutreClient.DataContext = client;
            }
            else
            {
                if (nomsRadComboBox.SelectedItem == null && nomsRadComboBox.Text != null && nomsRadComboBox.Text != "")
                {

                    Clients client = autoSaveClients(nomsRadComboBox.Text);
                    this.gbInfoClient.DataContext = client;
                    this.gbContactClient.DataContext = client;
                    this.gbAutreClient.DataContext = client;
                }

            }
        }

        private void UpdateMontant()
        {
            if (rcbTypeChambre.SelectedIndex != -1)
            {
                TypeChambres tpCham = rcbTypeChambre.SelectedItem as TypeChambres;
                Transactions Trans = (Transactions)this.gbFacture.DataContext;
                //Trans.Reduction = Convert.ToDecimal(rdReduction.Value);

                if (rcbTypeReservation.SelectedIndex != -1)
                {
                    ReservationTypes resType = rcbTypeReservation.SelectedItem as ReservationTypes;

                    if (resType.ReservationType == "NUIT")
                    {

                        TypePrix tpPrix = GlobalData.model.TypePrix.Where(c => c.Etat == "ACTIF" && c.idReservationType == resType.ID).FirstOrDefault();


                        var resreserv = from res in GlobalData.model.PrixChambres
                                        where res.Etat == "ACTIF" && res.idTypePrix == tpPrix.idTypePrix && res.idTypeChambre == tpCham.ID
                                        select res;

                        if (resreserv != null && resreserv.Count() != 0)
                        {
                            PrixChambres prix = resreserv.FirstOrDefault();

                            var resOR = from res in GlobalData.model.OperationRules
                                        where res.Etat == "ACTIF" && res.idReservationTypes == resType.ID && res.idTypeChambre == tpCham.ID
                                        select res;

                            if (resOR != null && resOR.Count() != 0)
                            {

                                Trans.TotalHT = Convert.ToDecimal((double)prix.Prix * rnNuit.Value) - Trans.Reduction;

                                foreach (OperationRules OpRule in resOR)
                                {

                                    int nbr = (int)(rnNuit.Value / (OpRule.Quantite + 1));
                                    double h = (double)rnNuit.Value;
                                    double n = (double)OpRule.Quantite;
                                    double r = (double)OpRule.Reductions.Valeur;
                                    double p = (double)prix.Prix;
                                    double pc = 0;


                                    switch (OpRule.Rules.idRules)
                                    {
                                        case 1:

                                            pc = (r * (int)(h / (n + 1)));
                                            Trans.TotalHT -= Convert.ToDecimal(pc);

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

                                            Trans.TotalHT -= Convert.ToDecimal(pc);
                                            break;
                                        case 3:
                                            pc = (r * h);
                                            Trans.TotalHT -= Convert.ToDecimal(pc);
                                            break;
                                        case 4:
                                            pc = (r);
                                            Trans.TotalHT -= Convert.ToDecimal(pc);
                                            break;
                                        case 5:
                                            pc = (r);
                                            Trans.TotalHT -= Convert.ToDecimal(pc);
                                            break;

                                        default:
                                            break;
                                    }
                                }

                            }
                            else
                            {
                                Trans.TotalHT = Convert.ToDecimal((double)prix.Prix * rnNuit.Value) - Trans.Reduction;

                            }

                        }
                        else
                        {
                            return;
                        }

                        //Transaction
                        //Trans.TotalHT = (decimal)(Convert.ToDouble(tpCham.PrixNuit) * Convert.ToDouble(rnNuit.Value));

                        //Trans.TVA = (decimal)(18 * (double)0.01 * (double)Trans.TotalHT);
                        Trans.TVA = 0;

                        Trans.TotalTTC = Trans.TotalHT + Trans.TVA;

                        Trans.TotalReste = Trans.TotalTTC;

                        if (rdMontantPaye.Value > (double)Trans.TotalReste)
                        {
                            Trans.TotalPaye = 0;

                            Trans.TotalReste = Trans.TotalTTC - Convert.ToDecimal(rdMontantPaye.Value);
                        }
                        else
                        {
                            Trans.TotalPaye = Convert.ToDecimal(rdMontantPaye.Value);

                            Trans.TotalReste = Trans.TotalTTC - Convert.ToDecimal(rdMontantPaye.Value);
                        }


                        rdMontant.Value = Convert.ToDouble(Trans.TotalHT);
                        //rdReduction.Value = Convert.ToDouble(Trans.Reduction);
                        rdTVA.Value = Convert.ToDouble(Trans.TVA);
                        rdTTC.Value = Convert.ToDouble(Trans.TotalTTC);
                        rdMontantPaye.Value = Convert.ToDouble(Trans.TotalPaye);
                        rdMontantRestant.Value = Convert.ToDouble(Trans.TotalReste);

                    }
                    else
                    {
                        TypePrix tpPrix = GlobalData.model.TypePrix.Where(c => c.Etat == "ACTIF" && c.idReservationType == resType.ID).FirstOrDefault();


                        var resreserv = from res in GlobalData.model.PrixChambres
                                        where res.Etat == "ACTIF" && res.idTypePrix == tpPrix.idTypePrix && res.idTypeChambre == tpCham.ID
                                        select res;

                        if (resreserv != null && resreserv.Count() != 0)
                        {
                            PrixChambres prix = resreserv.FirstOrDefault();

                            var resOR = from res in GlobalData.model.OperationRules
                                        where res.Etat == "ACTIF" && res.idReservationTypes == resType.ID && res.idTypeChambre == tpCham.ID
                                        select res;

                            if (resOR != null && resOR.Count() != 0)
                            {

                                Trans.TotalHT = Convert.ToDecimal((double)prix.Prix * rnNuit.Value) - Trans.Reduction;

                                foreach (OperationRules OpRule in resOR)
                                {

                                    int nbr = (int)(rnNuit.Value / (OpRule.Quantite + 1));
                                    double h = (double)rnNuit.Value;
                                    double n = (double)OpRule.Quantite;
                                    double r = (double)OpRule.Reductions.Valeur;
                                    double p = (double)prix.Prix;
                                    double pc = 0;


                                    switch (OpRule.Rules.idRules)
                                    {
                                        case 1:

                                            pc = (r * (int)(h / (n + 1)));
                                            Trans.TotalHT -= Convert.ToDecimal(pc);

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

                                            Trans.TotalHT -= Convert.ToDecimal(pc);
                                            break;
                                        case 3:
                                            pc = (r * h);
                                            Trans.TotalHT -= Convert.ToDecimal(pc);
                                            break;
                                        case 4:
                                            pc = (r);
                                            Trans.TotalHT -= Convert.ToDecimal(pc);
                                            break;
                                        case 5:
                                            pc = (r);
                                            Trans.TotalHT -= Convert.ToDecimal(pc);
                                            break;

                                        default:
                                            break;
                                    }
                                }

                            }
                            else
                            {
                                Trans.TotalHT = Convert.ToDecimal((double)prix.Prix * rnNuit.Value) - Trans.Reduction;

                            }

                        }
                        else
                        {
                            return;
                        }

                        //Transaction
                        //Trans.TotalHT = (decimal)(Convert.ToDouble(tpCham.PrixNuit) * Convert.ToDouble(rnNuit.Value));

                        //Trans.TVA = (decimal)(18 * (double)0.01 * (double)Trans.TotalHT);
                        Trans.TVA = 0;

                        Trans.TotalTTC = Trans.TotalHT + Trans.TVA;

                        Trans.TotalReste = Trans.TotalTTC;

                        if (rdMontantPaye.Value > (double)Trans.TotalReste)
                        {
                            Trans.TotalPaye = 0;

                            Trans.TotalReste = Trans.TotalTTC - Convert.ToDecimal(rdMontantPaye.Value);
                        }
                        else
                        {
                            Trans.TotalPaye = Convert.ToDecimal(rdMontantPaye.Value);

                            Trans.TotalReste = Trans.TotalTTC - Convert.ToDecimal(rdMontantPaye.Value);
                        }
                        
                        rdMontant.Value =  Convert.ToDouble(Trans.TotalHT);
                        //rdReduction.Value = Convert.ToDouble(Trans.Reduction);
                        rdTVA.Value = Convert.ToDouble(Trans.TVA);
                        rdTTC.Value = Convert.ToDouble(Trans.TotalTTC);
                        rdMontantPaye.Value = Convert.ToDouble(Trans.TotalPaye);
                        rdMontantRestant.Value = Convert.ToDouble(Trans.TotalReste);


                    }

                }

            }


        }

        private void GetReduction()
        {
            Transactions Trans = (Transactions)this.gbFacture.DataContext;

            if (rcbRules.SelectedItem != null && (double)Trans.TotalHT != 0 && (double)rdReduction.Value != 0)
            {
                Rules OpRule = rcbRules.SelectedItem as Rules;
                Reductions red = rcbReductions.SelectedItem as Reductions;

                int nbr = (int)rnNuit.Value;
                double h = (double)rnNuit.Value;
                double n = (double)rdNNuit.Value;
                double r = 0;

                if (red.OpenReduction == true)
                {
                    if (ReductionType == "FIXE")
                    {
                        r = (double)rdReduction.Value;
                    }
                    else
                    {
                        rdReduction.Value = (double)Trans.TotalHT * (double)rdPourcentage.Value * 0.01;
                        r = (double)rdReduction.Value;
                    }
                }
                else
                {
                    if (ReductionType == "FIXE")
                    {
                        r = (double)red.Valeur;
                    }
                    else
                    {
                        r = (double)Trans.TotalHT * (double)red.Pourcentage * 0.01;
                    }
                }

                double pc = 0;


                switch (OpRule.idRules)
                {
                    case 1:

                        pc = (r * (int)(h / (n + 1)));

                        if (pc < (double)Trans.TotalHT)
                        {
                            Trans.Reduction = Convert.ToDecimal(pc);
                        }
                        else
                        {
                            Trans.Reduction = 0;
                            MessageBox.Show("La reduction ne peut pa etre superieure au prix total", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                        }


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

                        if (pc < (double)Trans.TotalHT)
                        {
                            Trans.Reduction = Convert.ToDecimal(pc);
                        }
                        else
                        {
                            Trans.Reduction = 0;
                            MessageBox.Show("La reduction ne peut pa etre superieure au prix total", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                        }
                        break;
                    case 3:
                        pc = (r * h);
                        if (pc < (double)Trans.TotalHT)
                        {
                            Trans.Reduction = Convert.ToDecimal(pc);
                        }
                        else
                        {
                            Trans.Reduction = 0;
                            MessageBox.Show("La reduction ne peut pa etre superieure au prix total", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                        }
                        break;
                    case 4:
                        pc = (r);
                        if (pc < (double)Trans.TotalHT)
                        {
                            Trans.Reduction = Convert.ToDecimal(pc);
                        }
                        else
                        {
                            Trans.Reduction = 0;
                            MessageBox.Show("La reduction ne peut pa etre superieure au prix total", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                        }
                        break;
                    case 5:
                        pc = (r);
                        if (pc < (double)Trans.TotalHT)
                        {
                            Trans.Reduction = Convert.ToDecimal(pc);
                        }
                        else
                        {
                            Trans.Reduction = 0;
                            MessageBox.Show("La reduction ne peut pa etre superieure au prix total", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                        }
                        break;

                    default:

                        break;
                }
            }
            else
            {
                try
                {
                    Trans.Reduction = 0;
                }
                catch (Exception)
                {
                }
                
            }

        }

        private void rcbTypeReservation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rcbTypeReservation.SelectedIndex != -1)
            {

                try
                {
                    ReservationTypes resType = rcbTypeReservation.SelectedItem as ReservationTypes;

                    if (resType.ReservationType == "NUIT")
                    {
                        dpArrival.IsEnabled = false;
                        dpArrival.SelectedTime = resType.HeureDepart;
                        dpDepart.SelectedDate = dpArrival.SelectedDate.Value.AddDays(1);
                        dpDepart.SelectedTime = resType.HeureFin;

                        if (rnNuit.Value != 1)
                        {
                            rnNuit.Value = 1;
                        }

                        rnAdult.Value = 1;
                        rdNNuit.Maximum = 1;
                        rdNNuit.Minimum = 1;

                        if (EtatClear != "CLEAR")
                        {
                            if (!isAvailable())
                            {
                                if (rnNuit.Value != 0)
                                {
                                    //rnNuit.Value = 0;
                                    MessageBox.Show("Cette chambre n'est pas disponible", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }

                            }
                        }

                        //rdReduction.Value = 0;
                        //GetReduction();
                        UpdateMontant();
                    }
                    else
                    {
                        try
                        {
                            dpArrival.IsEnabled = true;
                            //dpArrival.SelectedTime = DateTime.Now.TimeOfDay ;
                            dpDepart.SelectedDate = dpArrival.SelectedDate.Value.AddDays((int)rnNuit.Value);
                            //dpDepart.SelectedTime = resType.HeureFin;
                            dpDepart.SelectedTime = dpArrival.SelectedTime.Value.Add(TimeSpan.FromMinutes(Convert.ToDouble(((int)rnNuit.Value * resType.Heure * 60) - 1)));
                            rdNNuit.Maximum = 1.7976931348623157E+308;
                            rdNNuit.Minimum = 0;

                            if (EtatClear != "CLEAR")
                            {
                                if (!isAvailable())
                                {
                                    //rnNuit.Value = 0;
                                    MessageBox.Show("Cette chambre n'est pas disponible", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);


                                }
                            }

                            rnAdult.Value = 1;
                            //rdReduction.Value = 0;
                            //GetReduction();
                            UpdateMontant();

                        }
                        catch (Exception)
                        {

                        }
                    }
                }
                catch (Exception)
                {


                }

                //rdReduction.Value = 0;
                //GetReduction();
                UpdateMontant();


            }
        }

        private void rcbTypeChambre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rcbTypeChambre.SelectedIndex != -1)
            {
                TypeChambres tpCham = rcbTypeChambre.SelectedItem as TypeChambres;

                if (tpCham != null)
                {
                    var resSer = tpCham.Chambres.Where(c => c.Etat != "SUPPRIMER");

                    ObservableCollection<Chambres> lstChambre = new ObservableCollection<Chambres>();

                    foreach (Chambres item in resSer)
                    {
                        if (isAvailable(dpArrival.SelectedValue.Value, dpDepart.SelectedValue.Value, item))
                        {
                            if (item.EtatOperation == "LIBRE" || item.EtatOperation == "RESERVER")
                            {
                                lstChambre.Add(item);
                            }
                        }
                    }

                    rcbChambres.ItemsSource = lstChambre;
                    rnAdult.Value = 1;
                    //rdReduction.Value = 0;
                    //GetReduction();
                    UpdateMontant();

                }



            }
        }

        private void dpArrival_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //try
            //{
            //    dpDepart.SelectedDate = dpArrival.SelectedDate.Value.AddDays((int)rnNuit.Value);
            //}
            //catch (Exception)
            //{

            //}
            //UpdateMontant();
        }

        private void dpDepart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //UpdateMontant();
        }

        private void rnNuit_ValueChanged(object sender, Telerik.Windows.Controls.RadRangeBaseValueChangedEventArgs e)
        {

            if (rcbTypeReservation.SelectedIndex != -1)
            {

                try
                {
                    ReservationTypes resType = rcbTypeReservation.SelectedItem as ReservationTypes;

                    if (resType.ReservationType == "NUIT")
                    {
                        dpArrival.IsEnabled = false;
                        dpArrival.SelectedTime = resType.HeureDepart;
                        dpDepart.SelectedDate = dpArrival.SelectedDate.Value.AddDays(1);
                        dpDepart.SelectedTime = resType.HeureFin;

                        if (rnNuit.Value != 1)
                        {
                            //rnNuit.Value = 1;
                            rdNNuit.Maximum = 1;
                            rdNNuit.Minimum = 1;
                        }

                        if (!isAvailable())
                        {
                            if (rnNuit.Value != 0)
                            {
                                //rnNuit.Value = 0;
                                MessageBox.Show("Cette chambre n'est pas disponible", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }

                        }
                        //rdReduction.Value = 0;
                        //GetReduction();
                        UpdateMontant();
                    }
                    else
                    {
                        try
                        {
                            dpArrival.IsEnabled = false;
                            dpArrival.SelectedTime = DateTime.Now.TimeOfDay;
                            dpDepart.SelectedDate = dpArrival.SelectedDate.Value.AddDays((int)rnNuit.Value);
                            //dpDepart.SelectedTime = resType.HeureFin;
                            dpDepart.SelectedTime = dpArrival.SelectedTime.Value.Add(TimeSpan.FromMinutes(Convert.ToDouble(((int)rnNuit.Value * resType.Heure * 60) - 1)));
                            rdNNuit.Maximum = 1.7976931348623157E+308;
                            rdNNuit.Minimum = 0;

                            if (!isAvailable())
                            {
                                //rnNuit.Value = 0;
                                MessageBox.Show("Cette chambre n'est pas disponible", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            //rdReduction.Value = 0;
                            //GetReduction();
                            UpdateMontant();

                        }
                        catch (Exception)
                        {

                        }
                    }
                }
                catch (Exception)
                {


                }

                //rdReduction.Value = 0;
                //GetReduction();
                UpdateMontant();


            }
        }

        private void rcbChambres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (!isAvailable())
            {

            }

            try
            {
                Chambres Cham = rcbChambres.SelectedItem as Chambres;

                if (Cham.EtatOperation == "SALLE")
                {
                    MessageBox.Show("La chambre est salle veuillez la nettoyée", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                    rcbChambres.SelectedItem = null;
                    return;
                }
            }
            catch (Exception)
            {

            }

        }

        private void paysRadComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public bool isAvailable()
        {
            try
            {
                DateTime date = dpArrival.SelectedValue.Value;
                DateTime datefin = dpDepart.SelectedValue.Value;
                Chambres chambre = rcbChambres.SelectedItem as Chambres;

                if (chambre != null)
                {
                    var resreserv = from res in GlobalData.model.Reservations
                                    where res.idChambre == chambre.ID && res.Etat != "TERMINER" && (date >= res.DateArrive || datefin >= res.DateArrive) && (date <= res.DateDepart || datefin <= res.DateDepart)
                                    select res;

                    if (resreserv != null && resreserv.Count() != 0)
                    {
                        return false;
                    }

                }
            }
            catch (Exception)
            {

            }


            return true;
        }

        public bool isAvailable(DateTime datedeb, DateTime dateF, Chambres chambre)
        {

            try
            {
                DateTime date = datedeb;
                DateTime datefin = dateF;

                if (chambre != null)
                {
                    var resreserv = from res in GlobalData.model.Reservations
                                    where res.idChambre == chambre.ID && res.Etat != "TERMINER" && (date >= res.DateArrive || datefin >= res.DateArrive) && (date <= res.DateDepart || datefin <= res.DateDepart)
                                    select res;

                    if (resreserv != null && resreserv.Count() != 0)
                    {
                        return false;
                    }

                }
            }
            catch (Exception)
            {


            }

            return true;
        }

        private void rdMontantPaye_ValueChanged(object sender, Telerik.Windows.Controls.RadRangeBaseValueChangedEventArgs e)
        {
            if (rdMontantPaye.Value != null)
            {
                UpdateMontant();
            }

        }

        private void nomsRadComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                if (nomsRadComboBox.SelectedIndex != -1)
                {
                    Clients client = nomsRadComboBox.SelectedItem as Clients;
                    txtNoms.SelectedItem = nomsRadComboBox.SelectedItem;
                    this.gbInfoClient.DataContext = client;
                    this.gbContactClient.DataContext = client;
                    this.gbAutreClient.DataContext = client;

                    rcbTypeChambre.Focus();
                }
                //else
                //{
                //    if (nomsRadComboBox.SelectedItem == null && nomsRadComboBox.Text != null && nomsRadComboBox.Text != "")
                //    {

                //        Clients client = autoSaveClients(nomsRadComboBox.Text);
                //        txtNoms.SearchText = nomsRadComboBox.Text;
                //        this.gbInfoClient.DataContext = client;
                //        this.gbContactClient.DataContext = client;
                //        this.gbAutreClient.DataContext = client;

                //        txtAddresse.Focus();

                //    }

                //}
            }
        }

        private void txtNoms_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                if (txtNoms.SelectedItem != null)
                {
                    Clients client = nomsRadComboBox.SelectedItem as Clients;
                    this.gbInfoClient.DataContext = client;
                    this.gbContactClient.DataContext = client;
                    this.gbAutreClient.DataContext = client;

                    rcbTypeChambre.Focus();
                }
                else
                {
                    if (txtNoms.SelectedItem == null && nomsRadComboBox.SelectedItem == null && txtNoms.SearchText != null && txtNoms.SearchText != "")
                    {

                        Clients client = autoSaveClients(txtNoms.SearchText);
                        this.gbInfoClient.DataContext = client;
                        this.gbContactClient.DataContext = client;
                        this.gbAutreClient.DataContext = client;

                        txtPrenoms.Focus();

                    }

                }
            }
        }

        private void txtPrenoms_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                paysRadComboBox.Focus();
            }
        }

        private void paysRadComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                villeRadComboBox.Focus();
            }
        }

        private void villeRadComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                communesRadComboBox.Focus();
            }
        }

        private void communesRadComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                quartierRadComboBox.Focus();
            }
        }

        private void quartierRadComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtAddresse.Focus();
            }
        }


        private void txtAddresse_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txttelephone.Focus();
            }
        }

        private void txttelephone_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtCellulaire.Focus();
            }
        }

        private void txtCellulaire_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtFax.Focus();
            }
        }

        private void txtFax_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtEmail.Focus();
            }
        }

        private void txtEmail_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                iDTypesRadComboBox.Focus();
            }
        }

        private void iDTypesRadComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtIdNumber.Focus();
            }
        }

        private void txtIdNumber_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                nationaliteRadComboBox.Focus();
            }
        }

        private void nationaliteRadComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                rdpDateNaissance.Focus();
            }
        }

        private void rdpDateNaissance_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtLieuNaissance.Focus();
            }
        }

        private void txtLieuNaissance_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                rcbTypeChambre.Focus();
            }
        }

        private void iDTypesRadComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void RadDatePicker_KeyUp_1(object sender, KeyEventArgs e)
        {

        }

        private void rdMontant_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                rcbModePaiement.SelectedIndex = 0;
                rdMontantPaye.Focus();
            }
        }

        private void rcbModePaiement_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (rcbModePaiement.SelectedItem != null)
                {
                    rdMontantPaye.Focus();
                }
            }
        }

        private void rdMontantPaye_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnValider.Focus();
            }
        }

        private void rdMontantRestant_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnValider.Focus();
            }
        }

        private void RadButton_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                nomsRadComboBox.Focus();
            }
        }

        private void rcbTypeChambre_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (rcbTypeChambre.SelectedItem != null)
                {
                    rcbChambres.SelectedIndex = 0;
                    rcbChambres.Focus();

                }
            }
        }

        private void rcbChambres_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (rcbChambres.SelectedItem != null)
                {
                    rcbTypeReservation.Focus();
                }

            }
        }

        private void rcbTypeReservation_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (rcbTypeReservation.SelectedItem != null)
                {
                    ReservationTypes resType = rcbTypeReservation.SelectedItem as ReservationTypes;

                    if (resType.ReservationType == "NUIT")
                    {
                        rnNuit.IsEnabled = false;
                        dpArrival.SelectedDate = DateTime.Now;
                        rnAdult.Focus();
                    }
                    else
                    {
                        rnNuit.IsEnabled = true;
                        dpArrival.SelectedDate = DateTime.Now;
                        rnNuit.Focus();
                    }


                }
            }
        }

        private void rnNuit_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (rnNuit.Value != 0)
                {
                    rnAdult.Focus();
                }

            }
        }

        private void dpArrival_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!isAvailable())
                {
                    rnNuit.Focus();
                    MessageBox.Show("Cette chambre n'est pas disponible", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
        }

        private void dpDepart_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Enter)
            //{
            //    rcbTypeChambre.Focus();
            //}
        }

        private void RadNumericUpDown_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                rnEnfant.Focus();
            }
        }

        private void RadNumericUpDown_KeyUp_2(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                rdMontant.Focus();
            }
        }

        private void rcbRules_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (rcbRules.SelectedItem != null)
                {
                    rcbReductions.Focus();
                }
            }
        }

        private void rcbReductions_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (rcbReductions.SelectedItem != null)
                {
                    rdReduction.Focus();
                }
            }
        }

        private void rdRemise_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (rcbReductions.SelectedItem != null)
                {
                    btnValider.Focus();
                }
            }

        }

        private void rdNbreNuit_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (rdNNuit.Value != 0)
                {
                    rdReduction.Focus();
                }
            }
        }

        private void rdReduction_ValueChanged(object sender, Telerik.Windows.Controls.RadRangeBaseValueChangedEventArgs e)
        {
            GetReduction();
            UpdateMontant();
        }

        private void rcbRules_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rcbRules.SelectedItem != null)
            {
                Rules rule = rcbRules.SelectedItem as Rules;

                switch (rule.idRules)
                {
                    case 1:
                        lblNbreNuit.Visibility = System.Windows.Visibility.Visible;
                        rdNNuit.Visibility = System.Windows.Visibility.Visible;
                        break;
                    case 2:
                        lblNbreNuit.Visibility = System.Windows.Visibility.Visible;
                        rdNNuit.Visibility = System.Windows.Visibility.Visible;
                        break;
                    case 3:
                        lblNbreNuit.Visibility = System.Windows.Visibility.Collapsed;
                        rdNNuit.Visibility = System.Windows.Visibility.Collapsed;
                        rdNNuit.Value = 1;
                        break;
                    case 4:
                        lblNbreNuit.Visibility = System.Windows.Visibility.Collapsed;
                        rdNNuit.Visibility = System.Windows.Visibility.Collapsed;
                        rdNNuit.Value = 1;
                        break;
                    case 5:
                        lblNbreNuit.Visibility = System.Windows.Visibility.Collapsed;
                        rdNNuit.Visibility = System.Windows.Visibility.Collapsed;
                        break;

                    default:
                        break;
                }
            }

            GetReduction();
            UpdateMontant();


        }

        private void rcbReductions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rcbReductions.SelectedIndex != -1)
            {
                Reductions red = rcbReductions.SelectedItem as Reductions;

                if (red.Type == 1)
                {
                    ReductionType = "FIXE";

                    if (red.OpenReduction == true)
                    {
                        lblPourcentage.Visibility = System.Windows.Visibility.Collapsed;
                        rdPourcentage.Visibility = System.Windows.Visibility.Collapsed;
                        lblValeur.Visibility = System.Windows.Visibility.Visible;
                        rdReduction.Visibility = System.Windows.Visibility.Visible;

                        //lblValeur.IsEnabled = true;
                        rdReduction.IsEnabled = true;
                    }
                    else
                    {
                        lblPourcentage.Visibility = System.Windows.Visibility.Collapsed;
                        rdPourcentage.Visibility = System.Windows.Visibility.Collapsed;
                        lblValeur.Visibility = System.Windows.Visibility.Collapsed;
                        rdReduction.Visibility = System.Windows.Visibility.Collapsed;
                    }



                }
                else
                {
                    ReductionType = "POURCENTAGE";

                    if (red.OpenReduction == true)
                    {
                        lblPourcentage.Visibility = System.Windows.Visibility.Visible;
                        rdPourcentage.Visibility = System.Windows.Visibility.Visible;
                        lblValeur.Visibility = System.Windows.Visibility.Visible;
                        rdReduction.Visibility = System.Windows.Visibility.Visible;

                        //lblValeur.IsEnabled = false;
                        rdReduction.IsEnabled = false;
                    }
                    else
                    {
                        lblPourcentage.Visibility = System.Windows.Visibility.Collapsed;
                        rdPourcentage.Visibility = System.Windows.Visibility.Collapsed;
                        lblValeur.Visibility = System.Windows.Visibility.Collapsed;
                        rdReduction.Visibility = System.Windows.Visibility.Collapsed;
                    }


                }
            }

            rdReduction.Value = 0;
            GetReduction();
            UpdateMontant();
        }

        private void rdPourcentage_ValueChanged_1(object sender, Telerik.Windows.Controls.RadRangeBaseValueChangedEventArgs e)
        {
            GetReduction();
            UpdateMontant();
        }

        private void rdNbreNuit_ValueChanged_1(object sender, Telerik.Windows.Controls.RadRangeBaseValueChangedEventArgs e)
        {
            GetReduction();
            UpdateMontant();
        }

        private void UserControl_Unloaded_1(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void chkConfirme_Checked(object sender, RoutedEventArgs e)
        {
            dpArrival.SelectedDate = DateTime.Now.Date;
        }

        private void chkReservation_Checked_1(object sender, RoutedEventArgs e)
        {

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
