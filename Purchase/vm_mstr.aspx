<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vm_mstr.aspx.cs" Inherits="Purchase_vm_mstr" %>

<!DOCTYPE html>

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
    <div align="Center">
    <table style="width:800px" id="tb1">
        <tr style="height:30px">
            <td>供应商&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<asp:DropDownList ID="ddl_vend" runat="server" Width="280px"  DataTextField="allname" DataValueField="usr_loginName"></asp:DropDownList></td>
            
        </tr>
        <tr style="height:30px">
            <td >模具编号&nbsp;&nbsp<asp:TextBox runat="server" ID="txt_MoldID" Width="200px"></asp:TextBox></td>
            <td style="text-align:right">模具产量&nbsp;&nbsp<asp:TextBox runat="server" ID="txt_Qty" Width="100px"></asp:TextBox></td>
        </tr>
        <tr style="height:30px">
            <td >模具状态&nbsp;&nbsp<asp:DropDownList ID="ddl_Status" runat="server" Width="100px"></asp:DropDownList></td>
            <td style="text-align:right">零件QAD号&nbsp;&nbsp<asp:TextBox runat="server" ID="txt_QAD" Width="200px"></asp:TextBox></td>
        </tr>
        <tr style="height:30px">
            <td >模具型腔&nbsp;&nbsp<asp:TextBox runat="server" ID="txt_Cavity" Width="200px"></asp:TextBox></td>
            <td style="text-align:right">图纸图号&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<asp:TextBox runat="server" ID="txt_Drawing" Width="200px"></asp:TextBox></td>
        </tr>
        <tr style="height:70px">
            <td colspan="2"  valign="top" style="text-align:right">
                备注&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<asp:TextBox TextMode="MultiLine" runat="server" Width="735px" Height="70px" ID="txt_remark"></asp:TextBox>
            </td>
        </tr>
       <tr style="height:30px">
            <td style="text-align:center"><asp:Button ID="btn_save" runat="server" CssClass="SmallButton3" Width="70px" Text="保存" OnClick="btn_save_Click" /></td>
            <td><asp:Button ID="btn_back" runat="server" CssClass="SmallButton3" Width="70px" Text="返回" OnClick="btn_back_Click"  /></td>
            
        </tr>
    </table>
    </div>
    </form>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
