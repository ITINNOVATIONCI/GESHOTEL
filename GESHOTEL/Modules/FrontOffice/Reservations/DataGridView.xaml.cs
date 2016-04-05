using GESHOTEL.FrontOffice;
using GESHOTEL.Models;
using GESHOTEL.ReservationsModules.ViewModels;
using GESHOTEL.UtilisateursModules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using Telerik.Windows.Controls.GridView;

namespace GESHOTEL.ReservationsModules
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DataGridView : UserControl
    {
        ReservationsViewModel viewM;
        public DataGridView()
        {
            InitializeComponent();

            this.dataGrid.MouseDoubleClick += this.OnGridMouseDoubleClick;

        }

        //Evenement double clic du RadGridview
        void OnGridMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement originalSender = e.OriginalSource as FrameworkElement;
            if (originalSender != null)
            {
                //var cell = originalSender.ParentOfType<GridViewCell>();
                //if (cell != null)
                //{
                //    MessageBox.Show("The double-clicked cell is " + cell.Value);
                //}

                var row = originalSender.ParentOfType<GridViewRow>();
                if (row != null)
                {
                    try
                    {
                        //if (GlobalData.VerificationDroit("CanEditReservations"))
                        //{
                        viewM = this.Main.DataContext as ReservationsViewModel;

                        RadDocumentPane rad = new RadDocumentPane();
                        rad.Content = new GESHOTEL.FrontOffice.TransactionEdit(viewM.SelectedData.ID, "Modification de Sejour " + viewM.SelectedData.ID.ToString());
                        rad.Header = "Modification de Sejour " + viewM.SelectedData.ID.ToString();
                        GlobalData.rrvMain.Title = "Modification de Sejour " + viewM.SelectedData.ID.ToString();
                        rad.FontFamily = new FontFamily("Perpetua Titling MT");
                        rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                        rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

                        if (!GlobalData.VerifyDock("Modification de Sejour " + viewM.SelectedData.ID.ToString()))
                        {
                            GlobalData.PaneGroup.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
                        }
                        else
                        {

                        }
                        //}


                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message + "Veuillez fermer le formulaire puis recommencer.", "CATEGORIE", MessageBoxButton.OK, MessageBoxImage.Warning);

                    }
                }

            }

        }

        public void BeginModifierReservation(Reservations Res)
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
                        rad.Content = new GESHOTEL.FrontOffice.TransactionEdit(Res.ID, "Modification de Sejour " + Res.ID.ToString());
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
                        rad.Content = new GESHOTEL.FrontOffice.TransactionEdit(Res.ID, "Modification de Sejour " + Res.ID.ToString());
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

        private void dataform_EditEnded(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndedEventArgs e)
        {
            //switch (e.EditAction)
            //{
            //    case Telerik.Windows.Controls.Data.DataForm.EditAction.Cancel:
            //        viewM = this.Main.DataContext as ModeReglementsViewModel;
            //        viewM.CancelChanged();
            //        (sender as RadDataForm).ValidationSummary.Errors.Clear();
            //        break;
            //    case Telerik.Windows.Controls.Data.DataForm.EditAction.Commit:
            //        viewM = this.Main.DataContext as ModeReglementsViewModel;
            //        var data = (sender as RadDataForm).CurrentItem as ModeReglements;
            //        //var modelContainsEntity = vehiVM.model.ModeReglements.Where(c => c.idModeReglements == data.idModeReglements).FirstOrDefault();
            //        if (data.idModeReglements == 0)
            //        {
            //            viewM.model.ModeReglements.Add(data);
            //        }

            //        viewM.model.SaveChangesAsync();
            //        break;
            //    default:
            //        throw new InvalidOperationException("Edit action should be Cancel or Commit only.");
            //}
        }

        private void dataform_CurrentItemChanged(object sender, EventArgs e)
        {
            //(sender as RadDataForm).CancelEdit();
            //(sender as RadDataForm).ValidationSummary.Errors.Clear();

            //viewM = this.Main.DataContext as UtilisateursViewModel;
            //viewM.CancelChanged();

        }

        private void dataform_AddedNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddedNewItemEventArgs e)
        {


        }


        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            viewM = this.Main.DataContext as ReservationsViewModel;
            viewM.Refresh();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                viewM = this.Main.DataContext as ReservationsViewModel;
                viewM.SelectedData = new Reservations();
                InsertData view = new InsertData("AJOUT", viewM.SelectedData, viewM);
                view.ShowDialog();

                if (view.Msg == "OK")
                {

                    MessageBox.Show("Opération effectuée avec succès", "Reservations", MessageBoxButton.OK, MessageBoxImage.Information);
                    viewM.Refresh();

                }
                else if (view.Msg == "Error")
                {
                    MessageBox.Show("   Echec Opération    ", "Reservations ", MessageBoxButton.OK, MessageBoxImage.Warning);
                    viewM.Refresh();

                }
                else
                {
                    viewM.Refresh();
                }
                //}


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Reservations", MessageBoxButton.OK, MessageBoxImage.Warning);

            }

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                try
                {
                    //if (GlobalData.VerificationDroit("CanAddReservations"))
                    //{
                    viewM = this.Main.DataContext as ReservationsViewModel;
                    InsertData view = new InsertData("MOD", viewM.SelectedData, viewM);
                    view.ShowDialog();

                    if (view.Msg == "OK")
                    {
                        MessageBox.Show("Opération effectuée avec succès", "Reservations", MessageBoxButton.OK, MessageBoxImage.Information);
                        viewM.Refresh();
                    }
                    else if (view.Msg == "Error")
                    {
                        MessageBox.Show("    Echec Opération    ", "Reservations", MessageBoxButton.OK, MessageBoxImage.Warning);
                        viewM.Refresh();
                    }
                    else
                    {
                        viewM.Refresh();
                    }

                    //}
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Reservations", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
            }
            else
            {
                MessageBox.Show("Aucune ligne selectionnée dans la liste", "Reservations", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            //if (GlobalData.VerificationDroit("CanAddReservations"))
            //{

            var result = MessageBox.Show("Voulez vous vraiment supprimer ?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {

                if (dataGrid.SelectedItem != null)
                {
                    try
                    {
                        viewM = this.Main.DataContext as ReservationsViewModel;
                        Reservations ent = dataGrid.SelectedItem as Reservations;
                        ent.Etat = "SUPPRIMER";

                        viewM.model.SaveChanges();

                        viewM.Refresh();

                        MessageBox.Show("Opération effectuée avec succès", "Reservations", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message, "Reservations", MessageBoxButton.OK, MessageBoxImage.Warning);
                        viewM.Refresh();

                    }
                }
                else
                {
                    MessageBox.Show("Aucune ligne selectionnée dans la liste", "Reservations", MessageBoxButton.OK, MessageBoxImage.Warning);

                }

            }
            //}

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (GlobalData.VerificationDroit("CanAddReservations"))
            {
                btnAdd.Visibility = System.Windows.Visibility.Visible;
            }

            if (GlobalData.VerificationDroit("CanEditReservations"))
            {
                btnEdit.Visibility = System.Windows.Visibility.Visible;
            }

            if (GlobalData.VerificationDroit("CanDeleteReservations"))
            {
                btnDelete.Visibility = System.Windows.Visibility.Visible;
            }

        }

        private void btnChambre_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                viewM = this.Main.DataContext as ReservationsViewModel;

                AssigneChambre view = new AssigneChambre(dataGrid.SelectedItem as Reservations, viewM.model, "Autre");
                view.ShowDialog();

                if (view.Msg == "OK")
                {
                    MessageBox.Show("Opération effectuée avec succès", "Clients", MessageBoxButton.OK, MessageBoxImage.Information);
                    viewM.Refresh();
                }
                else if (view.Msg == "Error")
                {
                    MessageBox.Show("    Echec Opération    ", "Clients", MessageBoxButton.OK, MessageBoxImage.Warning);
                    viewM.Refresh();
                }
                else
                {
                    viewM.Refresh();
                }
            }
        }

        private void btnClient_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                viewM = this.Main.DataContext as ReservationsViewModel;
                ClientsViewModel cli = new ClientsViewModel(viewM.model);
                cli.SelectedData = (dataGrid.SelectedItem as Reservations).Clients;

                ClientInsertData view = new ClientInsertData("MOD", cli.SelectedData, cli);
                view.ShowDialog();

                if (view.Msg == "OK")
                {
                    MessageBox.Show("Opération effectuée avec succès", "Clients", MessageBoxButton.OK, MessageBoxImage.Information);
                    viewM.Refresh();
                }
                else if (view.Msg == "Error")
                {
                    MessageBox.Show("    Echec Opération    ", "Clients", MessageBoxButton.OK, MessageBoxImage.Warning);
                    viewM.Refresh();
                }
                else
                {
                    viewM.Refresh();
                }
            }

        }
    }
}
