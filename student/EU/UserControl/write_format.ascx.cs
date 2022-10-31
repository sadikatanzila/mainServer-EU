using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace EUPortalWeb.UserControl
{
    public partial class UserControls_write_format : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void CrystalExportReporttoPdfDownloadDialouge(string rpt_name, ReportDocument oRepDoc)
        {
            MemoryStream oStream;
            oStream = (MemoryStream)
            oRepDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Context.Response.Clear();
            Context.Response.Buffer = true;
            Context.Response.AddHeader("content-disposition", "attachment;filename=" + rpt_name + ".pdf");
            Context.Response.ContentType = "application/pdf";
            Context.Response.BinaryWrite(oStream.ToArray());
            Context.Response.End();

        }
        public void CrystalExportReporttoPdfToThePage(string rpt_name, ReportDocument oRepDoc)
        {
            MemoryStream oStream;
            oStream = (MemoryStream)
            oRepDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Context.Response.Clear();
            Context.Response.Buffer = true;
            Context.Response.ContentType = "application/pdf";
            Context.Response.BinaryWrite(oStream.ToArray());
            Context.Response.End();
            Context.Response.Redirect(@"~/" + rpt_name + ".pdf");

        }

        public void printReport(ReportDocument crDocument)
        {
            string strFName;

            ExportOptions crExportOptions = new ExportOptions();
            DiskFileDestinationOptions crDiskFileDestination = new DiskFileDestinationOptions();



            strFName = Server.MapPath("lastrpt.pdf");

            crDiskFileDestination.DiskFileName = strFName;
            crExportOptions = crDocument.ExportOptions;
            crExportOptions.DestinationOptions = crDiskFileDestination;
            crExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            crExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            crDocument.Export();

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.WriteFile(strFName, true);
            Response.Flush();
            Response.Close();
            System.IO.File.Delete(strFName);
        }
    }
}