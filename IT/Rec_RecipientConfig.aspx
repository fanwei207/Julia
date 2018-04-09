<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rec_RecipientConfig.aspx.cs" Inherits="Rec_RecipientConfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .btnModCopyEml
        {}
    </style>
</head>
<body>
<div>
    <form id="form1" runat="server">
    <table id="tb1" cellpadding="0" cellspacing="0" bordercolor="Black" gridlines="Both"
            runat="server" style="width: 600px" align="center">
    <tr>
        <td style="height: 28px; width: 500px;">
        报表:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList 
                DataTextField="reportname" AutoPostBack="true"
             DataValueField="id" runat="Server" ID="ddl_report" Width="440px" 
                onselectedindexchanged="ddl_report_SelectedIndexChanged" 
                style="margin-top: 0px" Height="16px">
            </asp:DropDownList>
        </td>
        <td style="height: 28px; width: 100px;" align="center">
        <asp:Button ID="btnAdd" runat="server" Width="52px" CssClass="SmallButton3" 
                Text="新增报表" onclick="btnAdd_Click">
        </asp:Button>
        </td>
    </tr>
     <tr>
        <td>收件人：<asp:TextBox runat="server" ID="txtConsignee" ReadOnly="true" TextMode="SingleLine" Width="440px"></asp:TextBox></td>
        <td><asp:Button ID="btnModSendEml" runat="Server" CssClass="SmallButton3" 
                Text="添加收件人邮件" Width="97px" onclick="btnModSendEml_Click" /></td>
    </tr>
    <tr>
        <td>抄送人：<asp:TextBox runat="Server" ReadOnly="true" ID="txtCC" TextMode="SingleLine" Width="440px"></asp:TextBox></td>
        <td><asp:Button ID="btnModCopyEml" runat="Server" CssClass="SmallButton3" Text="添加抄送人邮件" 
                Width="97px" onclick="btnModCopyEml_Click" /></td>
    </tr>
    <tr >
        <td align="center"><asp:Button ID="btnSave" runat="server" Width="52px" 
                CssClass="SmallButton3" Text="保存" onclick="btnSave_Click" Visible="false"></asp:Button></td>
    </tr>
    </table>
    
    </form>
</div>
    <script language="javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
