<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportSIDDocumentInfo.aspx.cs" Inherits="SID_ExportSIDDocumentInfo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
	    <title><%=Session["orgName"]%></title>
	</HEAD>
	<BODY bottomMargin="0" bgColor="#ffffff" leftMargin="0" topMargin="0" rightMargin="0">
		<div align="center">
			<form id="Form1" method="post" runat="server">
            </form>
	    </div>
	    <script>
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
	    </script>
	</BODY>
</HTML>