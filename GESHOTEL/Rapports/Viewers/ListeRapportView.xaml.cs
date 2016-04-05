using GESHOTEL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using Telerik.Reporting;

namespace GESHOTEL.Rapports.Viewers
{
    /// <summary>
    /// Interaction logic for ListeRapportView.xaml
    /// </summary>
    public partial class ListeRapportView : UserControl
    {
        public ListeRapportView()
        {
            InitializeComponent();
            GetReport();
        }

        //private void GetReport()
        //{
        //    ObservableCollection<Fichiers> allRapport = new ObservableCollection<Fichiers>();


        //    foreach (var item in Directory.GetFiles(GlobalData.ReportListePath, "*.trdx"))
        //    {
        //        allRapport.Add(new Fichiers(item));
        //    }

        //    lstRapport.ItemsSource = allRapport;

        //    lstRapport.SelectedIndex = -1;
        //}

        private void GetReport()
        {

            GESHOTELEntities model = new GESHOTELEntities();

            List<Permissions> uti = GlobalData.CurrentUser.Permissions.Where(c => c.EstRapport == true).ToList();

            ObservableCollection<Fichiers> allRapport = new ObservableCollection<Fichiers>();

            foreach (var item in uti)
            {
                allRapport.Add(new Fichiers(item.Path, item.DisplayName));
            }

            lstRapport.ItemsSource = allRapport;

            lstRapport.SelectedIndex = -1;
        }

        private void ListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //reportViewer = new Telerik.ReportViewer.Wpf.ReportViewer(); 
                var uriReportSource = new UriReportSource();
                uriReportSource.Uri = (lstRapport.SelectedItem as Fichiers).FullName;
                reportViewer.ReportSource = uriReportSource;
                reportViewer.RefreshReport();
            }
            catch (Exception)
            {

            }
        }
    }

    public class Fichiers
    {

        #region Variables

        FileInfo _currentFichier;
        string _displayName;
        string _typeFichier;
        string _images;

        #endregion

        #region Propriete

        public Fichiers CurrentLVFile
        {
            get { return this; }
        }

        public FileInfo CurrentFichier
        {
            get
            {
                return _currentFichier;
            }
            set
            {
                _currentFichier = value;
            }
        }

        public string Name
        {
            get
            {

                return CurrentFichier.Name;

            }
        }

        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        public string FullName
        {
            get
            {

                return CurrentFichier.FullName;


            }
        }

        public string Type
        {
            get
            {
                if (_typeFichier == "FICHIER")
                {
                    //image1.Source = new BitmapImage(new Uri(@"C:\Users\Albert\Pictures\address-book-icon.jpg"));

                    //bi3.Source = new BitmapImage(new Uri("/Images/blue_mail_icon.png", UriKind.Relative));
                    //string str = CurrentFichier.Extension.Remove(0, 1);
                    //_images = @"C:\Users\Math\Pictures\GED\Chakram png\png\File " + str.ToUpper() + ".png";
                    return "Fichier " + CurrentFichier.Extension.Remove(0, 1);
                }
                else
                {
                    return "Dossier de Fichiers";
                }

            }
        }

        public string Taille
        {
            get
            {
                if (_typeFichier == "FICHIER")
                {
                    return CurrentFichier.Length.ToString() + "ko";
                }
                else
                {
                    return "";
                }
            }
        }

        public DateTime CreationDate
        {
            get
            {

                return CurrentFichier.CreationTime;

            }
        }

        public DateTime ModificationDate
        {
            get
            {

                return CurrentFichier.LastWriteTime;

            }
        }

        public DateTime DerniereUtilisationDate
        {
            get
            {

                return CurrentFichier.LastAccessTime;

            }


        }

        public string TypeFichier
        {
            get { return _typeFichier; }
            set { _typeFichier = value; }
        }

        #endregion

        #region Constructeur

        public Fichiers(string Chemin, string DName)
        {

            DisplayName = DName;
            CurrentFichier = new FileInfo(Chemin);



        }

        #endregion

        #region Methode

        #endregion

    }
}
