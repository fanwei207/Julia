﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WH_SIDdet.aspx.cs" Inherits="plan_WH_SIDdet" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <base target="_self">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div align="Center">
            <asp:Label ID="Label1" runat="server" Font-Size="12pt" Text=""></asp:Label>
            <table>
                <tr>
                    <td align="left">
                        <table>
                            <tr>
                                <td align="left">域:</td>
                                <td>
                                    <asp:TextBox ID="txtdomain" runat="server" ReadOnly="True"></asp:TextBox></td>
                                <td>地点:</td>
                                <td>
                                    <asp:TextBox ID="txtshipto" runat="server" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">PK号:</td>
                                <td>
                                    <asp:TextBox ID="txtPK" runat="server" ReadOnly="True"></asp:TextBox></td>
                                <td>发票号:</td>
                                <td>
                                    <asp:TextBox ID="txtnbr" runat="server" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                    <td align="left">理由:</td>
                    <td colspan="4">
                        <asp:TextBox ID="txt_reason" runat="server" Width="90%"></asp:TextBox>
                    </td>
                    <td><font face=宋体 color=crimson size=2>*取消时填写理由</font></td>
                </tr>
                        </table>
                    </td>
                    <td>
                        <asp:Button ID="btn_update" runat="server" Text="提交" OnClick="btn_update_Click" Width="69px" Height="36px" />
                    
                        <asp:Button ID="btn_cancel" runat="server" Text="取消" Width="69px" Height="36px" OnClick="btn_cancel_Click"  />
                    </td>
                </tr>
                
            </table>
            <table>
                <tr>
                    <td>
                        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                            Width="1230px" DataKeyNames="" OnRowDataBound="gv_RowDataBound">
                            <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                            <EmptyDataTemplate>
                                <asp:Table ID="Table1" Width="980px" CellPadding="-1" CellSpacing="0" runat="server"
                                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                    <asp:TableRow>
                                        <asp:TableCell Text="任务描述" Width="610px" HorizontalAlign="center"></asp:TableCell>
                                        <asp:TableCell Text="申请人" Width="60px" HorizontalAlign="center"></asp:TableCell>
                                        <asp:TableCell Text="跟踪人" Width="60px" HorizontalAlign="center"></asp:TableCell>
                                        <asp:TableCell Text="创建人" Width="60px" HorizontalAlign="center"></asp:TableCell>
                                        <asp:TableCell Text="创建时间" Width="90px" HorizontalAlign="center"></asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="SID_so_nbr" HeaderText="销售单">
                                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                    <ItemStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SID_so_line" HeaderText="行号">
                                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                    <ItemStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SID_QAD" HeaderText="物料号">
                                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                    <ItemStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:BoundField>
                                <asp:BoundField DataField="desc1" HeaderText="描述">
                                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                    <ItemStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:BoundField>
                                <asp:BoundField DataField="wh_loc" HeaderText="库位">
                                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                    <ItemStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:BoundField>
                                <asp:BoundField DataField="wh_serial" HeaderText="批次">
                                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                    <ItemStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:BoundField>
                                <asp:BoundField DataField="wh_qty_loc" HeaderText="出运数">
                                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                    <ItemStyle Width="60px" HorizontalAlign="right" VerticalAlign="Top" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SID_qty_all" HeaderText="出运总数">
                                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                    <ItemStyle Width="60px" HorizontalAlign="right" VerticalAlign="Top" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SID_qty_set" HeaderText="套数">
                                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                    <ItemStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SID_qty_pcs" HeaderText="只数">
                                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                    <ItemStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SID_qty_box" HeaderText="箱数">
                                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                    <ItemStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td></td>
                </tr>
                <tr align="center">
                    <td align="left"></td>
                    <td></td>
                </tr>
            </table>
        </div>
    </form>
    <script>
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
