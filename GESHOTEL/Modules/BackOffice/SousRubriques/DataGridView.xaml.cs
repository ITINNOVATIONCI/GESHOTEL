﻿using GESHOTEL.Models;
using GESHOTEL.Sous_RubriquesModules.ViewModels;
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

namespace GESHOTEL.Sous_RubriquesModules
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DataGridView : UserControl
    {
        Sous_RubriquesViewModel viewM;
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
                        if (GlobalData.VerificationDroit("CanEditSous_Rubriques"))
                        {
                            viewM = this.Main.DataContext as Sous_RubriquesViewModel;

                            InsertData view = new InsertData("MOD", viewM.SelectedData, viewM);
                            view.ShowDialog();

                            if (view.Msg == "OK")
                            {
                                MessageBox.Show("Opération effectuée avec succès", "Sous_Rubriques", MessageBoxButton.OK, MessageBoxImage.Information);
                                viewM.Refresh();
                            }
                            else if (view.Msg == "Error")
                            {
                                MessageBox.Show("    Echec Opération    ", "Sous_Rubriques", MessageBoxButton.OK, MessageBoxImage.Warning);
                                viewM.Refresh();
                            }
                            else
                            {
                                viewM.Refresh();
                            }
                        }


                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message + "Veuillez fermer le formulaire puis recommencer.", "CATEGORIE", MessageBoxButton.OK, MessageBoxImage.Warning);

                    }
                }

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
            viewM = this.Main.DataContext as Sous_RubriquesViewModel;
            viewM.Refresh();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
          
            try
            {
               viewM = this.Main.DataContext as Sous_RubriquesViewModel;
                viewM.SelectedData = new Sous_Rubriques();
                InsertData view = new InsertData("AJOUT", viewM.SelectedData, viewM);
                view.ShowDialog();

                if (view.Msg == "OK")
                {

                    MessageBox.Show("Opération effectuée avec succès", "Sous_Rubriques", MessageBoxButton.OK, MessageBoxImage.Information);
                    viewM.Refresh();

                }
                else if (view.Msg == "Error")
                {
                    MessageBox.Show("   Echec Opération    ", "Sous_Rubriques ", MessageBoxButton.OK, MessageBoxImage.Warning);
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

                MessageBox.Show(ex.Message, "Sous_Rubriques", MessageBoxButton.OK, MessageBoxImage.Warning);

            }

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                try
                {
                    //if (GlobalData.VerificationDroit("CanAddSous_Rubriques"))
                    //{
                    viewM = this.Main.DataContext as Sous_RubriquesViewModel;
                    InsertData view = new InsertData("MOD", viewM.SelectedData, viewM);
                    view.ShowDialog();

                    if (view.Msg == "OK")
                    {
                        MessageBox.Show("Opération effectuée avec succès", "Sous_Rubriques", MessageBoxButton.OK, MessageBoxImage.Information);
                        viewM.Refresh();
                    }
                    else if (view.Msg == "Error")
                    {
                        MessageBox.Show("    Echec Opération    ", "Sous_Rubriques", MessageBoxButton.OK, MessageBoxImage.Warning);
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

                    MessageBox.Show(ex.Message, "Sous_Rubriques", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
            }
            else
            {
                MessageBox.Show("Aucune ligne selectionnée dans la liste", "Sous_Rubriques", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            //if (GlobalData.VerificationDroit("CanAddSous_Rubriques"))
            //{

            var result = MessageBox.Show("Voulez vous vraiment supprimer ?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {

                if (dataGrid.SelectedItem != null)
                {
                    try
                    {
                        viewM = this.Main.DataContext as Sous_RubriquesViewModel;
                        Sous_Rubriques ent = dataGrid.SelectedItem as Sous_Rubriques;
                        ent.Etat = "SUPPRIMER";

                        viewM.model.SaveChanges();

                        viewM.Refresh();

                        MessageBox.Show("Opération effectuée avec succès", "Sous_Rubriques", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message, "Sous_Rubriques", MessageBoxButton.OK, MessageBoxImage.Warning);
                        viewM.Refresh();

                    }
                }
                else
                {
                    MessageBox.Show("Aucune ligne selectionnée dans la liste", "Sous_Rubriques", MessageBoxButton.OK, MessageBoxImage.Warning);

                }

            }
            //}

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (GlobalData.VerificationDroit("CanAddSous_Rubriques"))
            {
                btnAdd.Visibility = System.Windows.Visibility.Visible;
            }

            if (GlobalData.VerificationDroit("CanEditSous_Rubriques"))
            {
                btnEdit.Visibility = System.Windows.Visibility.Visible;
            }

            if (GlobalData.VerificationDroit("CanDeleteSous_Rubriques"))
            {
                btnDelete.Visibility = System.Windows.Visibility.Visible;
            }

        }

    }
}
