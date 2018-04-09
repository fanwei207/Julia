<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDIUpdateFTC.aspx.cs" Inherits="EDI_EDIUpdateFTC" %>

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
        <table cellspacing="0" cellpadding="0" width="500" bgcolor="white" border="0" style="margin-top: 4px;
            margin-bottom: 4px;">
            <tr style="background-image: url(../images/bg_tb2.jpg); background-repeat: repeat-x;
                height: 35px; font-family: Î¢ÈíÑÅºÚ;">
                <td style="width: 3px; background-image: url(../images/bg_tb1.jpg); background-repeat: no-repeat;
                    background-position: left top;">
                </td>
                <td align="left">
                    <asp:TextBox ID="txbJDE" runat="server" Width="196px"></asp:TextBox>
                    <asp:TextBox ID="txbQAD" runat="server" Width="152px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" Width="50px" TabIndex="0"
                        Text="Search" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton3" TabIndex="0" Text="Add"
                        OnClick="btnAdd_Click" />
                </td>
                <td style="width: 3px; background-image: url(../images/bg_tb3.jpg); background-repeat: no-repeat;
                    background-position: right top;">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvFTC" runat="server" AllowPaging="true" PageSize="25" DataKeyNames="ID"
            AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
            CellPadding="1" GridLines="Vertical" Height="10px" Width="500px" OnRowCancelingEdit="gvFTC_RowCancelingEdit"
            OnRowDeleting="gvFTC_RowDeleting" OnRowEditing="gvFTC_RowEditing" OnRowUpdating="gvFTC_RowUpdating"
            OnPageIndexChanging="gvFTC_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="JDEItem">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtJDE" runat="server" Text='<%# Bind("JDEItem") %>' Width="180px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtJDE"
                            Display="Dynamic" ErrorMessage="Input!"></asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="left" />
                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                    <ItemTemplate>
                        <%#Eval("JDEItem")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="QADItem">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtQAD" runat="server" Text='<%# Bind("QADItem") %>' Width="140px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemTemplate>
                        <%#Eval("QADItem")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" CancelText="Cancel" DeleteText="Delete" EditText="Edit"
                    UpdateText="Update">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
                <asp:CommandField ShowDeleteButton="True" DeleteText="&lt;SPAN onclick=&quot;return confirm('Delete?');&quot;&gt;Delete&lt;/SPAN&gt;">
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
