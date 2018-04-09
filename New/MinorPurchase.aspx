<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MinorPurchase.aspx.cs" Inherits="new_MinorPurchase" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .hidden
        {
            display: none;
        }
    </style>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="1000px" class="main_top">
            <tr>
                <td>
                    &nbsp;&nbsp;������<asp:TextBox ID="txt_uname" TabIndex="0" runat="server" Width="100px"
                        MaxLength="10"></asp:TextBox>&nbsp;&nbsp; ����<asp:DropDownList ID="dropDept" runat="server"
                            Width="100px">
                        </asp:DropDownList>
                    &nbsp;&nbsp; ����<asp:DropDownList ID="dropType" runat="server" Width="100px">
                    </asp:DropDownList>
                    &nbsp;&nbsp; ��Ӧ��<asp:TextBox ID="txtSupplier" runat="server" Width="120px"></asp:TextBox>&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSearch" TabIndex="0" runat="server"
                        Text="��ѯ" CssClass="SmallButton3" OnClick="btnSearch_Click"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="chkOwner" runat="server" Checked="True" Text="ֻ��ʾ�豾������" AutoPostBack="True">
                    </asp:CheckBox>
                </td>
                <td>
                    <asp:Button ID="btnNew" TabIndex="0" runat="server" Text="�½����ǲɹ����뵥" CssClass="SmallButton3"
                        Width="120" OnClick="btnNew_Click"></asp:Button>&nbsp;
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvPA" AllowPaging="True" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            runat="server" PageSize="20" OnRowCommand="gvPA_RowCommand" DataSourceID="obdsMp"
            DataKeyNames="Aid" Width="1000px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundField HeaderText="������" DataField="AName" ReadOnly="true">
                    <ItemStyle Width="80px" HorizontalAlign="center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="dname" ReadOnly="true">
                    <ItemStyle Width="150px" HorizontalAlign="center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="������Ʒ" DataField="Part">
                    <ItemStyle Width="250px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="Ptype">
                    <ItemStyle Width="80px" HorizontalAlign="center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="Quantity" DataFormatString="{0:N2}">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��������" DataField="creatdate" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False" ReadOnly="True">
                    <ItemStyle Width="80px" HorizontalAlign="center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="Price" DataFormatString="{0:N2}">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��Ӧ��" DataField="Supplier">
                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����״̬" DataField="Pname">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��һ������" DataField="bName">
                    <ItemStyle Width="100px" HorizontalAlign="center" />
                </asp:BoundField>
                <asp:ButtonField Text="<u>��ϸ</u>" ItemStyle-ForeColor="black" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center"
                    CommandName="1">
                    <ItemStyle HorizontalAlign="Center" ForeColor="Black" Width="40px"></ItemStyle>
                </asp:ButtonField>
                <asp:ButtonField Text="<u>ɾ��</u>" ItemStyle-ForeColor="black" ItemStyle-Width="40px"
                    ItemStyle-HorizontalAlign="Center" CommandName="2">
                    <ItemStyle HorizontalAlign="Center" ForeColor="Black" Width="40px"></ItemStyle>
                </asp:ButtonField>
                <asp:BoundField DataField="Pstatus" ReadOnly="True" Visible="False">
                    <ItemStyle />
                    <HeaderStyle />
                    <FooterStyle />
                </asp:BoundField>
                <asp:BoundField DataField="userID" ReadOnly="True" Visible="False">
                    <ItemStyle />
                    <HeaderStyle />
                    <FooterStyle />
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="table" runat="server" CellPadding="-1" BorderWidth="1" CellSpacing="0"
                    CssClass="GridViewHeaderStyle" GridLines="Both">
                    <asp:TableRow>
                        <asp:TableCell Text="������" Width="80px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="200px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="������Ʒ" Width="350px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="����" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="����" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="��������" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="����" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="��Ӧ��" Width="200px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="����״̬" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="��һ������" Width="100px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Width="60px"></asp:TableCell>
                        <asp:TableCell Width="60px"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:ObjectDataSource ID="obdsMp" runat="server" SelectMethod="MPList" TypeName="MinorP.MinorPurchase">
            <SelectParameters>
                <asp:ControlParameter ControlID="txt_uname" Name="strUser" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="dropDept" DefaultValue="0" Name="intDept" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="dropType" DefaultValue="0" Name="intType" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="txtSupplier" Name="strSp" PropertyName="Text" Type="String" />
                <asp:SessionParameter Name="intPlant" SessionField="plantcode" Type="Int32" />
                <asp:ControlParameter ControlID="chkOwner" DefaultValue="" Name="blchk" PropertyName="Checked"
                    Type="Boolean" />
                <asp:SessionParameter Name="intsid" SessionField="uid" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
