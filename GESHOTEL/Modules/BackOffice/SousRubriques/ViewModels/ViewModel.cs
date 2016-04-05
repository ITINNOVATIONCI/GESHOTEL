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

namespace GESHOTEL.Sous_RubriquesModules.ViewModels
{
    public class Sous_RubriquesViewModel : ObservableObject
    {

        #region Members
        public GESHOTELEntities model;
        private BackgroundWorker worker = new BackgroundWorker();
        ObservableCollection<Sous_Rubriques> _data = new ObservableCollection<Sous_Rubriques>();
        ObservableCollection<Rubriques> _dataRubriques = new ObservableCollection<Rubriques>();

        Sous_Rubriques _selectedData = new Sous_Rubriques();
        bool _isBusy;
        int _count = 0;
        #endregion

        #region Properties
        public ObservableCollection<Sous_Rubriques> AllData
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
        public ObservableCollection<Rubriques> AllRubriques
        {
            get
            {
                return _dataRubriques;
            }
            set
            {
                _dataRubriques = value;
                RaisePropertyChanged("AllRubriques");
            }
        }

        public Sous_Rubriques SelectedData
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
        public Sous_RubriquesViewModel()
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
            var resultat = from res in model.Sous_Rubriques
                           where res.Etat == "ACTIF"
                           select res;
            AllData = new ObservableCollection<Sous_Rubriques>(resultat.ToList());

            model = new GESHOTELEntities();
            var resultat1 = from res in model.Rubriques
                           where res.Etat == "ACTIF"
                           select res;
            AllRubriques = new ObservableCollection<Rubriques>(resultat1.ToList());

        }

        public void CancelChanged()
        {

            var resultat = from res in model.Sous_Rubriques
                           select res;

            AllData = new ObservableCollection<Sous_Rubriques>(resultat.ToList());

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
