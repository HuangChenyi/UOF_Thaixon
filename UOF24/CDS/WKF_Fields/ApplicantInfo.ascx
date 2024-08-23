<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ApplicantInfo.ascx.cs" Inherits="WKF_OptionalFields_ApplicantInfo" %>
<%@ Reference Control="~/WKF/FormManagement/VersionFieldUserControl/VersionFieldUC.ascx" %>

<table class="PopTable" style="width:500px">
    <tr>
        <td>
        <asp:Label ID="Label1" runat="server" Text="公司別"></asp:Label>
            </td>
       <td>
        <asp:Label ID="lblCompany" runat="server" Text=""></asp:Label>
            </td>
    </tr>
    <tr>
        <td>
        <asp:Label ID="Label2" runat="server" Text="廠別"></asp:Label>
            </td>
       <td>
        <asp:Label ID="lblFactory" runat="server" Text=""></asp:Label>
            </td>
    </tr>
        <tr>
        <td>
        <asp:Label ID="Label3" runat="server" Text="部門名稱"></asp:Label>
            </td>
       <td>
        <asp:Label ID="lblDeptName" runat="server" Text=""></asp:Label>
            </td>
    </tr>
        <tr>
    <td>
    <asp:Label ID="Label4" runat="server" Text="部門編號"></asp:Label>
        </td>
   <td>
    <asp:Label ID="lblDeptCode" runat="server" Text=""></asp:Label>
        </td>
</tr>
            <tr>
    <td>
    <asp:Label ID="Label5" runat="server" Text="員工編號"></asp:Label>
        </td>
   <td>
    <asp:Label ID="lblEmployeeNo" runat="server" Text=""></asp:Label>
        </td>
</tr>
</table>

<asp:Label ID="lblHasNoAuthority" runat="server" Text="無填寫權限" ForeColor="Red" Visible="False" meta:resourcekey="lblHasNoAuthorityResource1"></asp:Label>
<asp:Label ID="lblToolTipMsg" runat="server" Text="不允許修改(唯讀)" Visible="False" meta:resourcekey="lblToolTipMsgResource1"></asp:Label>
<asp:Label ID="lblModifier" runat="server" Visible="False" meta:resourcekey="lblModifierResource1"></asp:Label>
<asp:Label ID="lblMsgSigner" runat="server" Text="填寫者" Visible="False" meta:resourcekey="lblMsgSignerResource1"></asp:Label>
<asp:Label ID="lblAuthorityMsg" runat="server" Text="具填寫權限人員" Visible="False" meta:resourcekey="lblAuthorityMsgResource1"></asp:Label>