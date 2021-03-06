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

namespace GESHOTEL.ReservationsModules.ViewModels
{
    public class ReservationsViewModel : ObservableObject
    {

        #region Members
        public GESHOTELEntities model;
        private BackgroundWorker worker = new BackgroundWorker();
        ObservableCollection<Reservations> _data = new ObservableCollection<Reservations>();
        ObservableCollection<Reservations> _ListeArrivees = new ObservableCollection<Reservations>();
        ObservableCollection<Pays> _allPays = new ObservableCollection<Pays>();
        ObservableCollection<Villes> _allVilles = new ObservableCollection<Villes>();
        ObservableCollection<Nationalités> _allNationalités = new ObservableCollection<Nationalités>();
        ObservableCollection<Communes> _allCommunes = new ObservableCollection<Communes>();
        ObservableCollection<Quartiers> _allQuartiers = new ObservableCollection<Quartiers>();
        Reservations _selectedData = new Reservations();
        bool _isBusy;
        int _count = 0;
        #endregion

        #region Properties
        public ObservableCollection<Reservations> AllData
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

        public ObservableCollection<Reservations> ListeArrivees
        {
            get
            {
                return _ListeArrivees;
            }
            set
            {
                _ListeArrivees = value;
                RaisePropertyChanged("ListeArrivees");
            }
        }

        public Reservations SelectedData
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

        public ObservableCollection<Pays> AllPays
        {
            get
            {
                return _allPays;
            }
            set
            {
                _allPays = value;
                RaisePropertyChanged("AllPays");
            }
        }

        public ObservableCollection<Villes> AllVilles
        {
            get
            {
                return _allVilles;
            }
            set
            {
                _allVilles = value;
                RaisePropertyChanged("AllVilles");
            }
        }

        public ObservableCollection<Nationalités> AllNationalités
        {
            get
            {
                return _allNationalités;
            }
            set
            {
                _allNationalités = value;
                RaisePropertyChanged("AllNationalités");
            }
        }

        public ObservableCollection<Communes> AllCommunes
        {
            get
            {
                return _allCommunes;
            }
            set
            {
                _allCommunes = value;
                RaisePropertyChanged("AllCommunes");
            }
        }

        public ObservableCollection<Quartiers> AllQuartiers
        {
            get
            {
                return _allQuartiers;
            }
            set
            {
                _allQuartiers = value;
                RaisePropertyChanged("AllQuartiers");
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
        public ReservationsViewModel()
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

            var resultat = from res in model.Reservations
                           where res.TypeOperation == "RESERVATION" && res.Etat == "RESERVER"
                           orderby res.ID descending
                           select res;

            AllData = new ObservableCollection<Reservations>(resultat.ToList());

            DateTime t = DateTime.Now.Date;
            DateTime t1 = DateTime.Now.Add(TimeSpan.FromDays(1)).Date;

            var resultat10 = from res in model.Reservations
                           where res.DateCheckIn != null && res.DateCheckIn >= t && res.DateCheckIn <t1
                           orderby res.ID descending
                           select res;

            ListeArrivees = new ObservableCollection<Reservations>(resultat10.ToList());

            var resultat1 = from res in model.Quartiers
                            where res.Etat == "ACTIF"
                           select res;

            AllQuartiers = new ObservableCollection<Quartiers>(resultat1.ToList());

            var resultat2 = from res in model.Communes
                            where res.Etat == "ACTIF"
                           select res;

            AllCommunes = new ObservableCollection<Communes>(resultat2.ToList());

            var resultat3 = from res in model.Nationalités
                            where res.Etat == "ACTIF"
                           select res;

            AllNationalités = new ObservableCollection<Nationalités>(resultat3.ToList());

            var resultat4 = from res in model.Villes
                            where res.Etat == "ACTIF"
                           select res;

            AllVilles = new ObservableCollection<Villes>(resultat4.ToList());

            var resultat5 = from res in model.Pays
                           where res.Etat == "ACTIF"
                           select res;

            AllPays = new ObservableCollection<Pays>(resultat5.ToList());

        }

        public void CancelChanged()
        {

            var resultat = from res in model.Reservations
                           where res.TypeOperation == "RESERVATION" && res.Etat == "RESERVER"
                           orderby res.ID descending
                           select res;

            AllData = new ObservableCollection<Reservations>(resultat.ToList());

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
