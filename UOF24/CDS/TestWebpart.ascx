<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TestWebpart.ascx.cs" Inherits="CDS_TestWebpart" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="System.Web.UI" TagPrefix="cc1" %>
<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Fast" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Contenttemplate>
        <telerik:RadToolBar ID="RadToolBar1" Runat="server" Width="100%">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Button 0" Value="ChooseArea">
                <ItemTemplate>
                    <table style="margin-right:2px">
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="地區"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                 <asp:DropDownList ID="ddlDailyArea" runat="server"
                                    AutoPostBack="True" 
                                    OnSelectedIndexChanged="ddlDailyArea_SelectedIndexChanged">
                                    <asp:ListItem Text="臺北市" Value="36_01.xml"></asp:ListItem>
                                    <asp:ListItem Text="高雄市" Value="36_02.xml"></asp:ListItem>
                                    <asp:ListItem Text="基隆市" Value="36_03.xml"></asp:ListItem>
                                    <asp:ListItem Text="新北市" Value="36_04.xml"></asp:ListItem>
                                    <asp:ListItem Text="桃園市" Value="36_05.xml"></asp:ListItem>
                                    <asp:ListItem Text="新竹縣" Value="36_06.xml"></asp:ListItem>
                                    <asp:ListItem Text="苗栗縣" Value="36_07.xml"></asp:ListItem>
                                    <asp:ListItem Text="臺中市" Value="36_08.xml"></asp:ListItem>
                                    <asp:ListItem Text="彰化縣" Value="36_09.xml"></asp:ListItem>
                                    <asp:ListItem Text="南投縣" Value="36_10.xml"></asp:ListItem>
                                    <asp:ListItem Text="雲林縣" Value="36_11.xml"></asp:ListItem>
                                    <asp:ListItem Text="嘉義縣" Value="36_12.xml"></asp:ListItem>
                                    <asp:ListItem Text="臺南市" Value="36_13.xml"></asp:ListItem>
                                    <asp:ListItem Text="新竹市" Value="36_14.xml"></asp:ListItem>
                                    <asp:ListItem Text="屏東縣" Value="36_15.xml"></asp:ListItem>
                                    <asp:ListItem Text="嘉義市" Value="36_16.xml"></asp:ListItem>
                                    <asp:ListItem Text="宜蘭縣" Value="36_17.xml"></asp:ListItem>
                                    <asp:ListItem Text="花蓮縣" Value="36_18.xml"></asp:ListItem>
                                    <asp:ListItem Text="臺東縣" Value="36_19.xml"></asp:ListItem>
                                    <asp:ListItem Text="澎湖縣" Value="36_20.xml"></asp:ListItem>
                                    <asp:ListItem Text="金門縣" Value="36_21.xml"></asp:ListItem>
                                    <asp:ListItem Text="連江縣" Value="36_22.xml"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton runat="server" Text="Button 1" IsSeparator="true">
            </telerik:RadToolBarButton>
        </Items>
    </telerik:RadToolBar>
        <Fast:Grid ID="Grid1" runat="server" AllowSorting="True" AutoGenerateCheckBoxColumn="False"
    AutoGenerateColumns="False"  
    ShowHeader="False"  Width="100%" BorderStyle="None" 
            BorderWidth="0px" OnRowDataBound="Grid1_RowDataBound" 
            SkinID="HomepageBlockStyle"  
            DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" 
            EnhancePager="True" PageSize="15" 
    EmptyDataText="沒有資料" EnableModelValidation="True" 
    KeepSelectedRows="False">
      <EnhancePagerSettings ShowHeaderPager="True" />
      <ExportExcelSettings AllowExportToExcel="False" /><Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <table style="vertical-align:middle" width="100%">
                    <tr id="header" runat="server" style="background-color:White; border-bottom-width:thin; border-bottom-style:solid">
                        <td runat="server">
                        </td>
                        <td style="width:25%" runat="server">
                            <asp:Label ID="lblDate" runat="server"></asp:Label>
                        </td>
                        <td style="width:35%" runat="server">
                             <asp:Label ID="Label2" runat="server" Text="天氣狀況"></asp:Label>
                        </td>
                        <td style="width:25%" runat="server">
                            <asp:Label ID="Label3" runat="server" Text="溫度(℃)"></asp:Label>
                        </td>
                        <td style="width:15%" runat="server">
                            <asp:Label ID="Label4" runat="server" Text="降雨機率"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="min-width:16px">
                            <img height="16px" src="<%=ResolveUrl("~/common/images/icon/icon02.png")%>" width="16px" /> 
                        </td>
                        <td style="width:25%">
                            <asp:Label ID="lblTime" runat="server"></asp:Label>
                        </td>
                        <td style="width:35%">
                            <asp:Image ID="imgStatus" runat="server" Height="25px"  Width="25px" 
                                ImageAlign="Middle"/>
                             <asp:Label ID="lblStatus" runat="server"></asp:Label>
                         
                        </td>
                        <td style="width:25%">
                            <asp:Label ID="lblTemperature" runat="server"></asp:Label>
                        </td>
                        <td style="width:15%">
                            <asp:Label ID="lblRain" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</Fast:Grid>
        <table width="100%">
    <tr>
        <td>
            <asp:Label ID="lblErrorMessage" runat="server" Text="發生錯誤，錯誤原因如下:{0}" 
                Visible="False"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="lbtnSourceWeb" runat="server" Text="請點此連結觀看氣象署網頁"  
                Visible="False"></asp:LinkButton>
        </td>
    </tr>
</table>
        <asp:HiddenField ID="hfCurentUser" runat="server" />
    </Contenttemplate>
</asp:UpdatePanel>