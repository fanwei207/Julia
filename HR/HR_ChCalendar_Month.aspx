<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HR_ChCalendar_Month.aspx.cs" Inherits="HR_HR_ChCalendar_Month" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="../css/superTables.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/superTables.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/superTable.js" type="text/javascript"></script>
    <script type="text/javascript">
            $(function () {
                $("#gv_hac").toSuperTable({ width: "1000px", height: "460px", fixedCols: 5, headerRows: 1 })
            .find("tr:even").addClass("altRow");
            });
    </script>
</head>
<body>
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="1000">
            <tr>
                <td style="height: 23px">
                    部门<asp:DropDownList ID="ddl_dp" runat="server" Width="100px" AutoPostBack="false"
                        DataTextField="name" DataValueField="departmentID">
                    </asp:DropDownList>
                    &nbsp;&nbsp; &nbsp; 工号<asp:TextBox ID="txb_userno" runat="server" Width="50" TabIndex="3"
                        Height="22"></asp:TextBox>
                    &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; 年<asp:TextBox ID="txb_year" runat="server" Width="50"
                        TabIndex="3" Height="22" MaxLength="4" Style="ime-mode: disabled" onkeypress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false;"></asp:TextBox>
                </td>
                <td align="right" style="height: 23px">
                    <asp:Button ID="btn_search" runat="server" Width="60px" CssClass="SmallButton3" Text="查询"
                        TabIndex="4" OnClick="btn_search_Click"></asp:Button>&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_export" runat="server" Width="60px" CssClass="SmallButton3" Text="导出"
                        TabIndex="4" OnClick="btn_export_Click"></asp:Button>&nbsp;&nbsp; &nbsp;&nbsp;
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv_hac" runat="server" PageSize="30" AllowPaging="true" AutoGenerateColumns="False"
            CssClass="GridViewStyle"  Width="1000px" OnPageIndexChanging="gv_hac_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" Font-Names="Tahoma,Arial" Font-Size="8pt" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="dname" HeaderText="部门">
                    <ItemStyle HorizontalAlign="Center" Width="140px" />
                </asp:BoundField>
                <asp:BoundField DataField="usernO" HeaderText="工号 ">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="username" HeaderText="姓名">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="uyear" HeaderText="年">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="1" HeaderText="1月">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="2" HeaderText="2月">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="3" HeaderText="3月">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="4" HeaderText="4月">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="5" HeaderText="5月">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="6" HeaderText="6月">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="7" HeaderText="7月">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="8" HeaderText="8月">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="9" HeaderText="9月">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="10" HeaderText="10月">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="11" HeaderText="11月">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="12" HeaderText="12月">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="total" HeaderText="小计">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
