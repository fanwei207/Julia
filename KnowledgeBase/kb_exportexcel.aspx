<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.KB_exportExcel" codePage="936" CodeFile="KB_exportExcel.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% =Session("orgName")%>
		</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<asp:table id="exl" BorderWidth="1" GridLines="Both" width="800" cellspacing="0" cellpadding="0"
			runat="server" />
	</body>
</HTML>
