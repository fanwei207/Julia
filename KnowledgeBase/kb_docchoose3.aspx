<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.KB_docChoose3" CodeFile="KB_docChoose3.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		 <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
		<script language="javascript">
		     	function post()
			{
			  window.opener.document.getElementById("txb_chooseid").value=document.getElementById("txb_chooseid").value;
   		      window.opener.document.getElementById("txb_choose").value=document.getElementById("txb_choose").value;
   		      window.close();

           	}	
			
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table id="table1" cellpadding="0" cellspacing="0" width="400" align="center">
				<tr>
					<td>
						&nbsp;
					</td>
				</tr>
				<tr>
					<td>
						<asp:TextBox ID="txb_docid" Runat="server" style="DISPLAY:none"></asp:TextBox>
						<asp:TextBox ID="txb_typeid" Runat="server" style="DISPLAY:none"></asp:TextBox>
						<asp:TextBox ID="txb_chooseid" Runat="server" style="DISPLAY:none"></asp:TextBox>
						<asp:TextBox ID="txb_choose" Runat="server" Width="300px"></asp:TextBox>
						<input ID="btn_ok" value="OK" class="SmallButton2" type="button" OnClick="post()">
					</td>
				</tr>
				<tr>
					<td vAlign="top" align="left" width="450" colSpan="2" style="HEIGHT: 20px">
						公司<asp:dropdownlist id="ddl_Company" runat="server" Width="200px" AutoPostBack="True"></asp:dropdownlist>
					</td>
				</tr>
				<tr>
					<td vAlign="top" align="left" width="450" colSpan="2">部门<asp:dropdownlist id="ddl_department" runat="server" Width="120px" AutoPostBack="True"></asp:dropdownlist>
					人员<asp:TextBox ID="txb_user" CssClass="SmallTextBox" Width="80px" Runat="server"></asp:TextBox>
						<asp:Button ID="btn_search" CssClass="SmallButton3" Runat="server" Text="查询"></asp:Button>
					</td>
				</tr>
				 <tr>
					<td>
						<p style="COLOR: #ff0000">注:部门和人员至少存在一个条件,"人员"查询是模糊匹配!</p>
					</td>
				</tr>
				<tr>
					<td colSpan="2">
						<asp:panel id="Panel1" style="OVERFLOW:auto" runat="server" Width="300px" Height="300px" BorderWidth="1">
							<asp:CheckBoxList id="cbl_user" runat="server" AutoPostBack="True"></asp:CheckBoxList>
						</asp:panel>
					</td>
				</tr>
			</table>
		</form>
		<script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
		</script>
	</body>
</HTML>
