using GESHOTEL.Models;
using GESHOTEL.ReservationsModules.ViewModels;
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

namespace GESHOTEL.ReservationsModules
{
    /// <summary>
    /// Interaction logic for InsertData.xaml
    /// </summary>
    public partial class ClientInsertData : Window
    {
        string Etat = "";
        ClientsViewModel viewVM;
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

        public ClientInsertData(string etat, Clients ele, ClientsViewModel view)
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

                if (ele.Sexe != null)
                {
                    if (ele.Sexe == "M")
                    {
                        rdMasc.IsChecked = true;
                    }
                    else if (ele.Sexe == "F")
                    {
                        rdFem.IsChecked = true;
                    }
                }

            }
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClientsViewModel vm = this.DataContext as ClientsViewModel;
                Clients ent = vm.SelectedData;

                if (ent.Noms == null || ent.Noms.Trim() == "")
                {
                    lblMessageError.Text = "Remplir le champ Nom avant de continué";
                    lblMessageError.Visibility = System.Windows.Visibility.Visible;

                    return;
                }

                if (Etat == "AJOUT")
                {
                    try
                    {
                        if (rdMasc.IsChecked == true)
                        {
                            ent.Sexe = "M";
                        }
                        else if (rdFem.IsChecked == true)
                        {
                            ent.Sexe = "F";
                        }

                        ent.Etat = "ACTIF";
                        ent.idHotel = 1;
                        viewVM.model.Clients.Add(ent);
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

                        if (rdMasc.IsChecked == true)
                        {
                            ent.Sexe = "M";
                        }
                        else if (rdFem.IsChecked == true)
                        {
                            ent.Sexe = "F";
                        }

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
            ClientsViewModel vehi = this.DataContext as ClientsViewModel;
            vehi.SelectedData = null;

            this.Close();
        }

    }
}
