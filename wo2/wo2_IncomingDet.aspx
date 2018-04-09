<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_IncomingDet.aspx.cs" Inherits="wo2_wo2_IncomingDet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
            <table>
                <tr>
                    <td>工单号：<asp:Label ID="lbNbr" runat="server"></asp:Label>
                    </td>
                    <td>ID号：<asp:Label ID="lbLot" runat="server"></asp:Label>
                    </td>
                    <td>QAD：<asp:Label ID="lbQAD" runat="server"></asp:Label>
                    </td>
                    <td>
                        缺陷类：<asp:Label ID="lbtype" runat="server"></asp:Label>
                    </td>
                    <td>供应商：<asp:Label ID="lbVender" runat="server"></asp:Label>
                    </td>
                    <td>供应商名称：<asp:Label ID="lbVenderName" runat="server"></asp:Label>
                    </td>
                    <td>检测数量：<asp:Label ID="lbNum" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:GridView ID="gvInfo" runat="server" CssClass="GridViewStyle"
                            AllowPaging="true" AutoGenerateColumns="false">
                            <RowStyle CssClass="GridViewRowStyle" />
                            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <Columns>

                                 <asp:BoundField HeaderText="批次号" DataField="wo_batch">
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                     <asp:BoundField HeaderText="批次数量" DataField="wo_checkNum">
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="缺陷名" DataField="proName">
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                
                                <asp:BoundField HeaderText="缺陷数" DataField="wo_num">
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="明细" DataField="det">
                                    <HeaderStyle Width="300px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                 
                               

                            </Columns>
                        </asp:GridView>


                    </td>
                </tr>

            </table>
        </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
