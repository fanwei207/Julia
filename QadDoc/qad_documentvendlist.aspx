<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qad_documentvendlist.aspx.cs" Inherits="QadDoc_qad_documentvendlist" %>

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
            <table cellspacing="0" cellpadding="0" width="610" bgcolor="white" border="0">
                <tr>
                    <td align="left" colspan="2">
                        Vend:<asp:TextBox ID="txtVend" 
                            CssClass="smalltextbox Supplier" runat="server" Width="301px" 
                            MaxLength="8"></asp:TextBox>
                        <asp:Button ID="BtnAdd" runat="server" CssClass="SmallButton3" Text="Add" Width="50"
                            OnClick="BtnAdd_Click"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        Associated Vend
                    </td>
                    <td align="right">
                        <input class="SmallButton3" id="Button1" onclick="window.close();" type="button"
                            value="Close" name="Button1" runat="server">
                        </td>
                </tr>
            </table>
            <table cellspacing="0" cellpadding="0" width="610" align="center" bgcolor="white"
                border="0">
                <tr width="100%">
                    <td>
                        <asp:GridView ID="gvVend" runat="server" Width="610px" AllowPaging="True" PageSize="20"
                            AutoGenerateColumns="False" CssClass="GridViewStyle" DataKeyNames="id" OnRowDeleting="gvVend_RowDeleting"
                            OnPageIndexChanging="gvVend_PageIndexChanging" OnRowDataBound="gvVend_RowDataBound">
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                            <Columns>
                                <asp:TemplateField HeaderText="No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblViewNo" runat="server" Text='<%# (Container.DataItemIndex + 1) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="vend" HeaderText="Vend">
                                    <HeaderStyle Width="100px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="vendName" HeaderText="VendName">
                                    <HeaderStyle Width="430px"></HeaderStyle>   
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Delete">
                                    <HeaderStyle Width="40px" HorizontalAlign="Center"/>
                                    <ItemStyle Width="40px" HorizontalAlign="Center" ForeColor="Black" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" Text="Del" ForeColor="Black" CommandName="Delete"
                                         CommandArgument='<%# Eval("id") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ControlStyle Font-Bold="False" Font-Size="8pt" Font-Underline="True" ForeColor="Black" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </form>
    </div>

    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>

</body>
</html>
