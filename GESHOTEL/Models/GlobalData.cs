using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace GESHOTEL.Models
{
    public static class GlobalData
    {
        public static RadRibbonWindow Main;
        public static RadRibbonView rrvMain;
        public static RadPaneGroup PaneGroup;

        public static GESHOTELEntities model;
        public static bool isLoggedIn = false;
        public static string Nom;
        public static string Profil;
        public static int Id;
        public static HotelInfo CurrentHotel;
        public static Registres CurrentRegistres;
        public static int RegId;
        public static Utilisateurs CurrentUser;
        static string folderPath = Environment.CurrentDirectory;
        public static string ReportConnection;
        public static string Connection;
        public static string ConnectionL;
        public static string GedPath = "Y:";
        public static string RapportListePath = @"D:\DropBox\ERP\Rapport\INDEPENDANT\";
        public static string RapportBonPath = @"D:\DropBox\ERP\Rapport\Bon\";
        public static string RapportFiltrePath = @"D:\DropBox\ERP\Rapport\FILTRE\";
        public static string BoothPath = @"D:\DropBox\ERP\Rapport\FILTRE\";
        public static string RapportFilter = "";
        public static string RapportWhere = "";

        public static bool VerificationDroit(string droit)
        {
            List<Permissions> uti = GlobalData.CurrentUser.Permissions.Where(c => c.Description == droit).ToList();

            if (uti.Count == 0)
            {
                return false;
            }
            return true;
        }


        public static void Init()
        {
            model = new GESHOTELEntities();

            CurrentUser = model.Utilisateurs.Where(c => c.idUtilisateur == Id).FirstOrDefault();

        }

        public static bool VerifyClotureSession()
        {
            DateTime date = DateTime.Now.Date;

            var resultat = from res in GlobalData.model.Registres
                           where res.State != "FERMER"
                           select res;


            if (resultat != null && resultat.Count() != 0)
            {
                Registres reg = resultat.ToList().LastOrDefault();
                CurrentRegistres = resultat.ToList().LastOrDefault();
                RegId = CurrentRegistres.idRegistre;

                if (reg.Date != date && reg.State != "FERMER")
                {
                    date = reg.Date.Value.AddDays(1);
                    date = new DateTime(date.Year, date.Month, date.Day, 05, 00, 00);

                    if (DateTime.Now > date)
                    {

                        return false;
                    }
                }
                return true;
            }

            return false;
        }

        public static bool VerifyDock(string Header)
        {
            foreach (RadDocumentPane item in PaneGroup.Items)
            {
                string str = item.Header.ToString();
                if (str == Header)
                {
                    PaneGroup.SelectedItem = item;
                    return true;
                }
            }

            return false;
        }

        public static bool RemoveItem(string Header)
        {
            foreach (RadDocumentPane item in PaneGroup.Items)
            {
                string str = item.Header.ToString();
                if (str == Header)
                {
                    PaneGroup.RemovePane(item);
                    return true;
                }
            }

            return false;
        }

        public static void Readini()
        {

            try
            {
                //Writeini();

                IniFile ini = new IniFile(folderPath + @"\Config.ini");

                Connection = ini.IniReadValue("Info", "Connection");
                Connection = BuildConnection(Connection);

                ConnectionL = ini.IniReadValue("Info", "ConnectionL");
                ReportConnection = ConnectionL;
                ConnectionL = BuildConnection(ConnectionL);


                GedPath = ini.IniReadValue("Info", "GedPath");
                RapportListePath = ini.IniReadValue("Info", "ReportListePath");
                RapportFiltrePath = ini.IniReadValue("Info", "ReportFiltrePath");
                RapportBonPath = ini.IniReadValue("Info", "RapportBonPath");

            }
            catch (Exception)
            {
                //message
            }
        }

        public static void Writeini()
        {
            IniFile ini = new IniFile(folderPath + @"\Config.ini");
            ini.IniWriteValue("Info", "Connection", @"Data Source=.\Math;Initial Catalog=ERP;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework");
            ini.IniWriteValue("Info", "GedPath", "Z:");
            ini.IniWriteValue("Info", "ReportListePath", @"D:\DropBox\Dropbox\ERP\Rapport\INDEPENDANT\");
            ini.IniWriteValue("Info", "ReportFiltrePath", @"D:\DropBox\Dropbox\ERP\Rapport\FILTRE\");
            ini.IniWriteValue("Info", "RapportBonPath", @"D:\DropBox\Dropbox\ERP\Rapport\Bon\");
            //ini.IniWriteValue("Info", "ReportListe", "1200");

        }

        private static string BuildConnection(string providerString)
        {
            // Specify the provider name, server and database.
            string providerName = "System.Data.SqlClient";

            // Initialize the EntityConnectionStringBuilder.
            EntityConnectionStringBuilder entityBuilder =
                new EntityConnectionStringBuilder();

            //Set the provider name.
            entityBuilder.Provider = providerName;

            // Set the provider-specific connection string.
            entityBuilder.ProviderConnectionString = providerString;

            // Set the Metadata location.
            entityBuilder.Metadata = @"res://*/GEESTOCKModels.csdl|res://*/GEESTOCKModels.ssdl|res://*/GEESTOCKModels.msl";

            //using (EntityConnection conn = new EntityConnection(entityBuilder.ConnectionString))
            //{
            //    conn.Open();
            //    Console.WriteLine("Just testing the connection.");
            //    conn.Close();
            //}

            return entityBuilder.ConnectionString;
        }

        public static string GenerateFacture(string param, int num)
        {
            string tamp = "";

            string tp = param;
            tamp = tp;
            for (int i = tp.Length; i < num; i++)
            {
                tamp = "0" + tamp;
            }

            return tamp;
        }
    }

    public class IniFile
    {
        public string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// INIFile Constructor.
        /// </summary>
        /// <param name="INIPath"></param>
        public IniFile(string INIPath)
        {
            path = INIPath;
        }
        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <param name="Section"></param>
        /// Section name
        /// <param name="Key"></param>
        /// Key Name
        /// <param name="Value"></param>
        /// Value Name
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Path"></param>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
            return temp.ToString();

        }
    }

}
