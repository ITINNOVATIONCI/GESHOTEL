﻿
using GESHOTEL.Models;
using GESHOTEL.ProduitsModules.ViewModels;
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

namespace GESHOTEL.ProduitsModules
{
    /// <summary>
    /// Interaction logic for InsertData.xaml
    /// </summary>
    public partial class InsertData : Window
    {
        string Etat = "";
        ProduitsViewModel viewVM;
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

        public InsertData(string etat, Produits ele, ProduitsViewModel view)
        {
            InitializeComponent();

            this.DataContext = viewVM = view;

            Etat = etat;

            if (etat == "AJOUT")
            {
                this.Title = "Enregistrement d'un Produit";
                nupd.Value = 0;
            }
            else
            {
                this.Title = "Modification d'un Produit";
            }
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProduitsViewModel vm = this.DataContext as ProduitsViewModel;
                Produits ent = vm.SelectedData;

                if (Etat == "AJOUT")
                {
                    try
                    {
                        GESHOTELEntities context = new GESHOTELEntities();
                        var query = from res in context.Conditionnements
                                    where res.Libelle == rcbConditionnement.SearchText && res.Etat == "ACTIF"
                                    select res;
                        if (query.Count() != 0)
                        {

                        }
                            else
                        {
                        Conditionnements cd = new Conditionnements();
                            cd.Etat = "ACTIF";
                            cd.idHotel = 1;
                            cd.Libelle = rcbCategorie.SearchText;
                            viewVM.model.Conditionnements.Add(cd);
                            ent.Conditionnements = cd;
                        }

                        var quer = from res in context.Categories
                                    where res.Libelle == rcbCategorie.SearchText && res.Etat == "ACTIF"
                                    select res;
                        if (quer.Count() != 0)
                        {
                     
                        }
                        else
                        {
                            Categories cat = new Categories();
                            cat.Etat = "ACTIF";
                            cat.idHotel = 1;
                            cat.Libelle = rcbCategorie.SearchText;
                            viewVM.model.Categories.Add(cat);
                            ent.Categories = cat;
                        }


                        ent.Etat = "ACTIF";
                        ent.idHotel = 1;
                        viewVM.model.Produits.Add(ent);
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
            ProduitsViewModel vehi = this.DataContext as ProduitsViewModel;
            vehi.SelectedData = null;

            this.Close();
        }

    }
}
