<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EdiApproveSummary.aspx.cs" Inherits="EDI_EdiApproveSummary" %>

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
    <div>
    <table>
        <tr>
            <td style="width:400px;">
                正式订单未进系统套数：
            <asp:Label ID="lblTotal" runat="server"></asp:Label>套
                
            </td>
            <td align="Left">
                <asp:Button ID="btnRefresh" runat="server" Text="刷新" CssClass="SmallButton2" OnClick="btnRefresh_Click" />
            </td>
            <td></td>
        </tr>
        <tr>

            <td>
                非正式订单未进系统套数：
            <asp:Label ID="lblTotalR19" runat="server"></asp:Label>套
            </td>
        </tr>
        <tr>
            <td colspan="2">
                 <asp:GridView ID="gvlist" runat="server" AllowPaging="false" AutoGenerateColumns="False"  Width="1200px" 
                        CssClass="GridViewStyle">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                    <asp:BoundField DataField="Node_Name" HeaderText="审批节点">
                    <ItemStyle HorizontalAlign="left" Width="50px" />
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="runCount" HeaderText="正式订单未完成行数">
                    <ItemStyle HorizontalAlign="Right" Width="50px" />
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="runTotal" HeaderText="正式订单未完成套数">
                    <ItemStyle HorizontalAlign="Right" Width="50px" />
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="failCount" HeaderText="正式订单拒绝行数">
                    <ItemStyle HorizontalAlign="Right" Width="50px" />
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="failTotal" HeaderText="正式订单拒绝套数">
                    <ItemStyle HorizontalAlign="Right" Width="50px" />
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                 <asp:BoundField DataField="runCountR19" HeaderText="非正式订单未完成行数">
                    <ItemStyle HorizontalAlign="Right" Width="50px" />
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="runTotalR19" HeaderText="非正式订单未完成套数">
                    <ItemStyle HorizontalAlign="Right" Width="50px" />
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="failCountR19" HeaderText="非正式订单拒绝行数">
                    <ItemStyle HorizontalAlign="Right" Width="50px" />
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="failTotalR19" HeaderText="非正式订单拒绝套数">
                    <ItemStyle HorizontalAlign="Right" Width="50px" />
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                        </Columns>
                    </asp:GridView>

            </td>

        </tr>
    </table>
    </div>
    </form>
</body>
</html>
