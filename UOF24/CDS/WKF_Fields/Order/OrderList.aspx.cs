using Ede.Uof.Utility.Page;
using Ede.Uof.Utility.Page.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.WinControls.UI;
using Training.UCO;

public partial class CDS_WKF_Fields_Order_OrderList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        ((Master_DialogMasterPage)this.Master).Button1Text = "";
        ((Master_DialogMasterPage)this.Master).Button2Text = "";

        if (!IsPostBack)
        {
            BindOrderData();
        }
    }

    private void BindOrderData()
    {
        DemoUCO uco = new DemoUCO();
        grid.DataSource = uco.GetOrderList(txtCustomerID.Text).Orders;
        grid.DataBind();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindOrderData();
    }

    protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid.PageIndex = e.NewPageIndex;
        BindOrderData();
    }

    protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName == "lbtnOrderID")
        {
            string orderID= e.CommandArgument.ToString();
            Dialog.SetReturnValue2(orderID);
            Dialog.Close(this.Page);
        }
    }
}