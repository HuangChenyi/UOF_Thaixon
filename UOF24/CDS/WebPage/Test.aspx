<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="CDS_WebPage_Test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script>

        function CheckedData(source, arguments) {


            
            var LotQty = $find("<%=rnumLotQty.ClientID%>").get_value();
            var NGQty = $find("<%=rnumNGQty.ClientID%>").get_value();
            var Qty = $find("<%=rnumQty.ClientID%>").get_value();

            if (Qty + NGQty > LotQty) {
                arguments.IsValid = false;
                return;
            }
            else {
                arguments.IsValid = true;
                return;
            }
           
        }

    </script>

    <table class="PopTable" style="width:600px">
        <tr>
            <td>LotQty</td>
            <td>
                <telerik:RadNumericTextBox Value="0"
                    id="rnumLotQty" runat="server"></telerik:RadNumericTextBox>

            </td>
        </tr>
        <tr>
            <td>Qty</td>
            <td>
                <telerik:RadNumericTextBox Value="0"
                    id="rnumQty" runat="server"></telerik:RadNumericTextBox>

            </td>
            </tr>
                <tr>
            <td>NGQty</td>
            <td>
                <telerik:RadNumericTextBox Value="0"
                    id="rnumNGQty" runat="server"></telerik:RadNumericTextBox>

            </td>
            </tr>
    </table>
    <asp:CustomValidator ID="CustomValidator1" runat="server"
        ClientValidationFunction="CheckedData" Display="Dynamic"
        ErrorMessage="Your Msg"></asp:CustomValidator>
    <asp:Button ID="btnAdd" runat="server" Text="Add" CausesValidation="false"
        OnClick="btnAdd_Click" />
</asp:Content>

