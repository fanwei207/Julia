<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.inv_count_Export" CodeFile="inv_count_Export.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
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
				<table cellSpacing="2" cellPadding="2" width="900" bgColor="white" border="0">
					<tr>
						<td align="right" width="90">仓库:</td>
						<td>
							<asp:DropDownList id="DropDownList1" runat="server" Width="294px"></asp:DropDownList></td>
						<td colspan="4" align="center">
							<asp:Button ID="Export" Runat="server" Text="导出" Width="120px" CssClass="SmallButton2"></asp:Button></td>
						<td colspan="4" align="center">
							<asp:Button ID="Button1" Runat="server" Text="导出带数量" Width="120px" CssClass="SmallButton2"></asp:Button></td>
					</tr>
					
				</table>
                </form>
		</div>
		<script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
		</script>
	</body>
</HTML>
