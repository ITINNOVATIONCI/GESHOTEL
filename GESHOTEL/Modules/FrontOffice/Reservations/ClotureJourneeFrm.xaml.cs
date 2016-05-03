using GESHOTEL.ReservationsModules.ViewModel;
using GESHOTEL.Models;
using System;
using System.Collections.Generic;
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

namespace GESHOTEL.ReservationsModules
{
    /// <summary>
    /// Interaction logic for ClotureJourneeFrm.xaml
    /// </summary>
    public partial class ClotureJourneeFrm : UserControl
    {
        string PaneHeader = "";
        public ClotureJourneeFrm(string str)
        {
            InitializeComponent();

            PaneHeader = str;
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.DataContext = new OverviewsDashBoard();
        }

        private void RadButton_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void btnNvlleSession_Click(object sender, RoutedEventArgs e)
        {
            OverviewsDashBoard ovDash = this.DataContext as OverviewsDashBoard;
            if (ovDash.ClotureSession())
            {
                GlobalData.RemovePane(PaneHeader);
            }
            else
            {
                this.DataContext = new OverviewsDashBoard();
            }

        }

        private void btnModifier_Click_1(object sender, RoutedEventArgs e)
        {
            this.DataContext = new OverviewsDashBoard();
        }

    }
}
