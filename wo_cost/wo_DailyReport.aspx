<%@ Page Language="VB" AutoEventWireup="false" CodeFile="wo_DailyReport.aspx.vb"
    Inherits="wo_cost_wo_DailyReport" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="980">
            <tr>
                <td>
                    完工日期<asp:TextBox ID="txtDate" runat="server" Width="80" TabIndex="1" Height="22"
                        CssClass="Date"></asp:TextBox>&nbsp; 加工单号<asp:TextBox ID="txtWonbr" runat="server"
                            Width="100" TabIndex="2" Height="22"></asp:TextBox>&nbsp; 加工单ID<asp:TextBox ID="txtWoid"
                                runat="server" Width="100" TabIndex="3" Height="22"></asp:TextBox>&nbsp;
                    成本中心:<asp:TextBox ID="txtCenter" runat="server" Width="130" TabIndex="4" Height="22"></asp:TextBox>
                    &nbsp; 零件号:
                    <asp:TextBox ID="txtPart" runat="server" Width="120px" TabIndex="5" Height="22" MaxLength="14"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" Width="60px" TabIndex="6" CssClass="SmallButton3" />
                </td>
            </tr>
            <tr>
                <td>
                    <b>总工费 :
                    <asp:Label ID="lblTotalqty" runat="server" Font-Size="Small" ForeColor="red"></asp:Label>
                    &nbsp;&nbsp; 总工时 :<asp:Label ID="lblTotalcost" runat="server" Font-Size="Small" 
                        ForeColor="red"></asp:Label>
                    &nbsp;&nbsp; 生产人数A :<asp:Label ID="lblPeopleA" runat="server" Font-Size="Small" 
                        ForeColor="red"></asp:Label>
                    &nbsp;&nbsp; 辅助人员B :<asp:Label ID="lblPeopleB" runat="server" Font-Size="Small" 
                        ForeColor="red"></asp:Label>
                    &nbsp;&nbsp; 总人数:
                    <asp:Label ID="lblPeople" runat="server" Font-Size="Small" ForeColor="red"></asp:Label>
                    </b>
                </td>
                <td align="right">
                    &nbsp;</td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="980px" AutoGenerateColumns="False"
            AllowPaging="true" PageSize="22" CssClass="GridViewStyle AutoPageSize">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="site" HeaderText="地点" ItemStyle-Width="100" Visible="false">
                    <ItemStyle Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_type" HeaderText="工单类型" ItemStyle-Width="40" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_nbr" HeaderText="加工单号" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_ID" HeaderText="加工单ID" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_part" HeaderText="零件号" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Costcenter" HeaderText="成本中心" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="qty" HeaderText="汇报总工费" ItemStyle-Width="100" Visible="false">
                    <ItemStyle Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_cost" HeaderText="汇报总工时" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_date" HeaderText="汇报日期" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="apeople" HeaderText="生产人数A" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="bpeople" HeaderText="辅助人员B" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Right">
                    <ItemStyle HorizontalAlign="Right" Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="bpeople" HeaderText="行政人员" ItemStyle-Width="100" Visible="false">
                    <ItemStyle Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="people" HeaderText="人员总数" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        <asp:DropDownList ID="dropCenter" runat="server" Visible="false">
        </asp:DropDownList>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
