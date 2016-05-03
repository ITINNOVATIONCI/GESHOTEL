using GESHOTEL.ReservationsModules.ViewModel;
using GESHOTEL.Models;
using GESHOTEL.ReservationsModules.ViewModel;
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
    /// Interaction logic for RoomView.xaml
    /// </summary>
    public partial class RoomView : UserControl
    {
        public RoomView()
        {
            InitializeComponent();

            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                //DispatcherTimer messageTimer = new DispatcherTimer();
                //messageTimer.Tick += new EventHandler(messageTimer_Tick);
                //messageTimer.Interval = new TimeSpan(0, 0, 0, 1);
                //messageTimer.Start();

                DataContext = new RoomViewModel();

            }

            
        }

        void messageTimer_Tick(object sender, EventArgs e)
        {

            try
            {
                //GlobalData.modelRP.Refresh(RefreshMode.StoreWins, GlobalData.modelRP.ChambreDisponibilite);

                //var result = from res in GlobalData.modelRP.Reservations
                //             where res.Etat == "ACTIF"
                //             select res;


            }
            catch (Exception)
            {

            }
        }
    }
}
