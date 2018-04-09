<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wl_calendar.aspx.cs" Inherits="wl_calendar" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="1060">
            <tr>
                <td>
                    &nbsp;<asp:DropDownList ID="ddl_site" runat="server" Width="100px" AutoPostBack="True"
                        OnSelectedIndexChanged="ddl_site_SelectedIndexChanged">
                         <asp:ListItem Selected="true" Value="1">�Ϻ�ǿ�� SQL</asp:ListItem>
                        <asp:ListItem Selected="false" Value="2">��ǿ�� ZQL</asp:ListItem>
                        <asp:ListItem Selected="false" Value="5">����ǿ�� YQL</asp:ListItem>
                        <asp:ListItem Selected="false" Value="8">����ǿ�� HQL</asp:ListItem>
                        <asp:ListItem Selected="false" Value="11">�Ϻ���ӱ� TCB</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp; �ɱ�����
                    <asp:DropDownList ID="ddl_cc" runat="server" Width="90px" AutoPostBack="false">
                    </asp:DropDownList>
                    ����<asp:DropDownList ID="ddl_type" runat="server" Width="60px" AutoPostBack="True"
                        OnSelectedIndexChanged="ddl_type_SelectedIndexChanged">
                        <asp:ListItem Selected="true" Value="0">--</asp:ListItem>
                        <asp:ListItem Selected="false" Value="394">A��</asp:ListItem>
                        <asp:ListItem Selected="false" Value="395">B��</asp:ListItem>
                        <asp:ListItem Selected="false" Value="396">C��</asp:ListItem>
                        <asp:ListItem Selected="false" Value="397">D��</asp:ListItem>
                        <asp:ListItem Selected="false" Value="398">E��</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp; ����<asp:TextBox ID="txb_userno" runat="server" Width="50" TabIndex="3" Height="22"></asp:TextBox>
                    ��<asp:TextBox ID="txb_year" runat="server" Width="50" TabIndex="3" Height="22"></asp:TextBox>
                    ��<asp:TextBox ID="txb_month" runat="server" Width="30" TabIndex="3" Height="22"></asp:TextBox>
                    ��<asp:TextBox ID="txb_day" runat="server" Width="50" TabIndex="3" Height="22"></asp:TextBox>&nbsp;
                    <asp:CheckBox ID="chk_sum" runat="server" Text="��ʾ�ۼ�" AutoPostBack="true" Checked="false"
                        OnCheckedChanged="chk_sum_CheckedChanged" />&nbsp;
                    <asp:CheckBox ID="chk_multi" runat="server" Text="��ʾ�ֶ�" AutoPostBack="true" Checked="false"
                        OnCheckedChanged="chk_multi_CheckedChanged" />
                    <td align="right">
                        <asp:Button ID="btn_search" runat="server" Width="30" CssClass="SmallButton3" Text="��ѯ"
                            TabIndex="4" OnClick="btn_search_Click"></asp:Button>&nbsp;
                        <asp:Button ID="btn_list" runat="server" Width="30" CssClass="SmallButton3" Text="ˢ��"
                            TabIndex="4" OnClick="btn_list_Click"></asp:Button>&nbsp;
                        <asp:Button ID="btn_export" runat="server" Width="30" CssClass="SmallButton3" Text="����"
                            TabIndex="4" OnClick="btn_export_Click"></asp:Button>&nbsp;
                        <asp:Button ID="btn_detail" runat="server" Width="30" CssClass="SmallButton3" Text="ԭʼ"
                            TabIndex="4" OnClick="btn_detail_Click"></asp:Button>&nbsp;
                        <asp:Button ID="btn_repeat" runat="server" Width="30" CssClass="SmallButton3" Text="���"
                            TabIndex="4" OnClick="btn_repeat_Click"></asp:Button>&nbsp;
                    </td>
                    <td>
                    <asp:Button ID="btn_exportAll" runat="server" Width="80px" Visible="false" 
                        CssClass="SmallButton3" Text="��������" onclick="btn_exportAll_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;<asp:Label ID="lbl_qty" runat="server"></asp:Label>&nbsp;
                </td>
                <td>
                    &nbsp;<asp:Label ID="lbl_time" runat="server"></asp:Label>&nbsp;
                </td>
                <td>
                        <asp:Button ID="btn_listNew" runat="server" Width="60px" CssClass="SmallButton3" Text="ˢ�£��£�"
                            TabIndex="4" OnClick="btn_listNew_Click"></asp:Button></td>
            </tr>
        </table>
        <asp:GridView ID="gvBadProd" runat="server" AllowPaging="true" CssClass="GridViewStyle GridViewRebuild"
            AutoGenerateColumns="False" PageSize="20" OnPreRender="gvBadProd_PreRender" OnPageIndexChanging="gvBadProd_PageIndexChanging"
            Width="1120px">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="1120px" CellPadding="0" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="�ɱ�����" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="50px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="���ں�" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��������" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�ϰ�ʱ��" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�°�ʱ��" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="���ڹ�ʱ" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��ҹ��" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����Сʱ2" Width="100px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="group_id" Visible="False" />
                <asp:BoundField DataField="user_id" Visible="False" />
                <asp:BoundField DataField="group_cc" HeaderText="�ɱ�����">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_no" HeaderText="����">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_name" HeaderText="����">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_type" HeaderText="����">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_machine" HeaderText="���ں�">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_date" HeaderText="��������">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="group_start" HeaderText="�ϰ�ʱ��">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_end" HeaderText="�°�ʱ��">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_atten" HeaderText="���ڹ�ʱ">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_night" HeaderText="��ҹ��">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_atten2" HeaderText="����Сʱ2">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_starttime" HeaderText="ʵ���ϰ�ʱ��">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_endtime" HeaderText="ʵ���°�ʱ��">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script>
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
