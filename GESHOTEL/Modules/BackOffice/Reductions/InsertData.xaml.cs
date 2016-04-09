
using GESHOTEL.Models;
using GESHOTEL.ReductionsModules.ViewModels;
using System;
using System.Collections.Generic;
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

namespace GESHOTEL.ReductionsModules
{
    /// <summary>
    /// Interaction logic for InsertData.xaml
    /// </summary>
    public partial class InsertData : Window
    {
        string Etat = "";
        ReductionsViewModel viewVM;
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

        public InsertData(string etat, Reductions ele, ReductionsViewModel view)
        {
            InitializeComponent();

            this.DataContext = viewVM = view;

            Etat = etat;

            if (etat == "AJOUT")
            {
                this.Title = "Enregistrement d'un Quartier";
            }
            else
            {
                this.Title = "Modification d'un Quartier";
            }
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ReductionsViewModel vm = this.DataContext as ReductionsViewModel;
                Reductions ent = vm.SelectedData;

                if (Etat == "AJOUT")
                {
                    try
                    {
                        if (prct.IsChecked==true)
                        {
                            ent.Type = 2;
                        }
                        else
                        {
                            ent.Type = 1;
                        }

                        if (open.IsChecked == true)
                        {
                            ent.OpenReduction = true;
                        }
                        else
                        {
                            ent.OpenReduction = false;
                        }

                        ent.Etat = "ACTIF";
                        ent.idHotel = 1;
                        viewVM.model.Reductions.Add(ent);
                        viewVM.model.SaveChanges();
                         Msg = "OK";
                        this.Close();

                    }
                    catch (Exception ex)
                    {

                        Msg = "Error";
                        ErrorMsg = ex.Message;

                    }
                }
                else
                {
                    try
                    {

                        viewVM.model.SaveChanges();

                        Msg = "OK";
                        this.Close();

                    }
                    catch (Exception ex)
                    {

                        Msg = "Error";
                        ErrorMsg = ex.Message;

                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            ReductionsViewModel vehi = this.DataContext as ReductionsViewModel;
            vehi.SelectedData = null;

            this.Close();
        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
            if (open.IsChecked == true)
            {
                lval.Visibility = Visibility.Collapsed;
                Valeur.Visibility = Visibility.Collapsed;
            }
            if (open.IsChecked == false)
            {
                lval.Visibility = Visibility.Visible;
                Valeur.Visibility = Visibility.Visible;
            }


            else
            {

            }
        }

        private void open_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
