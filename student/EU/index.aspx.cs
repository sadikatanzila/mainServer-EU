/**
 * File name: index.aspx.cs
 * Author: Anjan Kumar Paul
 * Date: July 08, 2010
 * 
 * Modification history:
 * Name                                 Date                              Desc
 * Anjan Kumar Paul					July 08, 2010				        Created
 * Version: 1.0
  */

using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using EUPortalWeb.UserControl;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.IO;
using ARAS.Karnel.Utilities;
using EUPortal.BAL;

namespace EUPortalWeb
{
    public partial class index : System.Web.UI.Page
    {
        List<Message> _messages = null;
        Message _message = null;

        List<NewsAndEvents> _newsAndEvents = null;
        NewsAndEvents _newsAndEvent = null;

        List<Notice> _notices = null;
        Notice _notice = null;

        List<WelcomeMessage> _welcomeMessages = null;
        WelcomeMessage _welcomeMessage = null;


        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateControls();
            }
        }
        #endregion 

        #region PopulateControls

        private void PopulateControls()
        {
            try
            {
                _newsAndEvents = NewsAndEvents.Gets("");
                PopulateNewsAndEvents(_newsAndEvents);

                _messages = Message.Gets();
                PopulateMessage(_messages);

                _notices = Notice.Gets("");
                PopulateNotice(_notices);

                _welcomeMessages = WelcomeMessage.Gets();
                PopulateWelcomeMessage(_welcomeMessages);

                return;
            }
            catch (Exception ex)
            {
                return;
            }
        }
        #endregion

        #region PopulateNewsAndEvents
        private void PopulateNewsAndEvents(List<NewsAndEvents> newsAndEvents)
        {
            int counter = 0;

            StringBuilder sb = new StringBuilder();

            sb.Append("<h2>News &amp; Events</h2>");

            sb.Append("<strong ><font color=" + "#669900" + ">");
                        
            foreach (NewsAndEvents item in newsAndEvents)
            {
                counter++;

                if (counter == 8)
                {
                    sb.Append("<a href=" + "Pages/NewsAndEventsArchieve.aspx"+"  target="+"Pages/NewsAndEventsArchieve.aspx"+">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                        "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                        "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                        "&nbsp;&nbsp;&nbsp;MORE&gt;&gt;</a>");
                    break;
                }
                sb.Append("<dl ><a href=" + "Pages/NewsAndEventsDetail.aspx?id=" + item.ID + " target=" + "Pages/NewsAndEventsDetail.aspx?id=" + item.ID + ">" + item.Title + "</a></dl>");

            }
            
            sb.Append("</font></strong>");
            divNewsAndEvents.InnerHtml = sb.ToString();
                       
        }
        #endregion 

        #region PopulateMessage
        private void PopulateMessage(List<Message> messages)
        {
            foreach (Message item in messages)
            {
                switch (item.MessageType)
                {
                    case 1:
                        msgFromViceChancellor.InnerText = item.MessageBody.ToString();
                        Session["messageFromVice"] = "MESSAGE FROM THE VICE CHANCELLOR";
                        Session["messageVice"] = msgFromViceChancellor.InnerText;
                        break;
                    case 2:
                        msgFromChairman.InnerText = item.MessageBody.ToString();
                        Session["messageFromChair"] = "MESSAGE FROM THE CHAIRMAN";
                        Session["messageChair"] = msgFromChairman.InnerText;
                        break;
                    case 3:
                        msgFromWhy.InnerText = item.MessageBody.ToString();
                        Session["messageFromWhy"] = "WHY CHOOSE EU ?";
                        Session["messageWhy"] = msgFromWhy.InnerText;
                        break;
                    case 4:
                        msgFromConvocation.InnerText = item.MessageBody.ToString();
                        Session["messageFromEU"] = "EU CONVOCATION";
                        Session["messageEu"] = msgFromConvocation.InnerText;
                        break;

                    default:
                        break;
                }


            }
        }
        #endregion 

        #region PopulateNotice
        private void PopulateNotice(List<Notice> notices)
        {
            int counter = 0;

            StringBuilder sb = new StringBuilder();

            sb.Append("<h2>Notice Board</h2>");

            //sb.Append("<strong ><font color=" + "#669900" + ">");

            foreach (Notice item in notices)
            {
                counter++;

                if (counter == 7)
                {
                    sb.Append("<a href=" + "Pages/NoticeBoardArchieve.aspx" + " target=" + "Pages/NoticeBoardArchieve.aspx" + ">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                        "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                        "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                        "&nbsp;&nbsp;&nbsp;MORE&gt;&gt;</a>");
                    break;
                }
                sb.Append("<dl ><dd><a href=" + "Pages/NoticeBoardDetail.aspx?id=" + item.ID + " target=" + "Pages/NoticeBoardDetail.aspx?id=" + item.ID + ">" + item.Title + "</a></dd></dl>");

            }

            //sb.Append("</font></strong>");
            divNotice.InnerHtml = sb.ToString();
        }
        #endregion 

        #region PopulateWelcomeMessage
        private void PopulateWelcomeMessage(List<WelcomeMessage> WelcomeMessages)
        {

            welcomeMessage.InnerText = _welcomeMessages[0].MessageBody.ToString();

        }

        #endregion
    }
}
