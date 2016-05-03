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
    /// Interaction logic for AssigneChambre.xaml
    /// </summary>
    public partial class AssigneChambre : Window
    {
        public GESHOTELEntities model;
        Reservations Res;
        string etat;

        string msg;

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

        public AssigneChambre(Reservations res, GESHOTELEntities Model, string Etat)
        {
            model = Model;
            Res = res;
            etat = Etat;

            InitializeComponent();


        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void Load()
        {

            TypeChambres tpCham = Res.TypeChambres;

            if (tpCham != null)
            {
                var resSer = tpCham.Chambres.Where(c => c.Etat != "SUPPRIMER");

                ObservableCollection<Chambres> lstChambre = new ObservableCollection<Chambres>();

                foreach (Chambres item in resSer)
                {
                    if (isAvailable(Res.DateArrive.Value, Res.DateDepart.Value, item))
                    {
                        if (item.EtatOperation == "LIBRE" || item.EtatOperation == "RESERVER")
                        {
                            lstChambre.Add(item);
                        }
                    }
                }

                rcbChambres.ItemsSource = lstChambre;

            }


        }

        public bool isAvailable(DateTime datedeb, DateTime dateF, Chambres chambre)
        {

            try
            {
                DateTime date = datedeb;
                DateTime datefin = dateF;

                if (chambre != null)
                {
                    var resreserv = from res in model.Reservations
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

        private void rcbChambres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rcbChambres.SelectedItem != null)
            {
                Chambres Cham = rcbChambres.SelectedItem as Chambres;

                chambreAutoCompleteBox.SelectedItem = rcbChambres.SelectedItem as Chambres;

            }

        }

        private void chambreAutoCompleteBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void chambreAutoCompleteBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                if (chambreAutoCompleteBox.SelectedItem != null)
                {
                    Chambres Cham = chambreAutoCompleteBox.SelectedItem as Chambres;
                    rcbChambres.SelectedItem = chambreAutoCompleteBox.SelectedItem as Chambres;

                }

            }
        }

        private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            Chambres Cham = rcbChambres.SelectedItem as Chambres;

            if (rcbChambres.SelectedIndex == -1) { MessageBox.Show("Choisissez une chambre svp", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning); return; }

            //Cham.EtatOperation = Res.Chambres.EtatOperation;
            //Res.Chambres.EtatOperation = "LIBRE";

            Res.Chambres = Cham;
            //Cham.EtatOperation = "OCCUPER";

            try
            {


                var result = MessageBox.Show("Voulez vous enregistrer cette arrivée?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (Res.TotalReste == 0)
                    {
                        if (isAvailable(Res.DateArrive.Value, Res.DateDepart.Value, Res.Chambres))
                        {
                            if (Res.Chambres.EtatOperation != "LIBRE" || Res.Chambres.EtatOperation != "RESERVER")
                            {
                                Res.Etat = "ACTIF";
                                Res.EtatOperation = "ARRIVEE";
                                Res.Chambres.EtatOperation = "OCCUPER";

                                Res.DateCheckIn = DateTime.Now;
                                Res.isCheckIn = true;

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
                        //MessageBox.Show("Le Sejour de la chambre " + Res.Chambres.Numero + " n’a pas encore été soldé ", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    }

                }

            }
            catch (Exception)
            {

            }

            if (etat != "Modifier")
            {

                model.SaveChanges();
                Msg = "OK";
                MessageBox.Show("Operation terminée", "Message", MessageBoxButton.OK, MessageBoxImage.None);
            }
            this.Close();

        }

        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void BeginModifierReservation(object param)
        {

            try
            {
                if (Res != null)
                {
                    if (Res.ReservationTypes.ReservationType == "PASSAGE")
                    {
                        PassageWin view = new PassageWin(Res.ID);
                        view.ShowDialog();
                    }
                    else if (Res.ReservationTypes.ReservationType == "NUIT")
                    {
                        RadDocumentPane rad = new RadDocumentPane();
                        rad.Content = new GESHOTEL.ReservationsModules.TransactionEdit(Res.ID, "Modification de Sejour " + Res.ID.ToString());
                        rad.Header = "Modification de Sejour " + Res.ID.ToString();
                        GlobalData.rrvMain.Title = "Modification de Sejour " + Res.ID.ToString();
                        rad.FontFamily = new FontFamily("Perpetua Titling MT");
                        rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                        rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

                        if (!GlobalData.VerifyDock("Modification de Sejour " + Res.ID.ToString()))
                        {
                            GlobalData.PaneGroup.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
                        }
                        else
                        {

                        }
                    }
                    else if (Res.ReservationTypes.ReservationType == "SEJOUR")
                    {
                        RadDocumentPane rad = new RadDocumentPane();
                        rad.Content = new GESHOTEL.ReservationsModules.TransactionEdit(Res.ID, "Modification de Sejour " + Res.ID.ToString());
                        rad.Header = "Modification de Sejour " + Res.ID.ToString();
                        GlobalData.rrvMain.Title = "Modification de Sejour " + Res.ID.ToString();
                        rad.FontFamily = new FontFamily("Perpetua Titling MT");
                        rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                        rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

                        if (!GlobalData.VerifyDock("Modification de Sejour " + Res.ID.ToString()))
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
    }
}
