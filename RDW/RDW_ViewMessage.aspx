<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_ViewMessage.aspx.cs" Inherits="RDW_ViewMessage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
	<BODY>
		<div align="center">
			<form id="Form1" method="post" runat="server">
			    <asp:GridView ID="gvMessage" runat="server" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False" Width="600px" 
                    CssClass="GridViewStyle">
                    <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="false"/>
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                    <EmptyDataTemplate>
                        <asp:Table ID="Table1" Width="600px" CellPadding="-1" CellSpacing="0" runat="server"
                            CssClass="GridViewHeaderStyle" GridLines="Vertical">
                            <asp:TableRow>
                                <asp:TableCell Text="Message List" Width="600px" HorizontalAlign="center"></asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="RDW_Message" HeaderText="Message List" HtmlEncode="false">
                            <HeaderStyle Width="600px" HorizontalAlign="Center" />
                            <ItemStyle Width="600px" HorizontalAlign="Left"/>
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </form>
	    </div>
	    <script>
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
	    </script>
	</BODY>
</HTML>