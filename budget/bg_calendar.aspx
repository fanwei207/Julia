<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bg_calendar.aspx.cs" Inherits="BudgetProcess.budget_bg_calendar" %>

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
        <table style="width: 510px">
            <tr>
                <td style="width: 600px; height: 23px;">
                    域:<asp:DropDownList ID="dropDomain" runat="server" DataTextField="cc_domain" DataValueField="cc_domain">
                    </asp:DropDownList>
                    &nbsp; &nbsp;&nbsp; 期间:<asp:TextBox ID="txtDate" runat="server" CssClass="SmallTextBox"
                        Width="108px"></asp:TextBox>
                    &nbsp; &nbsp;&nbsp; 是否关闭:<asp:DropDownList ID="dropClose" runat="server">
                        <asp:ListItem>--</asp:ListItem>
                        <asp:ListItem Value="1">YES</asp:ListItem>
                        <asp:ListItem Value="1">NO</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton3" OnClick="btnAdd_Click"
                        Text="增加" Width="40" />
                </td>
                <td style="height: 23px">
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" OnClick="btnSearch_Click"
                        Text="查询" Width="40" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvCalendar" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            Width="491px" DataKeyNames="ID" OnRowCancelingEdit="gvCalendar_RowCancelingEdit"
            OnRowDataBound="gvCalendar_RowDataBound" OnRowDeleting="gvCalendar_RowDeleting"
            OnRowEditing="gvCalendar_RowEditing" 
            OnRowUpdating="gvCalendar_RowUpdating" AllowPaging="True" 
            onpageindexchanging="gvCalendar_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:TemplateField>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="域">
                    <EditItemTemplate>
                        <asp:Label ID="lblDomain" Text='<%# Bind("ca_domain") %>' runat="server" Visible="false"></asp:Label>
                        <asp:DropDownList ID="dropDomain" runat="server">
                            <asp:ListItem>--</asp:ListItem>
                            <asp:ListItem>SZX</asp:ListItem>
                            <asp:ListItem>ZQL</asp:ListItem>
                            <asp:ListItem>ZQZ</asp:ListItem>
                            <asp:ListItem>YQL</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemTemplate>
                        <%#Eval("ca_domain")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="期间">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDate" runat="server" Text='<%# Bind("ca_date") %>' Width="80px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemTemplate>
                        <%#Eval("ca_date")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="预测已关闭">
                    <EditItemTemplate>
                        <asp:Label ID="lblClose" Text='<%# Bind("ca_close") %>' runat="server" Visible="false"></asp:Label>
                        <asp:DropDownList ID="dropClose" runat="server">
                            <asp:ListItem>--</asp:ListItem>
                            <asp:ListItem Value="YES">YES</asp:ListItem>
                            <asp:ListItem Value="NO">NO</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemTemplate>
                        <%#Eval("ca_close")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                    EditText="<u>编辑</u>" UpdateText="<u>更新</u>" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
                <asp:CommandField ShowDeleteButton="True" DeleteText="<u>删除</u>">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
