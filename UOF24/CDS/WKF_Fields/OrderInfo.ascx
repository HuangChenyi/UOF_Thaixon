﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderInfo.ascx.cs" Inherits="WKF_OptionalFields_OrderInfo" %>
<%@ Reference Control="~/WKF/FormManagement/VersionFieldUserControl/VersionFieldUC.ascx" %>

<table class="PopTable" style="width:600px">
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="訂單編號"></asp:Label>

        </td>
        <td>
            <asp:Button ID="btnSelectOrder" runat="server" Text="選擇訂單" OnClick="btnSelectOrder_Click" />
            <asp:Label ID="lblOrderId" runat="server" Text=""></asp:Label>
        </td>
         <td>
     <asp:Label ID="Label4" runat="server" Text="客戶代號"></asp:Label>
 </td>
 <td>
     <asp:Label ID="lblCustomerID" runat="server" Text=""></asp:Label>

 </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Text="訂單日期"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblOrderDate" runat="server" Text=""></asp:Label>

        </td>
        <td>
            <asp:Label ID="Label3" runat="server" Text="需求日期"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblRequiredDate" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>

<asp:Label ID="lblHasNoAuthority" runat="server" Text="無填寫權限" ForeColor="Red" Visible="False" meta:resourcekey="lblHasNoAuthorityResource1"></asp:Label>
<asp:Label ID="lblToolTipMsg" runat="server" Text="不允許修改(唯讀)" Visible="False" meta:resourcekey="lblToolTipMsgResource1"></asp:Label>
<asp:Label ID="lblModifier" runat="server" Visible="False" meta:resourcekey="lblModifierResource1"></asp:Label>
<asp:Label ID="lblMsgSigner" runat="server" Text="填寫者" Visible="False" meta:resourcekey="lblMsgSignerResource1"></asp:Label>
<asp:Label ID="lblAuthorityMsg" runat="server" Text="具填寫權限人員" Visible="False" meta:resourcekey="lblAuthorityMsgResource1"></asp:Label>