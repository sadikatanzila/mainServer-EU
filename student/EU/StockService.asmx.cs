using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

namespace EUPortalWeb
{
    /// <summary>
    /// Summary description for StockService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class StockService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        #region DownloadShops

        [WebMethod]
        public DataTable DownloadShops(string shopCode, string userName)
        {
            DataTable dtShops = new DataTable();

            try
            {

            }
            catch (Exception ex)
            {
                //throw new Exception(string.Format("Can not download shops due to:{0}{1}", Environment.NewLine, ex.Message));
            }

            return dtShops;
        }

        #endregion

        #region DownloadUsers

        [WebMethod]
        public DataTable DownloadUsers(string shopCode)
        {
            DataTable dtUsers = new DataTable();

            try
            {

            }
            catch (Exception ex)
            {
                //throw new Exception(string.Format("Can not download users due to:{0}{1}", Environment.NewLine, ex.Message));
            }

            return dtUsers;
        }

        #endregion

        #region DownloadModels

        [WebMethod]
        public DataTable DownloadModels(string shopCode, string userName)
        {
            DataTable dtModels = new DataTable();

            try
            {

            }
            catch (Exception ex)
            {
                //throw new Exception(string.Format("Can not download models due to:{0}{1}", Environment.NewLine, ex.Message));
            }

            return dtModels;
        }

        #endregion

        #region DownloadStocks

        [WebMethod]
        public DataTable DownloadStocks(string shopCode, string userName)
        {
            DataTable dtStocks = new DataTable();

            try
            {

            }
            catch (Exception ex)
            {
                //throw new Exception(string.Format("Can not download stocks due to:{0}{1}", Environment.NewLine, ex.Message));
            }

            return dtStocks;
        }

        #endregion

        #region DownloadTaxInfo

        [WebMethod]
        public DataTable DownloadTaxInfo(string shopCode, string userName)
        {
            DataTable dtTaxInfo = new DataTable();

            try
            {

            }
            catch (Exception ex)
            {
                //throw new Exception(string.Format("Can not download tax info due to:{0}{1}", Environment.NewLine, ex.Message));
            }

            return dtTaxInfo;
        }

        #endregion

        #region UploadStocks

        [WebMethod]
        public string UploadStocks(string shopCode, string userName, DataTable dtStocks)
        {
            string ackMessage = "OK";

            try
            {

            }
            catch (Exception ex)
            {
                ackMessage = ex.Message;
                //throw new Exception(string.Format("Can not upload stocks due to:{0}{1}", Environment.NewLine, ex.Message));
            }

            return string.Format("#{0}#", ackMessage);
        }

        #endregion

        #region UploadShops

        [WebMethod]
        public string UploadShops(string shopCode, string userName, DataTable dtShops)
        {
            string ackMessage = "OK";

            try
            {

            }
            catch (Exception ex)
            {
                ackMessage = ex.Message;
                //throw new Exception(string.Format("Can not upload shops due to:{0}{1}", Environment.NewLine, ex.Message));
            }

            return string.Format("#{0}#", ackMessage);
        }

        #endregion

        #region UploadUsers

        [WebMethod]
        public string UploadUsers(string shopCode, string userName, DataTable dtUsers)
        {
            string ackMessage = "OK";

            try
            {

            }
            catch (Exception ex)
            {
                ackMessage = ex.Message;
                //throw new Exception(string.Format("Can not upload shops due to:{0}{1}", Environment.NewLine, ex.Message));
            }

            return string.Format("#{0}#", ackMessage);
        }

        #endregion

        #region UploadTransactions

        [WebMethod]
        public string UploadTransactions(string shopCode, string userName, DataTable dtTrans)
        {
            string ackMessage = "OK";

            try
            {

            }
            catch (Exception ex)
            {
                ackMessage = ex.Message;
                //throw new Exception(string.Format("Can not upload shops due to:{0}{1}", Environment.NewLine, ex.Message));
            }

            return string.Format("#{0}#", ackMessage);
        }

        #endregion
    }
}
