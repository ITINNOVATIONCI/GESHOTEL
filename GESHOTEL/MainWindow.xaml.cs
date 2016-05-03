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
using Telerik.Windows.Controls;

namespace GESHOTEL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RadRibbonWindow
    {
        public MainWindow()
        {

            //Windows8Colors.PaletteInstance.MainColor = Colors.White;
            Windows8Palette.Palette.AccentColor = (Color)ColorConverter.ConvertFromString("#FF09A6F7");
            Windows8Palette.Palette.BasicColor = Colors.DarkGray;
            Windows8Palette.Palette.StrongColor = Colors.Gray;
            //Windows8Colors.PaletteInstance.MarkerColor = Colors.White;
            //Windows8Colors.PaletteInstance.ValidationColor = Colors.Red;

            Windows8Palette.Palette.FontSizeXS = 10;
            Windows8Palette.Palette.FontSizeS = 11;
            Windows8Palette.Palette.FontSize = 12;
            Windows8Palette.Palette.FontSizeL = 14;
            Windows8Palette.Palette.FontSizeXL = 16;
            Windows8Palette.Palette.FontSizeXXL = 19;
            Windows8Palette.Palette.FontSizeXXXL = 24;
            //Windows8Palette.Palette.FontFamily = new FontFamily("Segoe Print");
            //Windows8Palette.Palette.FontFamilyLight = new FontFamily("Segoe Print");
            //Windows8Palette.Palette.FontFamilyStrong = new FontFamily("Segoe Print");

            StyleManager.ApplicationTheme = new Windows8Theme();
            GlobalData.Init();

            InitializeComponent();

            FirstLayout.Visibility = System.Windows.Visibility.Collapsed;
            logControl.Visibility = System.Windows.Visibility.Visible;

            GlobalData.Main = this;
            GlobalData.PaneGroup = rpgMainView;
            GlobalData.rrvMain = this.rrvMain;

        }

        public bool VerifyDock(string Header)
        {
            foreach (RadDocumentPane item in rpgMainView.Items)
            {
                string str = item.Header.ToString();
                if (str == Header)
                {
                    rpgMainView.SelectedItem = item;
                    return true;
                }
            }

            return false;
        }

        public void Init()
        {
            if (GlobalData.Nom != null)
            {
                string mess = "Bienvenue " + GlobalData.Nom;

                //txtLoginInfo.Text = mess;

                //lblSociete.Content = GlobalData.CurrentEntrepots.Libelle;

            }
        }

        private void RadRibbonWindow_Loaded_1(object sender, RoutedEventArgs e)
        {



        }

        public void LoadModule()
        {
            foreach (var item in rrvMain.Items)
            {
                bool rtEtat = false;
                RadRibbonTab rt = item as RadRibbonTab;

                foreach (var item1 in rt.Items)
                {

                    RadRibbonGroup pan = item1 as RadRibbonGroup;

                    foreach (var item2 in pan.Items)
                    {

                        RadRibbonButton btn = item2 as RadRibbonButton;

                        string tag = btn.Tag.ToString();
                        if (GlobalData.VerificationDroit(tag))
                        {
                            rt.Visibility = System.Windows.Visibility.Visible;
                            pan.Visibility = System.Windows.Visibility.Visible;
                            btn.Visibility = System.Windows.Visibility.Visible;
                        }

                    }

                }
            }
        }

        public void CleanModule()
        {

        }

        #region Menu

        private void btnUtilisateurs_Click(object sender, RoutedEventArgs e)
        {
            //RadDocumentPane rad = new RadDocumentPane();
            //rad.Content = new GESHOTEL.UtilisateursModules.DataGridView();
            //rad.Header = "Utilisateurs";
            //rrvMain.Title = "Utilisateurs";
            //rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            //rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


            //if (!VerifyDock("Utilisateurs"))
            //{
            //    rpgMainView.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
            //}
            //else
            //{

            //}
        }

        #endregion

        private void btnPassage_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalData.VerifyClotureSession())
            {

                var result = MessageBox.Show("Voulez-vous fermer la session précédente ?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    RadDocumentPane radMenu = new RadDocumentPane();
                    radMenu.Content = new GESHOTEL.ReservationsModules.ClotureJourneeFrm("Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString());
                    radMenu.Header = "Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString();
                    rrvMain.Title = "Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString();
                    radMenu.FontFamily = new FontFamily("Perpetua Titling MT");
                    radMenu.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                    radMenu.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


                    if (!VerifyDock("Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString()))
                    {
                        rpgMainView.AddItem(radMenu, Telerik.Windows.Controls.Docking.DockPosition.Center);
                    }
                    else
                    {

                    }
                }

                return;
            }

            RadDocumentPane rad = new RadDocumentPane();
            rad.Content = new GESHOTEL.ReservationsModules.PassageUC();
            rad.Header = "PASSAGE";
            rrvMain.Title = "PASSAGE";
            rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


            if (!VerifyDock("PASSAGE"))
            {
                rpgMainView.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
            }
            else
            {

            }
        }

        private void btnSjourNuit_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalData.VerifyClotureSession())
            {

                var result = MessageBox.Show("Voulez-vous fermer la session précédente ?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    RadDocumentPane radMenu = new RadDocumentPane();
                    radMenu.Content = new GESHOTEL.ReservationsModules.ClotureJourneeFrm("Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString());
                    radMenu.Header = "Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString();
                    rrvMain.Title = "Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString();
                    radMenu.FontFamily = new FontFamily("Perpetua Titling MT");
                    radMenu.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                    radMenu.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


                    if (!VerifyDock("Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString()))
                    {
                        rpgMainView.AddItem(radMenu, Telerik.Windows.Controls.Docking.DockPosition.Center);
                    }
                    else
                    {

                    }
                }

                return;
            }

            RadDocumentPane rad = new RadDocumentPane();
            rad.Content = new GESHOTEL.ReservationsModules.StayInsert();
            rad.Header = "WALK IN";
            rrvMain.Title = "WALK IN";
            rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


            if (!VerifyDock("WALK IN"))
            {
                rpgMainView.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
            }
            else
            {

            }
        }

        private void btnReservation_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalData.VerifyClotureSession())
            {

                var result = MessageBox.Show("Voulez-vous fermer la session précédente ?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    RadDocumentPane radMenu = new RadDocumentPane();
                    radMenu.Content = new GESHOTEL.ReservationsModules.ClotureJourneeFrm("Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString());
                    radMenu.Header = "Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString();
                    rrvMain.Title = "Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString();
                    radMenu.FontFamily = new FontFamily("Perpetua Titling MT");
                    radMenu.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                    radMenu.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


                    if (!VerifyDock("Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString()))
                    {
                        rpgMainView.AddItem(radMenu, Telerik.Windows.Controls.Docking.DockPosition.Center);
                    }
                    else
                    {

                    }
                }

                return;
            }

            RadDocumentPane rad = new RadDocumentPane();
            rad.Content = new GESHOTEL.ReservationsModules.ReservationInsert();
            rad.Header = "RESERVATIONS";
            rrvMain.Title = "RESERVATIONS";
            rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


            if (!VerifyDock("RESERVATIONS"))
            {
                rpgMainView.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
            }
            else
            {

            }
        }


        private void btnListeReservation_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalData.VerifyClotureSession())
            {

                var result = MessageBox.Show("Voulez-vous fermer la session précédente ?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    RadDocumentPane radMenu = new RadDocumentPane();
                    radMenu.Content = new GESHOTEL.ReservationsModules.ClotureJourneeFrm("Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString());
                    radMenu.Header = "Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString();
                    rrvMain.Title = "Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString();
                    radMenu.FontFamily = new FontFamily("Perpetua Titling MT");
                    radMenu.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                    radMenu.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


                    if (!VerifyDock("Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString()))
                    {
                        rpgMainView.AddItem(radMenu, Telerik.Windows.Controls.Docking.DockPosition.Center);
                    }
                    else
                    {

                    }
                }

                return;
            }

            RadDocumentPane rad = new RadDocumentPane();
            rad.Content = new GESHOTEL.ReservationsModules.DataGridView();
            rad.Header = "LISTE DES RESERVATIONS";
            rrvMain.Title = "LISTE DES RESERVATIONS";
            rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


            if (!VerifyDock("LISTE DES RESERVATIONS"))
            {
                rpgMainView.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
            }
            else
            {

            }
        }

        private void btnListeArriveReservation_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalData.VerifyClotureSession())
            {

                var result = MessageBox.Show("Voulez-vous fermer la session précédente ?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    RadDocumentPane radMenu = new RadDocumentPane();
                    radMenu.Content = new GESHOTEL.ReservationsModules.ClotureJourneeFrm("Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString());
                    radMenu.Header = "Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString();
                    rrvMain.Title = "Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString();
                    radMenu.FontFamily = new FontFamily("Perpetua Titling MT");
                    radMenu.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                    radMenu.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


                    if (!VerifyDock("Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString()))
                    {
                        rpgMainView.AddItem(radMenu, Telerik.Windows.Controls.Docking.DockPosition.Center);
                    }
                    else
                    {

                    }
                }

                return;
            }

            RadDocumentPane rad = new RadDocumentPane();
            rad.Content = new GESHOTEL.ReservationsModules.ArriveesDataGridView();
            rad.Header = "LISTE DES ARRIVEES";
            rrvMain.Title = "LISTE DES ARRIVEES";
            rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


            if (!VerifyDock("LISTE DES ARRIVEES"))
            {
                rpgMainView.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
            }
            else
            {

            }
        }

        private void btnDepense_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDecaissement_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnApprovisionnement_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDisponibilite_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalData.VerifyClotureSession())
            {

                var result = MessageBox.Show("Voulez-vous fermer la session précédente ?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    RadDocumentPane radMenu = new RadDocumentPane();
                    radMenu.Content = new GESHOTEL.ReservationsModules.ClotureJourneeFrm("Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString());
                    radMenu.Header = "Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString();
                    rrvMain.Title = "Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString();
                    radMenu.FontFamily = new FontFamily("Perpetua Titling MT");
                    radMenu.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                    radMenu.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


                    if (!VerifyDock("Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString()))
                    {
                        rpgMainView.AddItem(radMenu, Telerik.Windows.Controls.Docking.DockPosition.Center);
                    }
                    else
                    {

                    }
                }

                return;
            }

            RadDocumentPane rad = new RadDocumentPane();
            rad.Content = new GESHOTEL.ReservationsModules.StayViews();
            rad.Header = "DISPONIBILITE";
            rrvMain.Title = "DISPONIBILITE";
            rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


            if (!VerifyDock("DISPONIBILITE"))
            {
                rpgMainView.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
            }
            else
            {

            }
        }

        private void btnListeChambre_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalData.VerifyClotureSession())
            {

                var result = MessageBox.Show("Voulez-vous fermer la session précédente ?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    RadDocumentPane radMenu = new RadDocumentPane();
                    radMenu.Content = new GESHOTEL.ReservationsModules.ClotureJourneeFrm("Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString());
                    radMenu.Header = "Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString();
                    rrvMain.Title = "Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString();
                    radMenu.FontFamily = new FontFamily("Perpetua Titling MT");
                    radMenu.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                    radMenu.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


                    if (!VerifyDock("Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString()))
                    {
                        rpgMainView.AddItem(radMenu, Telerik.Windows.Controls.Docking.DockPosition.Center);
                    }
                    else
                    {

                    }
                }

                return;
            }

            RadDocumentPane rad = new RadDocumentPane();
            rad.Content = new GESHOTEL.ReservationsModules.RoomView();
            rad.Header = "LISTE DES CHAMBRES";
            rrvMain.Title = "LISTE DES CHAMBRES";
            rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


            if (!VerifyDock("LISTE DES CHAMBRES"))
            {
                rpgMainView.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
            }
            else
            {

            }
        }

        private void btnRecherche_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCloture_Click(object sender, RoutedEventArgs e)
        {

            //if (!GlobalData.VerifyClotureSession())
            //{

            GlobalData.VerifyClotureSession();
            var result = MessageBox.Show("Voulez-vous fermer la session précédente ?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                RadDocumentPane radMenu = new RadDocumentPane();
                radMenu.Content = new GESHOTEL.ReservationsModules.ClotureJourneeFrm("Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString());
                radMenu.Header = "Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString();
                rrvMain.Title = "Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString();
                radMenu.FontFamily = new FontFamily("Perpetua Titling MT");
                radMenu.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                radMenu.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


                if (!VerifyDock("Cloture de la session du " + GlobalData.CurrentRegistres.DateDebut.Value.ToShortDateString()))
                {
                    rpgMainView.AddItem(radMenu, Telerik.Windows.Controls.Docking.DockPosition.Center);
                }
                else
                {

                }
            }

            return;
            //}
        }

        private void btnVueDensemble_Click(object sender, RoutedEventArgs e)
        {
            GlobalData.VerifyClotureSession();

            RadDocumentPane rad = new RadDocumentPane();
            rad.Content = new GESHOTEL.ReservationsModules.ClotureJournee();
            rad.Header = "Vue d'ensemble";
            rrvMain.Title = "Vue d'ensemble";
            rad.FontFamily = new FontFamily("Perpetua Titling MT");
            rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

            if (!VerifyDock("Vue d'ensemble"))
            {
                rpgMainView.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
            }
            else
            {

            }
        }

        private void btnJcaisse_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEtat_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnStatistique_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEquipement_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNumeroChambre_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTypeChambre_Click(object sender, RoutedEventArgs e)
        {
            RadDocumentPane rad = new RadDocumentPane();
            rad.Content = new GESHOTEL.TypeChambresModules.DataGridView();
            rad.Header = "TYPE CHAMBRES";
            rrvMain.Title = "TYPE CHAMBRES";
            rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


            if (!VerifyDock("TYPE CHAMBRES"))
            {
                rpgMainView.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
            }
            else
            {

            }
        }

        private void btnCategorie_Click(object sender, RoutedEventArgs e)
        {
            RadDocumentPane rad = new RadDocumentPane();
            rad.Content = new GESHOTEL.CategoriesModules.DataGridView();
            rad.Header = "CATEGORIES";
            rrvMain.Title = "CATEGORIES";
            rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


            if (!VerifyDock("CATEGORIES"))
            {
                rpgMainView.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
            }
            else
            {

            }
        }

        private void btnConditionnement_Click(object sender, RoutedEventArgs e)
        {
            RadDocumentPane rad = new RadDocumentPane();
            rad.Content = new GESHOTEL.CommunesModules.DataGridView();
            rad.Header = "CONDITIONNEMENT";
            rrvMain.Title = "CONDITIONNEMENT";
            rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


            if (!VerifyDock("CONDITIONNEMENT"))
            {
                rpgMainView.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
            }
            else
            {

            }
        }

        private void btnProduit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnIdTypes_Click(object sender, RoutedEventArgs e)
        {
            RadDocumentPane rad = new RadDocumentPane();
            rad.Content = new GESHOTEL.IDTypesModules.DataGridView();
            rad.Header = "IDTYPES";
            rrvMain.Title = "IDTYPES";
            rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


            if (!VerifyDock("IDTYPES"))
            {
                rpgMainView.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
            }
            else
            {

            }
        }

        private void btnNationalite_Click(object sender, RoutedEventArgs e)
        {
            RadDocumentPane rad = new RadDocumentPane();
            rad.Content = new GESHOTEL.NationalitésModules.DataGridView();
            rad.Header = "NATIONALITES";
            rrvMain.Title = "NATIONALITES";
            rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


            if (!VerifyDock("NATIONALITES"))
            {
                rpgMainView.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
            }
            else
            {

            }
        }

        private void btnPays_Click(object sender, RoutedEventArgs e)
        {
            RadDocumentPane rad = new RadDocumentPane();
            rad.Content = new GESHOTEL.PaysModules.DataGridView();
            rad.Header = "PAYS";
            rrvMain.Title = "PAYS";
            rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


            if (!VerifyDock("PAYS"))
            {
                rpgMainView.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
            }
            else
            {

            }
        }

        private void btnVille_Click(object sender, RoutedEventArgs e)
        {
            RadDocumentPane rad = new RadDocumentPane();
            rad.Content = new GESHOTEL.VillesModules.DataGridView();
            rad.Header = "VILLE";
            rrvMain.Title = "VILLE";
            rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


            if (!VerifyDock("VILLE"))
            {
                rpgMainView.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
            }
            else
            {

            }
        }

        private void btnCommune_Click(object sender, RoutedEventArgs e)
        {
            RadDocumentPane rad = new RadDocumentPane();
            rad.Content = new GESHOTEL.CommunesModules.DataGridView();
            rad.Header = "COMMUNE";
            rrvMain.Title = "COMMUNE";
            rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


            if (!VerifyDock("COMMUNE"))
            {
                rpgMainView.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
            }
            else
            {

            }
        }

        private void btnQuartier_Click(object sender, RoutedEventArgs e)
        {
            RadDocumentPane rad = new RadDocumentPane();
            rad.Content = new GESHOTEL.QuartiersModules.DataGridView();
            rad.Header = "QUARTIER";
            rrvMain.Title = "QUARTIER";
            rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


            if (!VerifyDock("QUARTIER"))
            {
                rpgMainView.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
            }
            else
            {

            }
        }

        private void btnModePaiement_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRubrique_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSousRubrique_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnHotelInfo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnService_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRegleReduction_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPromotions_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReduction_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTypePrix_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPrixChambre_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRaison_Click(object sender, RoutedEventArgs e)
        {
            RadDocumentPane rad = new RadDocumentPane();
            rad.Content = new GESHOTEL.RaisonsModules.DataGridView();
            rad.Header = "RAISONS";
            rrvMain.Title = "RAISONS";
            rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


            if (!VerifyDock("RAISONS"))
            {
                rpgMainView.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
            }
            else
            {

            }
        }

        private void btnRaisonType_Click(object sender, RoutedEventArgs e)
        {
            RadDocumentPane rad = new RadDocumentPane();
            rad.Content = new GESHOTEL.RaisonTypeModules.DataGridView();
            rad.Header = "RAISONTYPE";
            rrvMain.Title = "RAISONTYPE";
            rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


            if (!VerifyDock("RAISONTYPE"))
            {
                rpgMainView.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
            }
            else
            {

            }
        }

        private void btnClients_Click(object sender, RoutedEventArgs e)
        {
            RadDocumentPane rad = new RadDocumentPane();
            rad.Content = new GESHOTEL.ClientsModules.DataGridView();
            rad.Header = "CLIENTS";
            rrvMain.Title = "CLIENTS";
            rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


            if (!VerifyDock("CLIENTS"))
            {
                rpgMainView.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
            }
            else
            {

            }
        }

    }
}
