<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Plan_SparesHistQuery.aspx.cs"
    Inherits="Plan_SparesHistQuery" %>

<%@ Import Namespace="Portal.Fixas" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
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
        <table cellspacing="2" cellpadding="2" bgcolor="white" border="0" style="width: 1044px">
            <tr>
                <td>
                    ɸѡ��
                </td>
                <td>
                    <asp:DropDownList ID="dropType" runat="server" Width="80%">
                        <asp:ListItem>��ѯȫ��</asp:ListItem>
                        <asp:ListItem>����ѯ���</asp:ListItem>
                        <asp:ListItem>����ѯ����</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    ��Ʒ��ţ�
                </td>
                <td>
                    <asp:TextBox ID="txtNo1" runat="server" CssClass="SmallTextBox4" TabIndex="1" Width="100px"></asp:TextBox>-<asp:TextBox
                        ID="txtNo2" runat="server" CssClass="SmallTextBox4" TabIndex="1" Width="100px"></asp:TextBox>
                </td>
                <td>
                    �����豸��
                </td>
                <td>
                    <asp:TextBox ID="txtDevice" runat="server" CssClass="SmallTextBox4" TabIndex="1"
                        Width="100px"></asp:TextBox>
                </td>
                <td>
                    ����\������ڣ�
                </td>
                <td>
                    <asp:TextBox ID="txtHoderDate1" runat="server" CssClass="SmallTextBox date" TabIndex="1"
                        Width="100px"></asp:TextBox>-<asp:TextBox ID="txtHoderDate2" runat="server" CssClass="SmallTextBox Date"
                            TabIndex="1" Width="100px"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    ����\����ˣ�
                </td>
                <td>
                    <asp:DropDownList ID="dropDept" runat="server" AutoPostBack="True" DataTextField="Name"
                        DataValueField="departmentID" OnSelectedIndexChanged="dropDept_SelectedIndexChanged"
                        Width="100px">
                    </asp:DropDownList>
                    &nbsp;<asp:DropDownList ID="dropUser" runat="server" DataTextField="userInfo" DataValueField="userID"
                        Width="100px">
                    </asp:DropDownList>
                </td>
                <td>
                    ����\��⳵�䣺
                </td>
                <td>
                    <asp:DropDownList ID="dropFloor" runat="server" DataTextField="Name" DataValueField="departmentID"
                        Width="100px">
                    </asp:DropDownList>
                </td>
                <td>
                    �������ڣ�
                </td>
                <td>
                    <asp:TextBox ID="txtCreatedDate1" runat="server" CssClass="SmallTextBox Date" TabIndex="1"
                        Width="100px"></asp:TextBox>-<asp:TextBox ID="txtCreatedDate2" runat="server" CssClass="SmallTextBox Date"
                            TabIndex="1" Width="100px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="5" Text="Query"
                        OnClick="btnQuery_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="21" OnPreRender="gvRDW_PreRender" DataKeyNames="ID"
            OnRowDataBound="gvRDW_RowDataBound" OnPageIndexChanging="gvRDW_PageIndexChanging"
            Width="1050px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="1050px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="��Ʒ���" Width="110px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="��Ʒ����" Width="280px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="�����豸" Width="100px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="����\�������" Width="90px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="����\�������" Width="90px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="����\��⳵��" Width="90px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="����\�����" Width="90px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="��������" Width="110px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="������" Width="50px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="����" Width="40px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="NO" HeaderText="��Ʒ���" ReadOnly="True">
                    <HeaderStyle Width="110px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Description" HeaderText="��Ʒ����" ReadOnly="True">
                    <HeaderStyle Width="300px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="300px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Device" HeaderText="�����豸" ReadOnly="True">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="����\�������">
                    <ItemTemplate>
                        <asp:Label ID="lbHolderDate" runat="server" Text='<%# ((User)((SpareHist)Container.DataItem).Holder).Date %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:TemplateField>
                <asp:BoundField DataField="Qty" HeaderText="����\�������" ReadOnly="True">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Floor" HeaderText="����\��⳵��" ReadOnly="True">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="����\�����">
                    <ItemTemplate>
                        <asp:Label ID="lbHolder" runat="server" Text='<%# ((User)((SpareHist)Container.DataItem).Holder).Name %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="��������">
                    <ItemTemplate>
                        <asp:Label ID="lbCreatedDate" runat="server" Text='<%# ((User)((SpareHist)Container.DataItem).Creator).Date %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="110px" />
                    <ItemStyle HorizontalAlign="Center" Width="110px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="������">
                    <ItemTemplate>
                        <asp:Label ID="lbCreator" runat="server" Text='<%# ((User)((SpareHist)Container.DataItem).Creator).Name %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </asp:TemplateField>
                <asp:BoundField DataField="Type" HeaderText="����" ReadOnly="True">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
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
