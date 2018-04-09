<%@ Page Language="C#" AutoEventWireup="true" CodeFile="oms_FSFCImport.aspx.cs" Inherits="oms_FSFCImport" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body style="text-align:center;">
    <div align="center"  >
        <form id="Form1" method="post" runat="server">
        <table cellpadding="0" cellspacing="0" width="780" style="background-color: White;
            text-align: center;" border="0">
            <tr>
                <td align="right" style="width: 90">
                    &nbsp;
                </td>
                <td valign="top" align="left" style="width: 500" colspan="2">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td style="height: 5">
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" style="width: 90" >
                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                           Customer:
                </td>
                <td style="width: 500" colspan="2" align="left" valign="bottom">
                    <asp:Label ID="lblCustCode" runat="server" ></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblCust"  runat="server"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td style="height: 5">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 90">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Import File: &nbsp; 
                </td>
                <td valign="top" style="width: 500" colspan="2"align="left" valign="bottom">
                    <input id="filename2" style="width: 369px; height: 23px" type="file" size="45" name="filename2"
                        runat="server" />
                    <asp:Button ID="btnImport" runat="server" CssClass="SmallButton2" Text="Import" Width="68px"
                        OnClick="btnImport_Click" />
                </td>
            </tr>
            <tr>
                <td style="height: 5">
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" style="width: 90; height: 21px;">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Download:</td>
                <td align="left" style="width: 500; height: 21px;" valign="bottom">
                    <label id="here" onclick="submit();">
                        <a href="../docs/Forecast.xls" target="blank"><font color="blue">Import
                            Template</font></a></label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" CssClass="SmallButton2" 
                        onclick="btnBack_Click" Text="Back" />
                </td>
                <td align="center">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="height: 5">
                </td>
            </tr>
            <tr>
                <td style="height: 5">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvForecast" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            AllowPaging="True" Width="800px" DataKeyNames="id" OnPageIndexChanging="gvForecast_PageIndexChanging">
            <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="1200px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="Customer Part" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="QAD Part" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Forecast Date" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Forecast Quantity" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Unit" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="CreatedBy" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="CreatedDate" Width="100px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="fc_cuPart" HeaderText="Customer Part">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="fc_part" HeaderText="QAD">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fc_date" HeaderText="Forecast Date">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="qty" HeaderText="Forecast Quantity">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="unit" HeaderText="Unit">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fc_createBy" HeaderText="CreatedBy">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fc_createDate" HeaderText="Created Date">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <%--<asp:TemplateField HeaderText="编辑">
            <HeaderStyle Width="100px" HorizontalAlign="Center" />
             <ItemStyle Width="100px" HorizontalAlign="Center" />
                <ItemTemplate >
                <asp:LinkButton ID="Edit1" runat="server" Font-Bold="False" Font-Size="12px"
                  CommandName="Edit1" Font-Underline="True" Text="编辑"  ></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>--%>
                <%--  <asp:CommandField  HeaderText="" ShowEditButton="True" CancelText="Cancel" UpdateText="Update" EditText="Edit">
            <HeaderStyle Width="150px" HorizontalAlign="Center" />
             <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:CommandField>
            <asp:TemplateField HeaderText="">
            <HeaderStyle Width="100px" HorizontalAlign="Center" />
             <ItemStyle Width="100px" HorizontalAlign="Center" />
                <ItemTemplate >
                <asp:LinkButton ID="Delete1" runat="server" Font-Bold="False" Font-Size="12px"
                            CommandName="Delete1" Font-Underline="True" Text="Delete"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
