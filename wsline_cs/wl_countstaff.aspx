<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wl_countstaff.aspx.cs" Inherits="wl_calendar" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="../css/superTables.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/superTables.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/superTable.js" type="text/javascript"></script>
    <style type="text/css">
        .altRow
        {
            background-color: #ddddff;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#gv_hac").toSuperTable({ width: "1000px", height: "460px", fixedCols: 2, headerRows: 2 })
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
                    部门<asp:DropDownList ID="ddl_dp" runat="server" Width="100px" AutoPostBack="True"
                        DataTextField="name" DataValueField="departmentID" OnSelectedIndexChanged="ddl_dp_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp; &nbsp; 类型<asp:DropDownList ID="ddl_type" runat="server" AutoPostBack="false"
                        Width="60px" DataTextField="systemCodeName" DataValueField="usertype">
                        <asp:ListItem Value="-1">----</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp;年<asp:TextBox ID="txb_year" runat="server"
                        Width="50" TabIndex="3" Height="22" MaxLength="4" Style="ime-mode: disabled"
                        onkeypress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false;"></asp:TextBox>&nbsp;
                </td>
                <td align="right" style="height: 23px">
                    <asp:Button ID="btn_search" runat="server" Width="30" CssClass="SmallButton3" Text="查询"
                        TabIndex="4" OnClick="btn_search_Click"></asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="BtnExcel" runat="server" Width="30" CssClass="SmallButton3" Text="Excel"
                        TabIndex="5" OnClick="BtnExcel_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv_hac" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            OnRowDataBound="gv_hac_RowDataBound" OnRowCreated="gv_hac_RowCreated">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        </asp:GridView>
        </form>
    </div>
    <script>
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
