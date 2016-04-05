using Decoder;
using GESHOTEL.COMMON;
using GESHOTEL.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
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

namespace GESHOTEL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {
        GESHOTELEntities model;
        List<Visual> collection = new List<Visual>();

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

        public LoginControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {

            //GlobalData.Readini();
            //GlobalData.Init();

            model = new GESHOTELEntities();

            Grid ch = this.Parent as Grid;

            EnumVisual(ch, collection);

        }

        public void EnumVisual(Visual parent, List<Visual> collection)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                // Get the child visual at specified index value.
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(parent, i);

                // Add the child visual object to the collection.
                collection.Add(childVisual);

                //// Recursively enumerate children of the child visual object.
                //EnumVisual(childVisual, collection);
            }
        }

        private void Grid_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (txtLogin.Text == "" || plain.Text == "") return;
                ValidateLogin();
            }
            else if (e.Key == Key.Escape)
            {
                txtLogin.Text = "";
                plain.Text = "";
            }

        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {

            ValidateLogin();

        }

        private void ValidateLogin()
        {

            var resutat = from res in model.Utilisateurs
                          where res.Login.Contains(txtLogin.Text)
                          select res;

            if (resutat != null && resutat.Count() != 0)
            {
                PswDecoder pswDeco = new PswDecoder();
                string pass = pswDeco.CrypterPWD(txtLogin.Text, plain.Text);
                string password = pswDeco.DecrypterPWD(resutat.First().Password);

                if (plain.Text == password)
                {

                    GlobalData.Nom = resutat.First().Nom + " " + resutat.First().Prenoms;
                    ////Global.Profil = resutat.First().Profils.Libelle;
                    GlobalData.Id = resutat.First().idUtilisateur;
                    GlobalData.CurrentUser = resutat.First();

                    Msg = "OK";

                    this.Visibility = System.Windows.Visibility.Hidden;

                    Grid grid = collection[0] as Grid;
                    grid.Visibility = System.Windows.Visibility.Visible;

                    Grid gridMain = this.Parent as Grid;

                    MainWindow win = gridMain.Parent as MainWindow;

                    win.LoadModule();
                    win.Init();

                    GlobalData.isLoggedIn = true;


                }
                else
                {
                    Msg = "Error";
                    MessageBox.Show("Mot de pass incorecte. Essayez encore!");
                }

            }
            else
            {
                MessageBox.Show("Login incorecte. Essayez encore!");
            }

        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtLogin.Text = "";
            plain.Text = "";
        }

        private void Window_Closed_1(object sender, EventArgs e)
        {
            Msg = "Error";
        }

        public void Clear()
        {
            txtLogin.Text = "";
            plain.Text = "";
        }

        private void txtLogin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                txtPassword.Focus();
            }
        }

        private void PasswordBox_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                txtPassword.Focus();
            }
        }

    }
}
