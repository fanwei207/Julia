<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pur_AddReviewConf.aspx.cs" Inherits="plan_pur_AddReviewConf" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="margin-top: 20px;">
        <table cellspacing="4" cellpadding="0" style="border: 0px solid #d7d7d7; margin-top: 2px;">
            <tr style="height:25px;">
                <td>供应商：</td>
                <td colspan="5"><asp:DropDownList ID="ddlSupplier" runat="server"  Width="350px" 
                    DataTextField="ad_name" DataValueField="ad_addr"  
                    AutoPostBack="True"></asp:DropDownList></td>                
            </tr>
            <tr style="height:25px;">
                <td>金额：</td>
                <td>
                    <asp:TextBox ID="txtMoney" runat="server" Width="110px" CssClass="SmallTextBox"></asp:TextBox></td>
                <td>是否显示：</td>
                <td style="width:110px;">
                    <asp:CheckBox ID="chkIsShow" runat="server" /></td>
                <td></td>
            </tr>
            <tr style="height:25px;">
                <td>节点一：</td>
                <td><asp:DropDownList ID="ddlNote1" runat="server"  Width="115px" 
                    DataTextField="Node_Name" DataValueField="Node_Id"  
                    AutoPostBack="True" onselectedindexchanged="ddlNote1_SelectedIndexChanged"></asp:DropDownList></td>
                <td>节点二：</td>
                <td><asp:DropDownList ID="ddlNote2" runat="server"  Width="115px" 
                    DataTextField="Node_Name" DataValueField="Node_Id"  
                    AutoPostBack="True" onselectedindexchanged="ddlNote2_SelectedIndexChanged"></asp:DropDownList></td>
                <td>节点三：</td>
                <td><asp:DropDownList ID="ddlNote3" runat="server"  Width="115px" 
                    DataTextField="Node_Name" DataValueField="Node_Id"  
                    AutoPostBack="True"></asp:DropDownList></td>
            </tr>
            <tr style="height:25px;">
                <td colspan="6" align="center">
                    <asp:Button ID="btnSubmit" runat="server" Text="添加"  CssClass="SmallButton2" 
                        onclick="btnSubmit_Click"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnTJ" runat="server" Text="保存"  CssClass="SmallButton2" onclick="btnTJ_Click" 
                        />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnBack" runat="server" Text="返回"  CssClass="SmallButton2" 
                        onclick="btnBack_Click"/></td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    </form>
</body>
</html>
