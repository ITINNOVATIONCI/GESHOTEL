using GESHOTEL.COMMON;
using GESHOTEL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Data.DataForm;
using Telerik.Windows.Data;

namespace GESHOTEL.HotelInfoModules.ViewModels
{
    public class HotelInfoViewModel : ObservableObject
    {

        #region Members
        public GESHOTELEntities model;
        private BackgroundWorker worker = new BackgroundWorker();
        ObservableCollection<HotelInfo> _data = new ObservableCollection<HotelInfo>();
        ObservableCollection<Pays> _Allpays = new ObservableCollection<Pays>();
        ObservableCollection<Villes> _Allvilles = new ObservableCollection<Villes>();

        HotelInfo _selectedData = new HotelInfo();
        bool _isBusy;
        int _count = 0;
        #endregion

        #region Properties
        public ObservableCollection<HotelInfo> AllData
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                RaisePropertyChanged("AllData");
            }
        }

        public ObservableCollection<Villes> Allvilles
        {
            get
            {
                return _Allvilles;
            }
            set
            {
                _Allvilles = value;
                RaisePropertyChanged("Allvilles");
            }
        }

        public ObservableCollection<Pays> Allpays
        {
            get
            {
                return _Allpays;
            }
            set
            {
                _Allpays = value;
                RaisePropertyChanged("Allpays");
            }
        }

        public HotelInfo SelectedData
        {
            get
            {
                return _selectedData;
            }
            set
            {
                _selectedData = value;
                RaisePropertyChanged("SelectedData");
            }
        }

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }

        #endregion

        #region Construction
        public HotelInfoViewModel()
        {
            worker.DoWork += this.WorkerDoWork;
            worker.RunWorkerCompleted += WorkerRunWorkerCompleted;

            Refresh();

        }

        public void Refresh()
        {

            //Demarre le Background Worker et commence le radBusyIndicator
            if (!worker.IsBusy)
            {
                this.IsBusy = true;
                worker.RunWorkerAsync();
            }

        }

        //La methode de demmarge du BackgroundWorker
        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            //appel la methode load
            Load();
        }

        //Met a jour le UI et stop le RadBusyIndicator
        private void UpdateDataSource()
        {

            this.IsBusy = false;

        }

        void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Quand la methode load a fini de charger les infos on appel la methode UpdateDataSource
            //pour mettre a jour le UI
            //Dispatcher.BeginInvoke(new Action(this.UpdateDataSource));

            this.IsBusy = false;
        }

        private void Load()
        {

            model = new GESHOTELEntities();

            var resultat = from res in model.HotelInfo
                           where res.Etat == "ACTIF"
                           select res;

            AllData = new ObservableCollection<HotelInfo>(resultat.ToList());

        }

        public void CancelChanged()
        {

            var resultat = from res in model.HotelInfo
                           select res;
                        AllData = new ObservableCollection<HotelInfo>(resultat.ToList());

        }

        public void SaveChanged()
        {

            model.SaveChanges();

        }

        #endregion

        #region Commands
        void AddCommandExecute()
        {

            if (SelectedData != null)
            {

            }
        }

        bool CanAddCommandExecute()
        {
            return true;
        }

        public ICommand AddCommand { get { return new RelayCommand(AddCommandExecute, CanAddCommandExecute); } }


        #endregion

    }
}
