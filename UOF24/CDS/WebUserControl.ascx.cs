using Ede.Uof.EIP.Organization.Util;
using Ede.Uof.Utility.Page;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using Ede.Uof.EIP.SystemInfo;
using Ede.Uof.Utility.Configuration;

public partial class CDS_WebUserControl : UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        hfCurentUser.Value = Current.UserGUID;
        DropDownList ddlDailyArea =
            (DropDownList)RadToolBar1.FindItemByValue("ChooseArea").FindControl("ddlDailyArea");

        var cookie = new UofCookieForUC(Page);

        var selectvalue = "";

        if (cookie.Get("DailyForecastDefaultValue") != null)
            selectvalue = cookie.Get("DailyForecastDefaultValue");

        if (selectvalue != "")
        {
            ddlDailyArea.SelectedValue = selectvalue;
        }
        else
        {
            ddlDailyArea.Items[0].Selected = true;
        }
        BindWeather();
    }

    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //<asp:Label ID="Label6" runat="server" Text=""></asp:Label>

            Label lblDate = (Label)e.Row.FindControl("lblDate");
            Label lblTime = (Label)e.Row.FindControl("lblTime");
            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            Label lblTemperature = (Label)e.Row.FindControl("lblTemperature");
            Label lblRain = (Label)e.Row.FindControl("lblRain");
            Image imgStatus = (Image)e.Row.FindControl("imgStatus");

            lblDate.Text = UserTime.SetZone(hfCurentUser.Value).GetNowForUi().Date.ToString("MM/dd");
            if (e.Row.DataItemIndex == 0)
            {
                string[] s = e.Row.DataItem.ToString().Split(' ');

                //先解析用哪張圖
                string imageUrl = GetImageType(s[3]);
                if (imageUrl != "")
                {
                    imgStatus.ImageUrl = imageUrl;
                }
                else
                {
                    imgStatus.Visible = false;
                }
                lblTime.Text = s[2];// +"(06:00 ~ 18:00)";
                lblStatus.Text = s[3];
                lblTemperature.Text = s[5] + " " + s[6] + " " + s[7];
                lblRain.Text = s[9];


            }
            else
            {
                HtmlTableRow tr = (HtmlTableRow)e.Row.FindControl("header");
                tr.Style.Add("display", "none");
                string[] s = e.Row.DataItem.ToString().Replace("<br>", " ").Split(' ');

                //先解析用哪張圖
                string imageUrl = GetImageType(s[1]);
                if (imageUrl != "")
                {
                    imgStatus.ImageUrl = imageUrl;
                }
                else
                {
                    imgStatus.Visible = false;
                }

                lblTime.Text = s[0];// +"(18:00 ~ 06:00)";
                lblStatus.Text = s[1];
                lblTemperature.Text = s[3] + " " + s[4] + " " + s[5];
                lblRain.Text = s[7];
            }
            //e.Row.Cells[1].Text = "123";
        }
    }

    private string GetImageType(string status)
    {
        Image img = new Image();
        //判斷 晴 (陰,雲) 雨
        //共會有七種狀況
        //晴 (陰,雲) 雨
        //0     0     1 (雨)
        //0     1     0 (陰 雲)
        //0     1     1 (陰雨 雲雨)
        //1     0     0 (晴)
        //1     0     1 (晴雨)
        //1     1     0 (陰晴 雲晴)
        //1     1     1 (陰晴雨 雲晴雨)
        if (!status.Contains("晴") &&
            (!status.Contains("陰") && !status.Contains("雲")) &&
            status.Contains("雨"))
        {
            return "~/Common/Images/Icon/icon_w0004.png";

        }
        else if (!status.Contains("晴") &&
                 (status.Contains("陰") || status.Contains("雲")) &&
                 !status.Contains("雨"))
        {
            return "~/Common/Images/Icon/icon_w0002.png";
        }
        else if (!status.Contains("晴") &&
                 (status.Contains("陰") || status.Contains("雲")) &&
                 status.Contains("雨"))
        {
            return "~/Common/Images/Icon/icon_w0006.png";

            //檢查檔案存不存在
            //if (File.Exists("~/Common/Images/Icon/icon_w0003.png"))
            //{
            //    img.ImageUrl = "~/Common/Images/Icon/icon_w0003.png";
            //    return img;
            //}
            //else
            //{
            //    return null;
            //}
        }
        else if (status.Contains("晴") &&
                 (!status.Contains("陰") && !status.Contains("雲")) &&
                 !status.Contains("雨"))
        {
            return "~/Common/Images/Icon/icon_w0001.png";
        }
        else if (status.Contains("晴") &&
                 (!status.Contains("陰") && !status.Contains("雲")) &&
                 status.Contains("雨"))
        {
            return "~/Common/Images/Icon/icon_w0005.png";
        }
        else if (status.Contains("晴") &&
                 (status.Contains("陰") || status.Contains("雲")) &&
                 !status.Contains("雨"))
        {
            return "~/Common/Images/Icon/icon_w0003.png";
        }
        else if (status.Contains("晴") &&
                 (status.Contains("陰") || status.Contains("雲")) &&
                 status.Contains("雨"))
        {
            return "~/Common/Images/Icon/icon_w0007.png";
        }
        else
        {
            return "";
        }
    }

    private void BindWeather()
    {
        string errorHostUrl = "";
        try
        {
            //if (!IsPostBack)
            //{
            //    if (Request.Cookies["UOFDailyForecastDefaultValue"] != null)
            //    {

            //        forecastDefaultValue = Request.Cookies["UOFDailyForecastDefaultValue"].Value;
            //        ddlDailyArea.SelectedValue = forecastDefaultValue;

            //    }
            //    else
            //    {
            //        ddlDailyArea.Items[0].Selected = true;
            //    }
            //}
            DropDownList ddlDailyArea = (DropDownList)RadToolBar1.FindItemByValue("ChooseArea").FindControl("ddlDailyArea");
            Setting setting = new Setting();
            string hostUrl = setting["CWARssUrl"] + ddlDailyArea.SelectedValue;

            HttpWebRequest request = WebRequest.Create(hostUrl) as HttpWebRequest;
            request.Timeout = 5000;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string xml = "";
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                xml = reader.ReadToEnd();
            }

            //開始解析
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            XmlNode xmlDayNode = xmlDoc.SelectSingleNode("/rss/channel/item/title");
            XmlNode xmlNightNode = xmlDoc.SelectSingleNode("/rss/channel/item/description");
            XmlNode xmlSourceUrl = xmlDoc.SelectSingleNode("/rss/channel/item/link");
            errorHostUrl = xmlSourceUrl.InnerText;

            List<string> list = new List<string>();
            list.Add(xmlDayNode.InnerText);
            list.Add(xmlNightNode.InnerText);
            Grid1.DataSource = list;
            Grid1.DataBind();
        }
        catch (Exception ex)
        {
            string openScript = "";
            if (errorHostUrl != "")
            {
                openScript = string.Format("window.open('{0}'); return false;", errorHostUrl);
            }
            else
            {
                openScript = string.Format("window.open('{0}'); return false;", "http://www.cwa.gov.tw");
            }

            Grid1.Visible = false;
            lblErrorMessage.Text = string.Format(lblErrorMessage.Text, ex.Message);
            lblErrorMessage.Visible = true;
            lbtnSourceWeb.Visible = true;
            lbtnSourceWeb.Attributes.Add("onclick", openScript);

        }
    }

    protected void ddlDailyArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlDailyArea = (DropDownList)RadToolBar1.FindItemByValue("ChooseArea").FindControl("ddlDailyArea");

        var cookie = new UofCookieForUC(Page);
        cookie.Set("DailyForecastDefaultValue", ddlDailyArea.SelectedValue);


        BindWeather();
    }
}