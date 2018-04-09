<%@ Reference Page="~/admin/department.aspx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.subPieceSearch" CodeFile="subPieceSearch.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% =Session("orgName") %>
		</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK media="all" href="../script/main.css" rel="stylesheet">
		<script language="javascript">
		function post()
		{  
		  var sday=document.getElementById("modid").value;
		  var eday=document.getElementById("moded").value;
		  
		  if(document.getElementById("code1").value=="13*")
 		  {
            window.top.location.href="/salary/Searchworkers.aspx";
            return false;
		  }
		  
		  var yy=document.getElementById("startDate").value;
		  if (isNaN(yy))
		  {
		    alert("请输入正确的年份数字XXXX"); 
		    return false;
		  }
		  if (yy<1000 || yy>9999)
		  {
		    alert("请输入正确的年份XXXX"); 
		    return false;
		  } 
		  
		   if ((sday=="" && eday!="") || (sday!="" && eday==""))
		  {
		    alert("输入的日期范围不完整"); 
		    return false;
		  }
		  else
		  {
		   if (sday!="" && eday!="")
		   {
		     if (isNaN(sday))
		     {
		       alert("请输入正确的日期数字XX"); 
		       return false;
			 }
			if (isNaN(eday))
			{
				alert("请输入正确的日期数字XX"); 
				return false;
			}
			if (sday<1 || sday>31)
			{
				alert("请输入正确的日期"); 
				return false;
			}
			if (eday<1 || eday>31)
			{
				alert("请输入正确的日期"); 
				return false;
			}
		   }
		  }
		  
		  parent.frames("list").location.href="/salary/subPieceSearchList.aspx?yr="+document.getElementById("startDate").value+"&ye="+document.getElementById("DropDownList1").value+"&gh1="+document.getElementById("code1").value+"&na="+escape(document.getElementById("name").value)+"&dp="+document.getElementById("Department").value+"&sd="+document.getElementById("modid").value+"&ed="+document.getElementById("moded").value;
		}
		</script>
	</HEAD>
	<body bottomMargin="0" bgColor="ghostwhite" leftMargin="0" topMargin="0" rightMargin="0">
		<div align="center">
			<form id="Form1" method="post" runat="server">
				<asp:validationsummary id="Validationsummary1" HeaderText="请检查以下字段:" ShowMessageBox="true" ShowSummary="false"
					Runat="server"></asp:validationsummary>
				<TABLE cellSpacing="4" cellPadding="4" border="0" width="110">
					<tr>
						<td align="left" width="100"><b>设置查询条件:</b></td>
					</tr>
					<tr>
						<td style="WIDTH: 100px; HEIGHT: 14px" align="left">工号 80,83-100</td>
					</tr>
					<tr>
						<td style="HEIGHT: 14px" vAlign="top"><asp:textbox id="code1" runat="server" Width="90px"></asp:textbox></td>
					</tr>
					<tr>
						<td style="WIDTH: 100px; HEIGHT: 14px" align="left">姓名</td>
					</tr>
					<tr>
						<td style="HEIGHT: 14px" vAlign="top"><asp:textbox id="name" runat="server" Width="90px"></asp:textbox></td>
					</tr>
					<TR>
						<td style="WIDTH:100px; HEIGHT:14px" align="left">部门</td>
					</TR>
					<tr>
						<td style="HEIGHT: 14px"><asp:dropdownlist id="Department" runat="server" Width="90px"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td style="WIDTH: 100px; HEIGHT: 14px" align="left">年月</td>
					</tr>
					<tr>
						<td vAlign="top" width="100"><asp:textbox id="startDate" runat="server" Width="40px"></asp:textbox>
							<asp:DropDownList id="DropDownList1" runat="server" Width="40px" Font-Size="10pt">
								<asp:ListItem>1</asp:ListItem>
								<asp:ListItem>2</asp:ListItem>
								<asp:ListItem>3</asp:ListItem>
								<asp:ListItem>4</asp:ListItem>
								<asp:ListItem>5</asp:ListItem>
								<asp:ListItem>6</asp:ListItem>
								<asp:ListItem>7</asp:ListItem>
								<asp:ListItem>8</asp:ListItem>
								<asp:ListItem>9</asp:ListItem>
								<asp:ListItem>10</asp:ListItem>
								<asp:ListItem>11</asp:ListItem>
								<asp:ListItem>12</asp:ListItem>
							</asp:DropDownList>
						</td>
					</tr>
					<tr>
						<td style="WIDTH: 100px; HEIGHT: 14px" align="left">日期范围</td>
					</tr>
					<tr>
						<td><asp:TextBox ID="modid" Runat="server" Width="30px"></asp:TextBox>--
							<asp:TextBox Runat="server" ID="moded" Width="30px"></asp:TextBox></td>
					</tr>
					<tr>
						<td>
							<input type="button" class="SmallButton2" id="Button1" value="查询" onclick="post();"></td>
					</tr>
				</TABLE>
			</form>
		</div>
		<script>
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
		</script>
	</body>
</HTML>
