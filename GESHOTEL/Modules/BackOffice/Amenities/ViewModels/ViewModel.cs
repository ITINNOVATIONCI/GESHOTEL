﻿using GESHOTEL.COMMON;
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

namespace GESHOTEL.AmenitiesModules.ViewModels
{
    public class AmenitiesViewModel : ObservableObject
    {

        #region Members
        public GESHOTELEntities model;
        private BackgroundWorker worker = new BackgroundWorker();
        ObservableCollection<Amenities> _data = new ObservableCollection<Amenities>();
        ObservableCollection<AmenitiesTypes> _AllAmenitiestype = new ObservableCollection<AmenitiesTypes>();
        Amenities _selectedData = new Amenities();
        bool _isBusy;
        int _count = 0;
        #endregion

        #region Properties
        public ObservableCollection<Amenities> AllData
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
        public ObservableCollection<AmenitiesTypes> AllAmenitiestypes
        {
            get
            {
                return _AllAmenitiestype;
            }
            set
            {
                _AllAmenitiestype = value;
                RaisePropertyChanged("AllAmenitiestypes");
            }
        }

        public Amenities SelectedData
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
        public AmenitiesViewModel()
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
                        var resultat = from res in model.Amenities
                           where res.Etat == "ACTIF"
                           select res;
             AllData = new ObservableCollection<Amenities>(resultat.ToList());

            model = new GESHOTELEntities();
            var resultat1 = from res in model.AmenitiesTypes
                           where res.Etat == "ACTIF"
                           select res;
            AllAmenitiestypes = new ObservableCollection<AmenitiesTypes>(resultat1.ToList());

        }

        public void CancelChanged()
        {

            var resultat = from res in model.Amenities
                           select res;

            AllData = new ObservableCollection<Amenities>(resultat.ToList());

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
