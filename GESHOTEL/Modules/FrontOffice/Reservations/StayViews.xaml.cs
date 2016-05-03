using GESHOTEL.ReservationsModules.ViewModel;
using GESHOTEL.Models;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GESHOTEL.ReservationsModules
{
    /// <summary>
    /// Interaction logic for StayViews.xaml
    /// </summary>
    public partial class StayViews : UserControl
    {
        string etat;
        CustomAppointment oldApp;

        public StayViews()
        {
            InitializeComponent();

            this.DataContext = new ScheduleViewModel();

        }

        private void RadScheduleView_ShowDialog(object sender, Telerik.Windows.Controls.ShowDialogEventArgs e)
        {
            e.Cancel = true;
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void scheduleview_AppointmentEditing(object sender, Telerik.Windows.Controls.AppointmentEditingEventArgs e)
        {

            //if (scheduleview.SelectedAppointment != null)
            //{
            //    oldApp = scheduleview.SelectedAppointment as CustomAppointment;
            //}
        }

        private void scheduleview_AppointmentEdited(object sender, Telerik.Windows.Controls.AppointmentEditedEventArgs e)
        {
            //if (scheduleview.SelectedAppointment != null)
            //{
            //    var resApp = e.OriginalSource;
            //    CustomAppointment app = scheduleview.SelectedAppointment as CustomAppointment;

            //    if (oldApp == app)
            //    {
            //        Reservations res = GlobalData.model.Reservations.Where(a => (a.ID == app.ReservationID)).First();
            //    }
                
            //}
        }
    }
}
