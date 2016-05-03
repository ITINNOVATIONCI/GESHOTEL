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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GESHOTEL.ReservationsModules
{
    /// <summary>
    /// Interaction logic for ClotureJournee.xaml
    /// </summary>
    public partial class ClotureJournee : UserControl
    {
        public ClotureJournee()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.DataContext = new OverviewsDashBoard();
        }

        private void btnClientModifier_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = new OverviewsDashBoard();
        }

        private void btnModifier_Click_1(object sender, RoutedEventArgs e)
        {
            this.DataContext = new OverviewsDashBoard();
        }
    }
}
