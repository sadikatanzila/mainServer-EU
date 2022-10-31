using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.Services;
using System.Drawing;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.IO;
using System.Collections.Generic;

using ARAS.Karnel.Utilities;
using EUPortal.BAL;

namespace EUPortalWeb
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Handler : IHttpHandler
    {
        List<Message> _messages = null;
        Message _message = null;

        List<NewsAndEvents> _newsAndEvents = null;
        NewsAndEvents _newsAndEvent = null;

        List<Notice> _notices = null;
        Notice _notice = null;

        List<WelcomeMessage> _welcomeMessages = null;
        WelcomeMessage _welcomeMessage = null;

        #region ProcessRequest

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        #endregion 

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        //public Image byteArrayToImage(byte[] byteArrayIn)
        //{
        //    MemoryStream ms = new MemoryStream(byteArrayIn);
                
        //    Image returnImage = Image.FromStream(ms);
        //    return returnImage;
        //}

        #region memoryStreamToImage

        public Image memoryStreamToImage(MemoryStream ms)
        {
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        #endregion 

        #region imageToByteArray

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        #endregion 

        #region RetrieveImageWelcomeMessage

        public Image RetrieveImageWelcomeMessage(int id)
        {
            Image image = null;
            _welcomeMessage = WelcomeMessage.Get(id);
            image = _welcomeMessage.Logo;

            return image;
        }

        #endregion 

        #region RetrieveImageNotice

        public Image RetrieveImageNotice(int id)
        {
            Image image = null;
            _notice = Notice.Get(id);
            image = _notice.Logo;

            return image;
        }

        #endregion


        #region RetrieveImageNewsAndEvents

        public Image RetrieveImageNewsAndEvents(int id)
        {
            Image image = null;
            _newsAndEvent = NewsAndEvents.Get(id);
            image = _newsAndEvent.Logo;

            return image;
        }

        #endregion

        #region RetrieveImageMessage

        public Image RetrieveImageMessage(int id)
        {
            Image image = null;
            _message = Message.Get(id);
            image = _message.Logo;

            return image;
        }

        #endregion

        
        public  DataTable ListToDataTable<T>(IEnumerable<T> list)
        {
            var dt = new DataTable();

            foreach (var info in typeof(T).GetProperties())
            {
                dt.Columns.Add(new DataColumn(info.Name, info.PropertyType));
            }
            foreach (var t in list)
            {
                var row = dt.NewRow();
                foreach (var info in typeof(T).GetProperties())
                {
                    row[info.Name] = info.GetValue(t, null);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }
    }

}
