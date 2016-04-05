using GESHOTEL.Models;
using GESHOTEL.UtilisateursModules.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace GESHOTEL.UtilisateursModules
{
    /// <summary>
    /// Interaction logic for UtlisateurModuleView.xaml
    /// </summary>
    public partial class UtlisateurModuleView : Window
    {

        Utilisateurs utilisateurs;
        UtilisateursViewModel viewVM;
        List<Permissions> lstRemPermission = new List<Permissions>();
        string state;
        string msg;

        public string Msg
        {
            get { return msg; }
            set { msg = value; }
        }

        string errorMsg;

        public string ErrorMsg
        {
            get { return errorMsg; }
            set { errorMsg = value; }
        }

        public UtlisateurModuleView(Utilisateurs Utilisateurs, UtilisateursViewModel view, string state)
        {
            InitializeComponent();
            this.DataContext = viewVM = view;
            utilisateurs = Utilisateurs;
            //this.DataContext = Utilisateurs;

            this.state = state;

            if (state != "AJOUT")
            {
                foreach (var item in utilisateurs.Permissions)
                {

                    lstDroit.Items.Add(item);

                }

            }

        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            radTreeView.ItemsSource = viewVM.model.Permissions.Where(c => c.idParent == null).ToList();

        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            this.Close();
        }

        private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (state == "AJOUT")
                {

                    foreach (Permissions item in lstDroit.Items)
                    {

                        Permissions per = item;

                        utilisateurs.Permissions.Add(per);
                    }

                    Msg = "OK";
                    Clear();
                    this.Close();

                }
                else
                {
                    if (lstRemPermission.Count != 0)
                    {
                        foreach (Permissions item in lstRemPermission)
                        {
                            utilisateurs.Permissions.Remove(item);
                        }
                    }

                    foreach (Permissions item in lstDroit.Items)
                    {

                        Permissions per = item;

                        utilisateurs.Permissions.Add(per);
                    }

                    Msg = "OK";
                    Clear();
                    this.Close();

                }

            }
            catch (Exception ex)
            {
                Msg = "Error";
                ErrorMsg = ex.Message;
            }

        }

        private void GetContainers()
        {
            // gets all nodes from the TreeView  
            Collection<RadTreeViewItem> allTreeContainers = GetAllItemContainers(this.radTreeView);

            foreach (RadTreeViewItem item in allTreeContainers)
            {
                if (item.IsChecked == true)
                {
                    //item.Item
                }
            }

        }

        private Collection<RadTreeViewItem> GetAllItemContainers(System.Windows.Controls.ItemsControl itemsControl)
        {
            Collection<RadTreeViewItem> allItems = new Collection<RadTreeViewItem>();
            for (int i = 0; i < itemsControl.Items.Count; i++)
            {
                // try to get the item Container  
                RadTreeViewItem childItemContainer = itemsControl.ItemContainerGenerator.ContainerFromIndex(i) as RadTreeViewItem;
                // the item container maybe null if it is still not generated from the runtime  
                if (childItemContainer != null)
                {
                    allItems.Add(childItemContainer);
                    Collection<RadTreeViewItem> childItems = GetAllItemContainers(childItemContainer);
                    foreach (RadTreeViewItem childItem in childItems)
                    {
                        allItems.Add(childItem);
                    }
                }
            }
            return allItems;
        }

        private void Clear()
        {
            //UtilisateurModuleGridView.Items.Clear();
        }

        private void radTreeView_Checked(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            var childNodes = e.Source;
            var isChecked = e.OriginalSource;
            //UpdateAllChildren(childNodes, isChecked);

            //RadTreeViewItem items = (e.Source as RadTreeViewItem);

            //foreach (RadTreeViewItem item in items.Items)
            //{
            //    item.IsChecked = true;
            //}


        }

        private void radTreeView_Unselected(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (radTreeView.SelectedItems != null)
            {

                foreach (var item in radTreeView.SelectedItems)
                {
                    if (!VerifDoublePermission(item as Permissions))
                    {

                        lstDroit.Items.Add(item);
                        lstRemPermission.Remove(item as Permissions);

                    }

                }

            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lstDroit.SelectedItem != null)
            {
                foreach (var item in radTreeView.SelectedItems)
                {
                    lstRemPermission.Add(item as Permissions);
                    lstDroit.Items.Remove(item);
                }

            }
        }

        public bool VerifDoublePermission(Permissions permis)
        {

            return lstDroit.Items.Contains(permis);
            //foreach (Permissions item in lstDroit.Items)
            //{
            //    if (true)
            //    {

            //    }
            //}
        }


    }
}
