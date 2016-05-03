using GESHOTEL.ReservationsModules.ViewModel;
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
using System.Windows.Threading;

namespace GESHOTEL.ReservationsModules
{
    /// <summary>
    /// Interaction logic for ReservationPanel.xaml
    /// </summary>
    public partial class ReservationPanel : UserControl
    {
        public ReservationPanel()
        {
            InitializeComponent();

            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                GlobalData.modelRP = new GESHOTELEntities();

                //DispatcherTimer messageTimer = new DispatcherTimer();
                //messageTimer.Tick += new EventHandler(messageTimer_Tick);
                //messageTimer.Interval = new TimeSpan(0, 0, 0, 1);
                //messageTimer.Start();
            }

        }

        void messageTimer_Tick(object sender, EventArgs e)
        {

            try
            {


            }
            catch (Exception)
            {

            }
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {

                GlobalFO.rpVM = new ReservationPanelViewModel();
                DataContext = GlobalFO.rpVM;
            }

        }

        private void ListBox_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {

            try
            {

                ReservationPanelViewModel ent = this.DataContext as ReservationPanelViewModel;

                ent.BeginModifierReservation(null);


            }
            catch (Exception ex)
            {

                //MessageBox.Show(Francais.MessageErrorElement, Francais.MessageAvertissementTitre, MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }

        private void TextBox_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtChambre.Text == "")
                {
                    nomsRadComboBox.SelectedItem = null;
                    (this.DataContext as ReservationPanelViewModel).Load();
                }
                else
                {
                    nomsRadComboBox.SelectedItem = null;
                    (this.DataContext as ReservationPanelViewModel).Recherche(txtChambre.Text);
                }
                
            }
        }

        private void nomsRadComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (nomsRadComboBox.SelectedItem != null)
                {
                    txtChambre.Text = "";
                    Clients cli = nomsRadComboBox.SelectedItem as Clients;
                    (this.DataContext as ReservationPanelViewModel).Recherche(cli);
                }
            }
        }

        private void nomsRadComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (nomsRadComboBox.SelectedItem != null)
            {
                txtChambre.Text = "";
                Clients cli = nomsRadComboBox.SelectedItem as Clients;
                (this.DataContext as ReservationPanelViewModel).Recherche(cli);
            }

        }

        private void RadButton_Click_1(object sender, RoutedEventArgs e)
        {
            txtChambre.Text = "";
            nomsRadComboBox.SelectedIndex = -1;
            (this.DataContext as ReservationPanelViewModel).Load();
        }

    }
}
