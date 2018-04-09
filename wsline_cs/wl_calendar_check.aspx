<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wl_calendar_check.aspx.cs"
    Inherits="wsline_cs_wl_calendar_check" %>

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
                    <td style="height: 23px">
                        ����<asp:DropDownList ID="ddl_dp" runat="server" Width="100px" AutoPostBack="True"
                            DataTextField="name" DataValueField="departmentID" OnSelectedIndexChanged="ddl_dp_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp; &nbsp; ����<asp:DropDownList ID="ddl_type" runat="server" AutoPostBack="false"
                            Width="60px">
                            <asp:ListItem Selected="true" Value="0">--</asp:ListItem>
                            <asp:ListItem Selected="false" Value="394">A��</asp:ListItem>
                            <asp:ListItem Selected="false" Value="395">B��</asp:ListItem>
                            <asp:ListItem Selected="false" Value="396">C��</asp:ListItem>
                            <asp:ListItem Selected="false" Value="397">D��</asp:ListItem>
                            <asp:ListItem Selected="false" Value="398">E��</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp; &nbsp; ����<asp:DropDownList ID="dropWorkShop" runat="server" Width="100px">
                        </asp:DropDownList>
                        &nbsp;&nbsp; &nbsp; ����<asp:TextBox ID="txb_userno" runat="server" Width="70px" TabIndex="3"
                            Height="22"></asp:TextBox>
                        &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; ��<asp:DropDownList ID="dropYears" Width="70px"
                            runat="server">
                        </asp:DropDownList>
                        &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; ��<asp:DropDownList ID="dropMonths" Width="50px"
                            runat="server">
                        </asp:DropDownList>
                    </td>
                    <td align="right" style="height: 23px">
                        <asp:Button ID="btn_search" runat="server" Width="30" CssClass="SmallButton3" Text="��ѯ"
                            TabIndex="4" OnClick="btn_search_Click"></asp:Button>&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_export" runat="server" Width="30" CssClass="SmallButton3" Text="����"
                            TabIndex="4" OnClick="btn_export_Click"></asp:Button>&nbsp;&nbsp; &nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div style="position: absolute; margin-left: 0px; margin-top: 25px; width: 1002px;">
            <asp:GridView ID="gvCalendarCheck" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                Width="3000px" PageSize="25" OnPageIndexChanging="gvCalendarCheck_PageIndexChanging"
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
                            <asp:TableCell Text="����" Width="90px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="����" Width="90px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="����" Width="90px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="��˾����" Width="90px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="��Ա����" Width="90px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="�Ƴ귽ʽ" Width="90px" HorizontalAlign="Center"></asp:TableCell>
                            <asp:TableCell Text="����" Width="90px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="��������" Width="90px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="��ְ����" Width="90px" HorizontalAlign="Center"></asp:TableCell>
                            <asp:TableCell Text="�ϰ�" Width="90px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="�°�" Width="90px" HorizontalAlign="center"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField HeaderText="����" DataField="DepartmentName">
                        <HeaderStyle Width="120px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="����" DataField="userNo">
                        <HeaderStyle Width="70px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="����" DataField="userName">
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="��˾����" DataField="enterDate" DataFormatString="{0:yyyy-MM-dd}"
                        HtmlEncode="False">
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="��Ա����" DataField="UserTypeName">
                        <HeaderStyle Width="50px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�Ƴ귽ʽ" DataField="workTypeName">
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="����" DataField="WorkShopName">
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="��������" DataField="checkDays">
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="��ְ����" DataField="leaveDate" DataFormatString="{0:yyyy-MM-dd}"
                        HtmlEncode="False">
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A1">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B1">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A2">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B2">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A3">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B3">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A4">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B4">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A5">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B5">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A6">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B6">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A7">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B7">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A8">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B8">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A9">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B9">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A10">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B10">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A11">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B11">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A12">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B12">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A13">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B13">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A14">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B14">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A15">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B15">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A16">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B16">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A17">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B17">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A18">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B18">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A19">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B19">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A20">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B20">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A21">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B21">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A22">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B22">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A23">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B23">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A24">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B24">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A25">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B25">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A26">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B26">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A27">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B27">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A28">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B28">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A29">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B29">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A30">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B30">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�ϰ�" DataField="A31">
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�°�" DataField="B31">
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
