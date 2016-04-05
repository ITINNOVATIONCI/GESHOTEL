﻿
using GESHOTEL.Models;
using GESHOTEL.ReservationsModules.ViewModels;
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

namespace GESHOTEL.ReservationsModules
{
    /// <summary>
    /// Interaction logic for InsertData.xaml
    /// </summary>
    public partial class InsertData : Window
    {
        string Etat = "";
        ReservationsViewModel viewVM;
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

        public InsertData(string etat, Reservations ele, ReservationsViewModel view)
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

                //if (ele.Sexe != null)
                //{
                //    if (ele.Sexe == "M")
                //    {
                //        rdMasc.IsChecked = true;
                //    }
                //    else if (ele.Sexe == "F")
                //    {
                //        rdFem.IsChecked = true;
                //    }
                //}

            }
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ReservationsViewModel vm = this.DataContext as ReservationsViewModel;
                Reservations ent = vm.SelectedData;

                //if (ent.Noms == null || ent.Noms.Trim() == "")
                //{
                //    lblMessageError.Text = "Remplir le champ Nom avant de continué";
                //    lblMessageError.Visibility = System.Windows.Visibility.Visible;

                //    return;
                //}

                if (Etat == "AJOUT")
                {
                    try
                    {
                        //if (rdMasc.IsChecked == true)
                        //{
                        //    ent.Sexe = "M";
                        //}
                        //else if (rdFem.IsChecked == true)
                        //{
                        //    ent.Sexe = "F";
                        //}

                        ent.Etat = "ACTIF";
                        ent.idHotel = 1;
                        viewVM.model.Reservations.Add(ent);
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

                        //if (rdMasc.IsChecked == true)
                        //{
                        //    ent.Sexe = "M";
                        //}
                        //else if (rdFem.IsChecked == true)
                        //{
                        //    ent.Sexe = "F";
                        //}

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
            ReservationsViewModel vehi = this.DataContext as ReservationsViewModel;
            vehi.SelectedData = null;

            this.Close();
        }

    }
}
