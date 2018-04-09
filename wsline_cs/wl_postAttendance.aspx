<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wl_postAttendance.aspx.cs" Inherits="wsline_cs_wl_postAttendance" %>

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
    <style type="text/css">
        .altRow
        {
            background-color: #ddddff;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#gvCalendarCheck").toSuperTable({ width: "1006px", height: "480px", fixedCols: 9, headerRows: 2 })
            .find("tr:even").addClass("altRow");
        });
    </script>
</head>
<body>
    <div>
        <form id="Form1" method="post" runat="server">
        <div style="width: 1000px; text-align: left; height: 30px;
            position: absolute; margin-left: 5px; padding: 2px;">
            <table id="table1" cellspacing="0" cellpadding="0" width="1000">
                <tr>
                    <td style="height: 23px; width:500px">
                        部门<asp:DropDownList ID="ddl_dp" runat="server" Width="100px" AutoPostBack="True"
                            DataTextField="name" DataValueField="departmentID" OnSelectedIndexChanged="ddl_dp_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;&nbsp; &nbsp; 工段<asp:DropDownList ID="dropWorkShop" runat="server" Width="100px">
                        </asp:DropDownList>
                        &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; 年<asp:DropDownList ID="dropYears" Width="70px"
                            runat="server">
                        </asp:DropDownList>
                        &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; 月<asp:DropDownList ID="dropMonths" Width="50px"
                            runat="server">
                        </asp:DropDownList>
                    </td>
                    <td align="left" style="height: 23px">
                        <asp:Button ID="btn_search" runat="server" Width="30" CssClass="SmallButton3" Text="查询"
                            TabIndex="4" OnClick="btn_search_Click"></asp:Button>&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_export" runat="server" Width="30" CssClass="SmallButton3" Text="导出"
                            TabIndex="4" OnClick="btn_export_Click"></asp:Button>&nbsp;&nbsp; &nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div style="position: absolute; margin-left: 0px; margin-top: 25px; width: 1002px;">
            <asp:GridView ID="gvCalendarCheck" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                Width="2000px" PageSize="25" OnPageIndexChanging="gvCalendarCheck_PageIndexChanging"
                OnRowCreated="gvCalendarCheck_RowCreated" OnRowDataBound="gvCalendarCheck_RowDataBound">
                <RowStyle CssClass="GridViewRowStyle" Wrap="false" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table1" Width="1000px" CellPadding="0" CellSpacing="0" runat="server"
                        BackColor="#006699" Font-Bold="True" Font-Names="Tahoma,Arial" Font-Size="8pt"
                        ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Text="部门" Width="90px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="在职" Width="90px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="出勤" Width="90px" HorizontalAlign="center"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField HeaderText="部门" DataField="deptName">
                        <HeaderStyle Width="120px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="工段" DataField="workshopName">
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A1">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B1">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A2">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B2">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A3">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B3">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A4">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B4">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A5">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B5">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A6">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B6">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A7">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B7">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A8">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B8">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A9">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B9">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A10">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B10">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A11">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B11">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A12">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B12">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A13">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B13">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A14">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B14">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A15">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B15">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A16">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B16">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A17">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B17">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A18">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B18">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A19">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B19">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A20">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B20">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A21">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B21">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A22">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B22">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A23">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B23">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A24">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B24">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A25">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B25">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A26">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B26">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A27">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B27">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A28">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B28">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A29">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B29">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A30">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B30">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="在职" DataField="A31">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="出勤" DataField="B31">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
        </form>
    </div>
    <script>
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>