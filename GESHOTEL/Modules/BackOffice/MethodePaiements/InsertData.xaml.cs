﻿
using GESHOTEL.Models;
using GESHOTEL.MethodePaiementsModules.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GESHOTEL.MethodePaiementsModules
{
    /// <summary>
    /// Interaction logic for InsertData.xaml
    /// </summary>
    public partial class InsertData : Window
    {
        string Etat = "";
        MethodePaiementsViewModel viewVM;
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

        public InsertData(string etat, MethodePaiements ele, MethodePaiementsViewModel view)
        {
            InitializeComponent();

            this.DataContext = viewVM = view;

            Etat = etat;

            if (etat == "AJOUT")
            {
                this.Title = "Enregistrement d'un Quartier";
            }
            else
            {
                this.Title = "Modification d'un Quartier";
            }
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MethodePaiementsViewModel vm = this.DataContext as MethodePaiementsViewModel;
                MethodePaiements ent = vm.SelectedData;

                if (Etat == "AJOUT")
                {
                    try
                    {
                        
                        ent.Etat = "ACTIF";
                        ent.idHotel = 1;
                        viewVM.model.MethodePaiements.Add(ent);
                        viewVM.model.SaveChanges();
                         Msg = "OK";
                        this.Close();

                    }
                    catch (Exception ex)
                    {

                        Msg = "Error";
                        ErrorMsg = ex.Message;

                    }
                }
                else
                {
                    try
                    {

                        viewVM.model.SaveChanges();

                        Msg = "OK";
                        this.Close();

                    }
                    catch (Exception ex)
                    {

                        Msg = "Error";
                        ErrorMsg = ex.Message;

                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            MethodePaiementsViewModel vehi = this.DataContext as MethodePaiementsViewModel;
            vehi.SelectedData = null;

            this.Close();
        }

    }
}
