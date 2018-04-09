<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wl_calendar_pivot.aspx.cs"
    Inherits="wl_calendar" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="../css/superTables.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
   <%-- <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>--%>
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
                    &nbsp; &nbsp; 类型<asp:DropDownList ID="ddl_type" runat="server" AutoPostBack="false"
                        Width="60px">
                        <asp:ListItem Selected="true" Value="0">--</asp:ListItem>
                        <asp:ListItem Selected="false" Value="394">A类</asp:ListItem>
                        <asp:ListItem Selected="false" Value="395">B类</asp:ListItem>
                        <asp:ListItem Selected="false" Value="396">C类</asp:ListItem>
                        <asp:ListItem Selected="false" Value="397">D类</asp:ListItem>
                        <asp:ListItem Selected="false" Value="398">E类</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp; &nbsp; 工号<asp:TextBox ID="txb_userno" runat="server" Width="50" TabIndex="3"
                        Height="22"></asp:TextBox>
                    &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; 年<asp:TextBox ID="txb_year" runat="server" Width="50"
                        TabIndex="3" Height="22" MaxLength="4" Style="ime-mode: disabled" onkeypress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false;"></asp:TextBox>
                    月<asp:TextBox ID="txb_month" runat="server" Width="30" TabIndex="3" Height="22" MaxLength="2"
                        Style="ime-mode: disabled" onkeypress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false;"></asp:TextBox>
                </td>
                <td align="right" style="height: 23px">
                    <asp:Button ID="btn_search" runat="server" Width="60px" CssClass="SmallButton3" Text="查询"
                        TabIndex="4" OnClick="btn_search_Click"></asp:Button>&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_export" runat="server" Width="60px" CssClass="SmallButton3" Text="导出"
                        TabIndex="4" OnClick="btn_export_Click"></asp:Button>&nbsp;&nbsp; &nbsp;&nbsp;
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv_hac" runat="server" AutoGenerateColumns="False" OnRowDataBound="gv_pv_RowDataBound"
            CssClass="GridViewStyle"  Width="2800px" >
            <RowStyle CssClass="GridViewRowStyle" Font-Names="Tahoma,Arial" Font-Size="8pt" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
