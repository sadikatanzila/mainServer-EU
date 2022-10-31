/**
 * File name: clsDateValidation.cs
 * Author: Anjan Kumar Paul
 * Date: May 22, 2010
 * 
 * Modification history:
 * Name                                 Date                              Desc
 * Anjan Kumar Paul					May 22, 2010				        Created
 * Version: 1.0
  */


using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for clsDateValidation
/// </summary>
namespace EUPortalWeb.CommonClass
{
    public class clsDateValidation : IDisposable
    {
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true); // as a service to those who might inherit from us
        } // end Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return; // we're being collected, so let the GC take care of this object
        } 

        public bool ValidateDate(String date, String format)
        {
            try
            {
                System.Globalization.DateTimeFormatInfo dtfi = new
                System.Globalization.DateTimeFormatInfo();
                dtfi.ShortDatePattern = format;
                DateTime dt = DateTime.ParseExact(date, "d", dtfi);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public bool ValidateDate(String date)
        {
            try
            {
                System.Globalization.DateTimeFormatInfo dtfi = new System.Globalization.DateTimeFormatInfo();
                //dtfi.ShortDatePattern = "dd/MM/yyyy";
                dtfi.ShortDatePattern = "dd-MMM-yyyy";
                DateTime dt = DateTime.ParseExact(Convert.ToDateTime(date).ToString("dd-MMM-yyyy"), "d", dtfi);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}