<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DialogMasterPage.master" AutoEventWireup="true" CodeFile="OrderList.aspx.cs" Inherits="CDS_WKF_Fields_Order_OrderList" %>
<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Fast" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <table class="PopTable" style="width:100%">
       <tr>
           <td>
               <asp:Label ID="Label1" runat="server" Text="客戶代號"></asp:Label>
           </td>
           <td>
               <asp:TextBox ID="txtCustomerID" runat="server"></asp:TextBox>
               <asp:Button ID="btnQuery" runat="server" Text="查詢" OnClick="btnQuery_Click" />
           </td>
       </tr>
   </table>
    <Fast:Grid ID="grid" runat="server" AutoGenerateColumns="false"
        AllowPaging="true" PageSize="8" OnPageIndexChanging="grid_PageIndexChanging"
        OnRowCommand="grid_RowCommand"
        AutoGenerateCheckBoxColumn="false">
        <Columns>
            <asp:TemplateField  HeaderText="訂單編號">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnOrderID" runat="server"
                        Text='<%#Bind("OrderID") %>' CommandName="lbtnOrderID"
                        CommandArgument='<%#Eval("OrderID") %>'
                        ></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="客戶代號" DataField="CustomerID" />
            <asp:BoundField HeaderText="訂單日期" DataField="OrderDate" />
            <asp:BoundField HeaderText="需求日期" DataField="RequiredDate" />
        </Columns>
    </Fast:Grid>

</asp:Content>

