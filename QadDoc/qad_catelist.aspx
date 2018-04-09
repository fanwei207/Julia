<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.qad_catelist" CodeFile="qad_catelist.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
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
        <table id="Table1" runat="server" cellpadding="0" cellspacing="0" width="1080px">
            <tr>
                <td>
                    Schema
                    <asp:DropDownList ID="SelectSchemaDropDown" runat="server" Width="130px" AutoPostBack="True">
                    </asp:DropDownList>
                    Category
                    <asp:DropDownList ID="SelectTypeDropDown" runat="server" Width="200px" AutoPostBack="True">
                    </asp:DropDownList>
                    &nbsp;名称<asp:TextBox ID="txbadd" runat="server" Width="200px" CssClass="smalltextbox"></asp:TextBox>
                    条目<asp:DropDownList ID="dropTackingType" runat="server" Width="60px" 
                        AutoPostBack="True" DataTextField="ptt_type" DataValueField="ptt_type">
                    </asp:DropDownList>
                    <asp:DropDownList ID="dropTackingDetail" runat="server" Width="100px" 
                        DataTextField="ptt_detail" DataValueField="ptt_id">
                    </asp:DropDownList>
                    <input id="hidID" type="hidden" runat="server" />
                </td>
                <td horizontalalign="Right">
                    <asp:Button ID="AddBtn" runat="server" CssClass="SmallButton3" Width="60px" Text="Add">
                    </asp:Button>
                    &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="SmallButton3" Text="Cancel" Width="60"
                        Visible="false"></asp:Button>
                    &nbsp;<asp:Button ID="btnback" runat="server" CssClass="SmallButton3" Text="Back" Width="60"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="1080px" AutoGenerateColumns="False" DataKeyField="id"
            PageSize="23" AllowPaging="True"
            CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="id" ReadOnly="True" HeaderText="id">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gsort" ReadOnly="True" HeaderText="序号">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="名称" DataField="cat" ReadOnly="True">
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="450px" />
                    <ItemStyle Font-Bold="False" HorizontalAlign="Center" Width="450px" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="关联供应商">
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="70px" />
                    <ItemStyle Font-Bold="False" HorizontalAlign="Center" Width="70px" />
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSinger" runat="server" Checked='<%# Bind("linkVend") %>'  AutoPostBack="True" OnCheckedChanged="chkSinger_OnCheckedChanged" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="条目" DataField="ptt_type" ReadOnly="True">
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Width="50px" />
                    <ItemStyle Font-Bold="False" HorizontalAlign="Center" Width="50px" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="条目细类" DataField="ptt_detail" ReadOnly="True">
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Width="150px" />
                    <ItemStyle Font-Bold="False" HorizontalAlign="Center" Width="150px" />
                </asp:BoundColumn>
                <asp:TemplateColumn>
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemTemplate>
                        <asp:LinkButton runat="server" CausesValidation="false" CommandName="myEdit" 
                            Text="&lt;u&gt;Edit&lt;/u&gt;"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                </asp:TemplateColumn>
                <asp:ButtonColumn Text="<u>Del</u>" CommandName="DeleteBtn">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>Select</u>" HeaderText="子类" CommandName="Select">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="60px"></HeaderStyle>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" />
                </asp:ButtonColumn>
                <asp:BoundColumn Visible="False" DataField="pub" ReadOnly="True" HeaderText="pub">
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
