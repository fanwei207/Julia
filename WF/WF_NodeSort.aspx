<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_NodeSort.aspx.cs" Inherits="WF_NodeSort" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="HEAD1" runat="server">
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
        <table cellspacing="2" cellpadding="2" width="325" bgcolor="white" border="0" style="margin-top: 20px;">
            <tr>
                <td style="width: 274px">
                    <asp:Label ID="lbName" runat="server" Width="49px">Ãû³Æ:</asp:Label>
                    <asp:TextBox ID="txtSortName" runat="server" CssClass="SmallTextBox" Height="20px"
                        Width="160px"></asp:TextBox>
                    <asp:Label ID="lbID" runat="server" Text="Label" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton3" OnClick="btnAdd_Click"
                        TabIndex="0" Text="Ôö¼Ó" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvSort" runat="server" AutoGenerateColumns="False" DataKeyNames="Sort_ID"
            CssClass="GridViewStyle" Width="325px" OnRowDataBound="gvSort_RowDataBound" OnRowDeleting="gvSort_RowDeleting"
            OnRowCommand="gvSort_RowCommand">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="Sort_Order" HeaderText="ÐòºÅ">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="Sort_Name" HeaderText="Ãû³Æ">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkEdit" runat="server" CommandArgument='<%# Eval("Sort_ID") %>'
                            CommandName="myEdit" Font-Size="12px" Font-Underline="true" ForeColor="Blue"
                            Text="<u>±à¼­</u>"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkdelete" runat="server" CommandArgument='<%# Eval("Sort_ID") %>'
                            CommandName="Delete" Font-Size="12px" Font-Underline="true" ForeColor="Blue"
                            Text="<u>É¾³ý</u>"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
