<%@ Page Language="C#" AutoEventWireup="true" CodeFile="soque_track.aspx.cs" Inherits="plan_soque_track" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <table style=" width:990px">
                <tr>
                    <td style="width:60px"> SO：</td>
                    <td align="left" style="width:120px">
                        <asp:TextBox ID="txtSoStart" runat="server" Width="100px"></asp:TextBox>
                        <asp:TextBox ID="txtSoEnd" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td style="width:40px">Order：</td>
                    <td align="left" style="width:120px">
                        <asp:TextBox ID="txtPoNbrStart" runat="server" Width="100px"></asp:TextBox>
                        <asp:TextBox ID="txtPoNbrStartEnd" runat="server" Width="100px"></asp:TextBox>

                    </td>
                    <td style="width:40px">QAD：</td>
                    <td align="left" style="width:130px">
                        <asp:TextBox ID="txtQADStart" runat="server" Width="120px"></asp:TextBox>
                        <asp:TextBox ID="txtQADEnd" runat="server" Width="120px"></asp:TextBox>

                    </td>
                    <td align="left">
                    <asp:Button ID="bntSearch" runat="server" CssClass="SmallButton3" Text="Qeury" onclick="bntSearch_Click" 
                             />
                        <asp:Button ID="btnExport" runat="server" CssClass="SmallButton3" Text="Export" 
                            onclick="btnExport_Click" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                Width="990px" PageSize="25" OnRowCreated="gv_RowCreated" AllowPaging="true" 
                onpageindexchanging="gv_PageIndexChanging" >
                <RowStyle CssClass="GridViewRowStyle" Wrap="false" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table1" Width="990px" CellPadding="0" CellSpacing="0" runat="server" Font-Size="8"
                        CssClass="GridViewHeaderStyle" HorizontalAlign="Center" VerticalAlign="Middle" GridLines="Vertical">
                        <asp:TableRow Height="27px">
                            <asp:TableCell Text="So Info"></asp:TableCell>
                            <asp:TableCell Text="EDI Info" ColumnSpan="5"></asp:TableCell>
                            <asp:TableCell Text="Details" ColumnSpan="4"></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow Height="27px">
                            <asp:TableCell Text="SO" Width="100px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Order" Width="100px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Line" Width="30px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Cust Part" Width="150px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="QAD" Width="100px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Remarks" Width="100px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Detail" Width="150px" HorizontalAlign="Center"></asp:TableCell>
                            <asp:TableCell Text="User Name" Width="100px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Comfirm Date" Width="80px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Due Date" Width="80px" HorizontalAlign="Center"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField HeaderText="SO" DataField="so_po">
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Order" DataField="soque_nbr">
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Line" DataField="soque_line">
                        <HeaderStyle Width="30px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Cust Part" DataField="soque_cus_part">
                        <HeaderStyle Width="150px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="QAD" DataField="soque_part">
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Remarks" DataField="soque_remarks">
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Detail" DataField="soques_step">
                        <HeaderStyle Width="150px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="User Name" DataField="userName">
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Comfirm Date" DataField="soques_confirmDate" DataFormatString="{0:yyyy-MM-dd}"
                        HtmlEncode="False">
                        <HeaderStyle Width="80px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Request Date" DataField="reqDate" DataFormatString="{0:yyyy-MM-dd}"
                        HtmlEncode="False">
                        <HeaderStyle Width="80px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    
                </Columns>
            </asp:GridView>
    
    </div>
    </form>
</body>
</html>
