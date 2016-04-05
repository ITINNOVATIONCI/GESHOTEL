using GESHOTEL.UtilisateursModules.ViewModels;
using GESHOTEL.Models;
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
using Decoder;

namespace GESHOTEL.UtilisateursModules
{
    /// <summary>
    /// Interaction logic for InsertData.xaml
    /// </summary>
    public partial class InsertData : Window
    {
        Utilisateurs uti;
        string Etat = "";
        PswDecoder pswDeco = new PswDecoder();
        UtilisateursViewModel viewVM;
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

        public InsertData(string etat, Utilisateurs ele, UtilisateursViewModel view)
        {
            InitializeComponent();

            this.DataContext = viewVM = view;

            Etat = etat;

            if (etat == "AJOUT")
            {
                this.Title = "Enregistrement de Utilisateurs";
            }
            else
            {
                this.Title = "Modification de Utilisateurs";
            }
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Utilisateurs ent = ((UtilisateursViewModel)this.DataContext).SelectedData;
                if (ent.Nom == null || ent.Nom.Trim() == "")
                {
                    lblMessageError.Text = "Remplir le champ Nom avant de continuer";
                    lblMessageError.Visibility = System.Windows.Visibility.Visible;

                    return;
                }

                if (ent.Prenoms == null || ent.Prenoms.Trim() == "")
                {
                    lblMessageError.Text = "Remplir le champ Prenoms avant de continuer";
                    lblMessageError.Visibility = System.Windows.Visibility.Visible;

                    return;
                }

                if (ent.Login == null || ent.Login.Trim() == "")
                {
                    lblMessageError.Text = "Remplir le champ Login avant de continuer";
                    lblMessageError.Visibility = System.Windows.Visibility.Visible;

                    return;
                }

                if (plain.Text == null || plain.Text.Trim() == "")
                {
                    lblMessageError.Text = "Remplir le champ Password avant de continuer";
                    lblMessageError.Visibility = System.Windows.Visibility.Visible;

                    return;
                }

                if (Etat == "AJOUT")
                {
                    try
                    {

                        ent.Etat = "ACTIF";

                        //Parametres param = model.Parametres.Where(c => c.idParametre == 1).First();

                        //ent.CodeEntrepot = GenerateFacture(param.ArtID.ToString(), 4);

                        //param.ArtID++;

                        viewVM.model.Utilisateurs.Add(ent);
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
            UtilisateursViewModel vehi = this.DataContext as UtilisateursViewModel;
            vehi.SelectedData = null;

            this.Close();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        private void chkPassword_Checked(object sender, RoutedEventArgs e)
        {
            pssBox.IsEnabled = chkPassword.IsChecked.Value;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Etat != "AJOUT")
            {
                chkPassword.Visibility = System.Windows.Visibility.Visible;
                chkPassword.IsEnabled = true;
                pssBox.IsEnabled = false;

                UtilisateursViewModel vehi = this.DataContext as UtilisateursViewModel;
                plain.Text = pswDeco.DecrypterPWD(vehi.SelectedData.Password);
            }
        }

        private void chkPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            pssBox.IsEnabled = chkPassword.IsChecked.Value;
        }

        private void btnModules_Click_1(object sender, RoutedEventArgs e)
        {
            UtilisateursViewModel vehi = this.DataContext as UtilisateursViewModel;

            if (Etat == "AJOUT")
            {

                UtlisateurModuleView modVew = new UtlisateurModuleView(vehi.SelectedData, vehi, "AJOUT");
                modVew.ShowDialog();

            }
            else
            {
                UtlisateurModuleView modVew = new UtlisateurModuleView(vehi.SelectedData, vehi, "MOD");
                modVew.ShowDialog();
            }
        }

    }
}
