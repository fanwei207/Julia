<%@ Page Language="C#" AutoEventWireup="true" CodeFile="F4311.aspx.cs" Inherits="price_F4311" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="left">
        <form id="form1" runat="server">
        <table cellspacing="0" cellpadding="0" width="960" bgcolor="white" border="0">
            <tr>
                <td>
                    Order:
                </td>
                <td>
                    <asp:TextBox ID="txtOrder" runat="server" CssClass="smalltextbox" Width="115px"></asp:TextBox>
                </td>
                <td>
                    Item Number:
                </td>
                <td>
                    <asp:TextBox ID="txtItemNumber" runat="server" CssClass="smalltextbox" Width="115px"></asp:TextBox>
                </td>
                <td>
                    Requested Date:
                </td>
                <td>
                    <asp:TextBox ID="txtReqDate1" runat="server" CssClass="smalltextbox Date" Width="95px"></asp:TextBox>--<asp:TextBox
                        ID="txtReqDate2" runat="server" CssClass="smalltextbox Date" Width="95px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnExport" runat="server" Text="Excel" CssClass="SmallButton3" OnClick="btnExport_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    Order Date:
                </td>
                <td>
                    <asp:TextBox ID="txtOrdDate1" runat="server" CssClass="smalltextbox Date" Width="95px"></asp:TextBox>--<asp:TextBox
                        ID="txtOrdDate2" runat="server" CssClass="smalltextbox Date" Width="95px"></asp:TextBox>
                </td>
                <td>
                    Updated Date:
                </td>
                <td>
                    <asp:TextBox ID="txtUpdDate1" runat="server" CssClass="smalltextbox Date" Width="95px"></asp:TextBox>--<asp:TextBox
                        ID="txtUpdDate2" runat="server" CssClass="smalltextbox Date" Width="95px"></asp:TextBox>
                </td>
                <td>
                    Reference:
                </td>
                <td>
                    <asp:TextBox ID="txtRef" runat="server" CssClass="smalltextbox" Width="100%"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" Text="Query" CssClass="SmallButton3" OnClick="btnQuery_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvF4311" name="gvlist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            PageSize="21" OnPageIndexChanging="gvF4311_PageIndexChanging" Width="2500px"
            CssClass="GridViewStyle AutoPageSize">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="Order" HeaderText="Order">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="LineNumber" HeaderText="LineNumber">
                    <HeaderStyle Width="70px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="BusinessUnit" HeaderText="BusinessUnit">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ShipTo" HeaderText="ShipTo">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Requested" HeaderText="RequestedDate" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="OrderDate" HeaderText="OrderDate" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="OriginalPromised" HeaderText="OriginalPromised" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ActualShip" HeaderText="ActualShip" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="CancelDate" DataField="CancelDate" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Reference" DataField="Reference2">
                    <HeaderStyle Width="120px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="ItemNumber" DataField="ItemNumber">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="2ndItemNumber" DataField="2ndItemNumber">
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Description" DataField="Description">
                    <HeaderStyle Width="250px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="LineType" DataField="LineType">
                    <HeaderStyle Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="StatusCode-Next" DataField="StatusCode-Next">
                    <HeaderStyle Width="120px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="StatusCode-Last" DataField="StatusCode-Last">
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="U/M" DataField="U/M">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Quantity" DataField="Quantity">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="QuantityOpen" DataField="QuantityOpen">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="QuantityReceived" DataField="QuantityReceived">
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="UnitCost" DataField="UnitCost">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="PrintMessage" DataField="PrintMessage">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="FreightHandlingCode" DataField="FreightHandlingCode">
                    <HeaderStyle Width="110px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="BuyerNumber" DataField="BuyerNumber">
                    <HeaderStyle Width="90px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="TransactionOriginator" DataField="TransactionOriginator">
                    <HeaderStyle Width="110px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="UserID" DataField="UserID">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="UpdatedDate" DataField="DateUpdated" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script>
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>
