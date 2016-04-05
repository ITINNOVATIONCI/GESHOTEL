using GESHOTEL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Telerik.Reporting;
using Telerik.Reporting.Processing;

namespace GESHOTEL.Rapports.Viewers
{
    /// <summary>
    /// Logique d'interaction pour RapportWindows.xaml
    /// </summary>
    public partial class RapportWindows : Window
    {
        Telerik.Reporting.InstanceReportSource instanceReportSource = new Telerik.Reporting.InstanceReportSource();

        public RapportWindows()
        {
            InitializeComponent();

        }

        public RapportWindows(string Rapport)
        {
            InitializeComponent();

            var uriReportSource = new UriReportSource();
            uriReportSource.Uri = Rapport;

            reportViewer.ReportSource = uriReportSource;
            reportViewer.RefreshReport();

        }

        public RapportWindows(string Rapport, object param)
        {
            InitializeComponent();

            var uriReportSource = new UriReportSource();
            uriReportSource.Uri = Rapport;
            uriReportSource.Parameters.Add(new Telerik.Reporting.Parameter("ID", param));

            reportViewer.ReportSource = uriReportSource;
            reportViewer.RefreshReport();

        }

        public void Mail(string No)
        {
            try
            {


                //var reportProcessor = new ReportProcessor();
                ////reportProcessor.PrintReport(instanceReportSource, null);

                //// set any deviceInfo settings if necessary
                //System.Collections.Hashtable deviceInfo =
                //    new System.Collections.Hashtable();

                //instanceReportSource = new Telerik.Reporting.InstanceReportSource();
                //instanceReportSource.ReportDocument = reportViewer.ReportSource;

                //RenderingResult result = reportProcessor.RenderReport("PDF", instanceReportSource, deviceInfo);

                ////string fileName = result.DocumentName + "." + result.Extension;
                ////string path = System.IO.Path.GetTempPath();
                ////string filePath = System.IO.Path.Combine(path, fileName);

                ////using (System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
                ////{
                ////    fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
                ////}

                //MemoryStream ms = new MemoryStream(result.DocumentBytes);
                //ms.Position = 0;

                //Attachment attachment = new Attachment(ms, String.Format("proforma{0}.pdf", No));
                ////MailMessage msg = new MailMessage(from, to, subject, body);
                ////msg.Attachments.Add(attachment);
                ////SmtpClient client = new SmtpClient(smptHost);
                ////client.Send(msg);

                //using (MailMessage mm = new MailMessage("yverson@gmail.com", "boamathieu@yahoo.fr"))
                //{
                //    mm.Subject = "ATT";
                //    mm.Body = "ATT";
                //    mm.Attachments.Add(attachment);
                //    mm.IsBodyHtml = true;
                //    SmtpClient smtp = new SmtpClient();
                //    smtp.Host = "smtp.gmail.com";
                //    smtp.EnableSsl = true;
                //    NetworkCredential NetworkCred = new NetworkCredential("yverson@gmail.com", "Aminata11031985");
                //    smtp.UseDefaultCredentials = true;
                //    smtp.Credentials = NetworkCred;
                //    smtp.Port = 587;
                //    smtp.Send(mm);

                //}

            }
            catch (Exception)
            {

                //MessageBox.Show("Test");
            }

        }

    }
}
