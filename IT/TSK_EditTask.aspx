<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_EditTask.aspx.cs" Inherits="TSK_EditTask" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table style="width: 600px">
            <tr>
                <td align="left" colspan="2">
                    任务描述：(*可以留空,300字以内)
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Width="600px" Height="300px"
                        MaxLength="300"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    申请人：<asp:TextBox 
                        ID="txtUserNo" runat="server" Width="353px" ReadOnly="True"></asp:TextBox>
                    （*不可更改）</td>
                <td align="center">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="left">
                    上传附件(*5M以内)：
                </td>
                <td align="center">
                    &nbsp;
                    </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:HyperLink ID="hlinkFile" 
                        runat="server"
                        Target="_blank">[hlinkFile]</asp:HyperLink>
                </td>
                <td align="right">
                        <asp:LinkButton ID="linkDelete" runat="server" onclick="linkDelete_Click">Delete</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <input id="filename" runat="server" name="filename1" style="width: 100%" 
                        type="file" />
                </td>
                <td align="right">
                        <input id="hidTaskNbr" type="hidden" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: center; height: 15px;">
                </td>
                <td style="text-align: center; height: 15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="2">
                    <asp:Button ID="btnDone" runat="server" Text="SAVE" CssClass="SmallButton3" 
                        OnClick="btnDone_Click" />
                &nbsp;&nbsp;
                    <asp:Button ID="btnBack" runat="server" Text="BACK" CssClass="SmallButton3" 
                        OnClick="btnBack_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
