<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.qad_bomviewdoc" CodeFile="qad_bomviewdoc.aspx.vb" %>

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
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0">
            <tr  style="display:none">
                <td>
                    Site
                    <asp:DropDownList ID="ddl_site" runat="server" Width="120px" AutoPostBack="True">
                        <asp:ListItem Selected="True" Value="szx">上海振欣 SZX</asp:ListItem>
                        <asp:ListItem Selected="false" Value="zql">镇江强凌 ZQL</asp:ListItem>
                        <asp:ListItem Selected="false" Value="zqz">镇江强灵照明 ZQZ</asp:ListItem>
                        <asp:ListItem Selected="false" Value="yql">扬州强凌 YQL</asp:ListItem>
                        <asp:ListItem Selected="false" Value="hql">淮安强凌 HQL</asp:ListItem>
                    </asp:DropDownList>
                    BOM Code
                    <asp:TextBox ID="txb_bom_code" runat="server" Width="130px" CssClass="smalltextbox Part"
                        MaxLength="20"></asp:TextBox>
                    Item Code
                    <asp:TextBox ID="txb_item_code" runat="server" Width="200px" 
                        CssClass="smalltextbox Item" MaxLength="50"></asp:TextBox>
                    BOM Date
                    <asp:TextBox ID="txb_bom_date" runat="server" Width="70px" CssClass="smalltextbox Date"
                        MaxLength="10"></asp:TextBox>
                    Level
                    <asp:TextBox ID="txb_lel" runat="server" Width="50px" CssClass="smalltextbox"></asp:TextBox>
                    <asp:CheckBox ID="CheckBox1" runat="server" Text="QAD Desc" Checked="True" AutoPostBack="True">
                    </asp:CheckBox><asp:CheckBox ID="Checkbox2" runat="server" Text="Associated Doc" Checked="True"
                        AutoPostBack="True"></asp:CheckBox>
                    
                </td>
                </tr>
                <tr>
                <td align="right">
                    <asp:Button ID="btn_search" runat="server" CssClass="SmallButton3" Width="60" Text="Search" Visible="False">
                    </asp:Button>
                    &nbsp;
                    <asp:Button ID="Button1" runat="server" CssClass="SmallButton3" Text="Back"></asp:Button>
                    &nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" CssClass="SmallButton3" Text="install DWG viewer"
                        Width="120px" Visible="False"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="1740px" AutoGenerateColumns="False"
            CssClass="GridViewStyle GridViewRebuild" PageSize="50" Height="470px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle Height="40px" CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="id" ReadOnly="True" Visible="False" HeaderText="">
                    <HeaderStyle Width="0px"></HeaderStyle>
                    <ItemStyle Width="0px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gsort" ReadOnly="True" HeaderText="No.">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="glevel" ReadOnly="True" HeaderText="Level">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gPart" ReadOnly="True" HeaderText="Item">
                    <HeaderStyle Width="90px"></HeaderStyle>
                    <ItemStyle Width="90px" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gqty" ReadOnly="True" HeaderText="Qty">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gCode" ReadOnly="True" HeaderText="Old Item">
                    <HeaderStyle Width="180px"></HeaderStyle>
                    <ItemStyle Width="180px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gLead" ReadOnly="True" HeaderText="Lead Time">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gDesc" ReadOnly="True" HeaderText="Description">
                    <HeaderStyle Width="500px"></HeaderStyle>
                    <ItemStyle Width="500px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                
<%--                <asp:ButtonColumn Text="" HeaderText="Open" CommandName="docview" DataTextField="open">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle Width="50px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>--%>
                <asp:BoundColumn DataField="open" ReadOnly="True" HeaderText="Open">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle Width="50px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="fid" ReadOnly="True" Visible="false" HeaderText="">
                    <HeaderStyle Width="20px"></HeaderStyle>
                    <ItemStyle Width="20px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="isNewMechanism" ReadOnly="True" Visible="false" HeaderText="">
                    <HeaderStyle Width="20px"></HeaderStyle>
                    <ItemStyle Width="20px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="filename" ReadOnly="True" HeaderText=" Associate FileName">
                    <HeaderStyle HorizontalAlign="Left" Width="220px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="220px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="typeid" ReadOnly="True" Visible="false" HeaderText="">
                    <HeaderStyle Width="20px"></HeaderStyle>
                    <ItemStyle Width="20px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="cateid" ReadOnly="True" Visible="false" HeaderText="">
                    <HeaderStyle Width="20px"></HeaderStyle>
                    <ItemStyle Width="20px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="typename" ReadOnly="True" HeaderText="Category">
                    <HeaderStyle Width="150px"></HeaderStyle>
                    <ItemStyle Width="150px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="catename" ReadOnly="True" HeaderText="Type">
                    <HeaderStyle Width="150px"></HeaderStyle>
                    <ItemStyle Width="150px" HorizontalAlign="Left"></ItemStyle>
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

