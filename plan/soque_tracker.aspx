<%@ Page Language="C#" AutoEventWireup="true" CodeFile="soque_tracker.aspx.cs" Inherits="plan_soque_tracker" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <div style="width: 1000px; margin: 0 auto;">
            <p style="text-align: left; margin: 0; padding: 5px; background-image: url(../images/banner01.jpg);">
                <asp:Label ID="Label2" runat="server" Text="��˾"></asp:Label>
                <asp:DropDownList ID="dropPlants" runat="server" Width="250px">
                </asp:DropDownList>
                &nbsp;&nbsp;
                <asp:Label ID="Label3" runat="server" Text="����"></asp:Label>
                <asp:TextBox ID="txbUserNo" runat="server" Width="240px" CssClass="SmallTextBox"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="��ѯ" CssClass="SmallButton2" Width="70px"
                    OnClick="btnSearch_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text="����" CssClass="SmallButton2" Width="70px"
                    OnClick="btnUpdate_Click" />
            </p>
            <asp:GridView ID="gvTrackers" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
                Width="1000px" AllowPaging="True" AllowSorting="True" PageSize="25" OnPageIndexChanging="gvTrackers_PageIndexChanging"
                OnRowCommand="gvTrackers_RowCommand">
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table1" Width="1000px" CellPadding="0" CellSpacing="0" runat="server"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Text="����" Width="100px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="����" Width="100px" HorizontalAlign="Center"></asp:TableCell>
                            <asp:TableCell Text="��˾" Width="160px" HorizontalAlign="Center"></asp:TableCell>
                            <asp:TableCell Text="Email" Width="160px" HorizontalAlign="Center"></asp:TableCell>
                            <asp:TableCell Text="������" Width="100px" HorizontalAlign="Center"></asp:TableCell>
                            <asp:TableCell Text="����ʱ��" Width="100px" HorizontalAlign="Center"></asp:TableCell>
                            <asp:TableCell Text="�޸���" Width="100px" HorizontalAlign="Center"></asp:TableCell>
                            <asp:TableCell Text="�޸�ʱ��" Width="100px" HorizontalAlign="Center"></asp:TableCell>
                            <asp:TableCell Text="����" Width="80px" HorizontalAlign="Center"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="id" Visible="False" HeaderText="ID"></asp:BoundField>
                    <asp:BoundField DataField="userID" Visible="False" HeaderText="userID"></asp:BoundField>
                    <asp:BoundField DataField="userNo" HeaderText="����">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="userName" HeaderText="����">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="plantName" HeaderText="��˾">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="160px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="160px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="email" HeaderText="Email">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="160px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="160px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="createdName" HeaderText="������">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="createdDate" HeaderText="����ʱ��">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="modifiedName" HeaderText="�޸���">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="modifiedDate" HeaderText="�޸�ʱ��">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="ltnEdit" Text="<u>�༭</u>" ForeColor="Blue" Font-Underline="true"
                                Font-Size="12px" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="myEdit" />
                            <asp:LinkButton ID="ltnDelete" Text="<u>ɾ��</u>" ForeColor="Blue" Font-Underline="true"
                                Font-Size="12px" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="myDelete" />
                        </ItemTemplate>
                        <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="false" />
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <asp:Label ID="labTrackerID" runat="server" Text="0" Visible="false"></asp:Label>
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal runat="server" id="ltlAlert" EnableViewState="false"></asp:Literal>
    </script>
</body>
</html>
