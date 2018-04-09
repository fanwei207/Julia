<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_SKUMaint.aspx.cs" Inherits="RDW_SKUMaint" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
            <table cellspacing="2" cellpadding="2" width="950px" bgcolor="white" border="0">
                <tr>
                    <td style="width: 42px">
                        <asp:Label ID="lblSKU" runat="server" Width="34px" CssClass="LabelRight" Text="SKU:"
                            Font-Bold="False"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSKU" runat="server" Width="120px" TabIndex="1" 
                            CssClass="SmallTextBox2 Param"></asp:TextBox>
                        <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="5" Text="Query"
                            Width="40px" OnClick="btnQuery_Click" />
                        <asp:Button ID="btnNew" runat="server" CssClass="SmallButton2" TabIndex="5" Text="New"
                            Width="40px" OnClick="btnNew_Click" />
                        <asp:Button ID="btnExcel" runat="server" CssClass="SmallButton2" TabIndex="6" Text="Excel"
                            Width="40px" OnClick="btnReport_Click" Visible="False" />
                        <asp:CheckBox ID="chk" runat="server" Visible="False" /></td>
                </tr>
            </table>
            <asp:GridView ID="gvRDW" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                CssClass="GridViewStyle GridViewRebuild" PageSize="20" OnPreRender="gvRDW_PreRender" OnPageIndexChanging="gvRDW_PageIndexChanging"
                Width="1000px" OnRowCommand="gvRDW_RowCommand" 
                    OnRowDataBound="gvRDW_RowDataBound" DataKeyNames="SKU" 
                    OnRowDeleting="gvRDW_RowDeleting">
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table1" Width="960px" CellPadding="-1" CellSpacing="0" runat="server"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Text="SKU #" Width="240px" HorizontalAlign="center" 
                                Font-Bold="false"></asp:TableCell>
                            <asp:TableCell Text="UPC #" Width="80px" HorizontalAlign="center" 
                                Font-Bold="false"></asp:TableCell>
                            <asp:TableCell Text="Voltage" Width="60px" HorizontalAlign="center" 
                                Font-Bold="false"></asp:TableCell>
                            <asp:TableCell Text="Wattage" Width="60px" HorizontalAlign="center" 
                                Font-Bold="false"></asp:TableCell>
                            <asp:TableCell Text="Lumens" Width="60px" HorizontalAlign="center" 
                                Font-Bold="false"></asp:TableCell>
                            <asp:TableCell Text="LPW" Width="60px" HorizontalAlign="center" 
                                Font-Bold="false"></asp:TableCell>
                            <asp:TableCell Text="CBCPest" Width="60px" HorizontalAlign="center" 
                                Font-Bold="false"></asp:TableCell>
                            <asp:TableCell Text="Beam Angle" Width="60px" HorizontalAlign="center" 
                                Font-Bold="false"></asp:TableCell>
                            <asp:TableCell Text="CCT" Width="60px" HorizontalAlign="center" 
                                Font-Bold="false"></asp:TableCell>
                            <asp:TableCell Text="CRI" Width="60px" HorizontalAlign="center" 
                                Font-Bold="false"></asp:TableCell>
                            <asp:TableCell Text="STK/MTO" Width="60px" HorizontalAlign="center" 
                                Font-Bold="false"></asp:TableCell>
                            <asp:TableCell Text="Case Qty" Width="60px" HorizontalAlign="center" 
                                Font-Bold="false"></asp:TableCell>
                            <asp:TableCell Text="" Width="40px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="SKU #">
                        <ItemTemplate>
                            <asp:LinkButton ID="linkSKU" runat="server" CommandArgument='<%# Bind("SKU") %>'
                                CommandName="Operation" Font-Bold="False" Font-Size="11px" Font-Underline="True"
                                ForeColor="Black" Text='<%# Bind("SKU") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="200px" />
                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="UPC" HeaderText="UPC # ">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="80px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Voltage" HeaderText="Voltage ">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Wattage" HeaderText="Wattage ">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Lumens" HeaderText="Lumens">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LPW" HeaderText="LPW">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CBCPest" HeaderText="CBCPest">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="BeamAngle" HeaderText="Beam Angle">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CCT" HeaderText="CCT">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CRI" HeaderText="CRI">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                    </asp:BoundField> 
                    <asp:BoundField DataField="CaseQty" HeaderText="Case Qty" >
                        <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                    </asp:BoundField> 
                    <asp:BoundField DataField="STKorMTO" HeaderText="STK/MTO">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:BoundField> 
                    <asp:CommandField ShowDeleteButton="True" DeleteText="Del" >
                        <ControlStyle Font-Bold="False" Font-Underline="True" ForeColor="Black" />
                        <HeaderStyle HorizontalAlign="Center" Width="40px" />
                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                    </asp:CommandField>
                     <asp:BoundField DataField="CreateUser" HeaderText="Creator"   >
                        <HeaderStyle Width="110px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="110px" HorizontalAlign="Left" />
                    </asp:BoundField> 
                </Columns>
            </asp:GridView>
        </form>
    </div>
</body>
</html>
