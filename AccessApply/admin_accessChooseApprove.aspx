<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin_accessChooseApprove.aspx.cs"
    Inherits="AccessApply_admin_acessChooseApprove" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择下一审批人</title>
       <link media="all" href="../css/main.css" rel="stylesheet" />
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
	<meta content="Visual C# .NET 7.1" name="CODE_LANGUAGE" />
	<meta content="JavaScript" name="vs_defaultClientScript" />
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" /> 
	<script language ="javascript" type ="text/javascript">
 	function post()
		{
		  window.opener.document.getElementById("txt_approveName").value=document.getElementById("txtApproveName").value;
	      window.opener.document.getElementById("txt_approveEmail").value=document.getElementById("txtApproveEmail").value;
	      window.opener.document.getElementById("txt_approveID").value=document.getElementById("txtApproveID").value;
	      window.close();
       	}		
	</script>
    
    
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <table style="margin-top: 5px; width:400px">
                <tr>
                    <td  style=" margin-right: 10px; width: 280px; height: 18px;"></td>
                    <td style="width: 5px; margin-right: 10px; height: 18px;"> 
                    </td>
                </tr>
                <tr>
                    <td  style="vertical-align: top; text-align: left; width: 280px;" rowspan="2">
                        公司:<asp:DropDownList ID="ddl_Company" runat="server" AutoPostBack="True" DataTextField="description"
                            DataValueField="plantID" OnSelectedIndexChanged="ddl_Company_SelectedIndexChanged"
                            Width="193px">
                        </asp:DropDownList>
                        <br />
                        部门:<asp:DropDownList ID="ddl_department" runat="server" AutoPostBack="True" DataTextField="name"
                            DataValueField="departmentID" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged"
                            Width="192px">
                        </asp:DropDownList>
                        <br />
                        人员:<asp:TextBox ID="txt_user" runat="server" CssClass="SmallTextBox" Width="80px"></asp:TextBox><asp:Button
                            ID="btn_search" runat="server" CssClass="SmallButton3" OnClick="btn_search_Click"
                            Text="查询" Width="40px" />
                        <%--<p style="color: #ff0000; font-size:12px;text-align: left; vertical-align: text-top; margin-top: 0px; padding-bottom: 0px;">注:部门和人员至少存在一个条件,"人员"查询是模糊匹配!</p>--%>
                        <asp:Panel ID="Panel3" runat="server" BorderWidth="1" Height="250px"
                            HorizontalAlign="Left" ScrollBars="Vertical" valign="top" Width="250px" Direction="LeftToRight">
                            <asp:RadioButtonList ID="radioBtnList_SelecetApprove" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="radioBtnList_SelecetApprove_SelectedIndexChanged">
                            </asp:RadioButtonList></asp:Panel> 审批人:<asp:TextBox ID="txtApproveName" runat="server" ReadOnly="true" Width="97px"></asp:TextBox>
                        &nbsp; &nbsp;
                        <asp:TextBox ID="txtApproveID" runat="server"  Width="0px" BorderWidth = "0" ></asp:TextBox> <br /> 邮 箱:<asp:TextBox ID="txtApproveEmail" runat="server" Width="186px"></asp:TextBox>&nbsp;
                       
                    </td>
                    <td style="vertical-align: top; width: 5px; height: 83px; text-align: left">
                        &nbsp;<asp:Button ID="btn_close2" runat="server" Text="返回" OnClick="btn_close_Click" CssClass="SmallButton3" Width="32px" /></td>
                </tr>
                <tr>
                    <td style="width: 5px; height: 21px">
                        </td>
                </tr>
                <tr>
                    <td align="center" rowspan="1" style="vertical-align: top; width: 280px;">
                        <asp:Button ID="btn_sure" runat="server"  Text="确定选择"  CssClass="SmallButton2" Width="54px" OnClientClick="return post();" ></asp:Button>
                        <asp:Button id="btn_close1" runat="server" Text="返回" OnClick="btn_close_Click" CssClass="SmallButton2" Width="46px" /></td>
                    <td style="width: 5px; height: 21px">
                    </td>
                </tr>
            </table>
        </div>
        <script type ="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
		</script>
    </form>
</body>
</html>
