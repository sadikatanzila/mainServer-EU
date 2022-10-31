using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("http://www.easternuni.edu.bd/HomeEU.aspx");
       /* Session["ctrl_admin_Id"] = "";
        Session["ctrlId"] = "";
        Session["user"] = "";
        Session.Abandon();  
        loadGeneralNotices();
        load_news();
        load_events();*/
    }

    private void loadGeneralNotices()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_general_notice());
         

        Table tbl = new Table();
        tbl.BorderColor = System.Drawing.Color.Red;
        //tbl.BorderStyle=  
       // tbl.CssClass = "<span style=\" border-right: #ff0066 3px dashed; border-top: #ff0066 3px dashed; border-left: #ff0066 3px dashed; border-bottom: #ff0066 3px dashed\" ></span>\" ";
        tbl.BorderStyle = BorderStyle.Dashed;
        tbl.BorderWidth = new Unit(2);


        PlaceHolder1.Controls.Add(tbl);
        int i = 0;
        foreach (DataRow dr in  ds.Tables["WEB_NOTICE_BOARD"].Rows)
        {
            if (i == 5)
            break;

            TableRow tr = new TableRow();
            tbl.Controls.Add(tr);

            TableCell tdL = new TableCell();
            tr.Controls.Add(tdL);
            tdL.VerticalAlign = VerticalAlign.Top;
            Image img = new Image();
            tdL.Controls.Add(img);
            img.ImageUrl = "images\\arrow3.gif";

            TableCell tdR = new TableCell();
            tr.Controls.Add(tdR); //color: #0066cc
            tdR.Text = "<a href=\"general/newsEvents/_notice.aspx?nid=" + dr["NOTICE_ID"].ToString() + "\"><span style=\"font-size: 9pt;color: #0066cc;  font-family: Arial\">" + dr["TITLE"].ToString() + "</span></a>";
            tdR.ForeColor = System.Drawing.Color.FromName("#0066cc");
            tdR.Font.Name = "Arial";
            tdR.Font.Underline = false;
            tdR.Font.Size = new FontUnit(9);

            i++;
        }

    }

    private void load_events()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_all_events());
        DateTime today=new DateTime();
        today=DateTime.Today;
        //date check
        for (int k = 0; k < ds.Tables["WEB_NEWS_EVENTS"].Rows.Count; k++)
        {
            if (!string.IsNullOrEmpty( ds.Tables["WEB_NEWS_EVENTS"].Rows[k]["TO_DATE"].ToString()))
            {

                if (today > Convert.ToDateTime(ds.Tables["WEB_NEWS_EVENTS"].Rows[k]["TO_DATE"].ToString()))
                {
                    ds.Tables["WEB_NEWS_EVENTS"].Rows.RemoveAt(k);
                    k--;
                }
            }
        }
        // limit check
        for (int i = 0; i < ds.Tables["WEB_NEWS_EVENTS"].Rows.Count; i++)
        {
            if (i == 5)
            {
                ds.Tables["WEB_NEWS_EVENTS"].Rows.RemoveAt(i);
                i--;
            }
        }

        Table tbl = new Table();
        PlaceHolder_events.Controls.Add(tbl);

         
        foreach (DataRow dr in ds.Tables["WEB_NEWS_EVENTS"].Rows)
        { 
                TableRow tr = new TableRow();
                tbl.Controls.Add(tr);

                TableCell tdL = new TableCell();
                tr.Controls.Add(tdL);
                tdL.Text = "<span style=\"font-size: 8pt;color: #000000; font-family: Arial\">" + new cls_tools().get_user_short_formateDate(dr["FROM_DATE"].ToString()) + "</span>" ;// - " + new cls_tools().get_user_short_formateDate(dr["TO_DATE"].ToString()) + "</span>";
                tdL.Width = new Unit(60);
                tdL.VerticalAlign = VerticalAlign.Top;
                 
                TableCell tdR = new TableCell();
                tr.Controls.Add(tdR);
                tdR.Text = "<a href=\"general/newsEvents/_event.aspx?evn=" + dr["NEWS_EVENT_ID"].ToString() + "\"><span style=\"font-size: 9pt;color: #0066cc; font-family: Arial\">" + dr["TITLE"].ToString() + "</span></a>";
             
        }

        TableRow tr_link = new TableRow();
        tbl.Controls.Add(tr_link);

        TableCell td_link = new TableCell();
        //td_blank.BackColor = System.Drawing.Color.Black;
        td_link.Height = new Unit(2);
        td_link.ColumnSpan = 2;
        td_link.Width = new Unit("100%");
        tr_link.Controls.Add(td_link);
        td_link.Text = " <a class=\"alinkmore_wuc\" href=\"general/newsEvents/_eventBox.aspx\"><span style=\"font-size: 7pt;color: #0066cc; font-family: Arial\"><strong>More»</strong></span></a>";


    }


    private void load_news()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_all_news());
        
        Table tbl_root = new Table();
        PlaceHolder_news_events.Controls.Add(tbl_root);

        for (int i = 0; i < ds.Tables["WEB_NEWS_EVENTS"].Rows.Count;i++ )
        {
            if (i == 3)
                {
                    ds.Tables["WEB_NEWS_EVENTS"].Rows.RemoveAt(i);
                    i--;
                }            
        }

        foreach (DataRow dr in ds.Tables["WEB_NEWS_EVENTS"].Rows)
        {
            
            TableRow tr_root = new TableRow();
            tbl_root.Controls.Add(tr_root);

            TableCell td_root = new TableCell();
            tr_root.Controls.Add(td_root);

            // News table (left+content+right)

            Table tbl_news = new Table();
            tbl_news.BackImageUrl = "images1/News-text3-b.jpg";
            tbl_news.CellPadding = 0; 
            tbl_news.Height=new Unit(93);
            tbl_news.Width= new Unit(221);
            td_root.Controls.Add(tbl_news);

            TableRow tr_news = new TableRow();
            tbl_news.Controls.Add(tr_news);

            TableCell td_leftNews = new TableCell();
            td_leftNews.Width = new Unit(9);
            td_leftNews.Text = "&nbsp;";
            tr_news.Controls.Add(td_leftNews); 

            // Mid TD
            
            TableCell td_mainNews = new TableCell();
            td_mainNews.CssClass = " align:left;  valign:top; width:203";
            tr_news.Controls.Add(td_mainNews);

            Table tbl_child = new Table();
            tbl_child.CellPadding=2;
            tbl_child.CellSpacing=2;
            tbl_child.Width=new Unit("100%");
            td_mainNews.Controls.Add(tbl_child);

            TableRow tr_child = new TableRow();
            tbl_child.Controls.Add(tr_child);

            TableCell td_s_l = new TableCell();
            td_s_l.Height=new Unit(89);
            td_s_l.Width = new Unit("44%");
            tr_child.Controls.Add(td_s_l);

            Image img = new Image();
            img.CssClass = "align:top;border:0; ";
            img.Width = new Unit("91");
            img.ImageUrl = "admin/news_event_images/" + dr["NEWS_EVENT_ID"].ToString() + "." + dr["EVENT_IMAGE"].ToString().Split('.')[dr["EVENT_IMAGE"].ToString().Split('.').Length - 1];
            img.Height=new Unit(73);
            td_s_l.Controls.Add(img);

            TableCell td_s_r = new TableCell();
            td_s_r.Width = new Unit("56%");            
            td_s_r.CssClass="align:left; valign:top";
            tr_child.Controls.Add(td_s_r);

            Table tbl_txt = new Table();
            tbl_txt.CellPadding=1;
            tbl_txt.CellSpacing=1;
            tbl_txt.Width = new Unit("100%");
            td_s_r.Controls.Add(tbl_txt);

            TableRow tr_txt = new TableRow();
            tbl_txt.Controls.Add(tr_txt);

            TableCell td_txt = new TableCell();
            td_txt.Text = "<a  href=\"general/newsEvents/_news.aspx?nws=" + dr["NEWS_EVENT_ID"].ToString() + "\"><span style=\"font-size: 9pt;color: #0066cc; font-family: Arial\"> " + dr["title"].ToString() + "</span> </a>";            
            tr_txt.Controls.Add(td_txt);

            // end mid TD

            TableCell td_rightNews = new TableCell();
            td_rightNews.Width = new Unit(10);
            //td_rightNews.CssClass = "font-size: 9pt; color: #0066cc; font-family: Arial";
            //td_rightNews.Text = "&nbsp;";
            tr_news.Controls.Add(td_rightNews);


            TableRow tr_blank = new TableRow();
            tbl_root.Controls.Add(tr_blank);

            TableCell td_blank = new TableCell();
            //td_blank.BackColor = System.Drawing.Color.Black;
            td_blank.Height = new Unit(5);
            td_blank.Width = new Unit("100%");
            tr_blank.Controls.Add(td_blank);
        }

            TableRow tr_link = new TableRow();
            tbl_root.Controls.Add(tr_link);

            TableCell td_link= new TableCell();
            //td_blank.BackColor = System.Drawing.Color.Black;
            td_link.Height = new Unit(2);
            td_link.Width = new Unit("100%");
            tr_link.Controls.Add(td_link);
            td_link.Text = " <a class=\"alinkmore_wuc\" href=\"general/newsEvents/_newsGallery.aspx\"><span style=\"font-size: 7pt;color: #0066cc; font-family: Arial\"><strong>More»</strong></span></a>";

    }




}
